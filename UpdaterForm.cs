using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AYGEST.Updater
{
    public partial class UpdaterForm : Form
    {
        // Repo: https://github.com/AYF-TRADING-SOLUTIONS/AYGEST-ERP
        private const string Owner = "AYF-TRADING-SOLUTIONS";
        private const string Repo = "AYGEST-ERP";

        private const string AppExeName = "AYGEST ERP.exe";
        private static readonly string InstallDir = @"C:\AYF\AYGEST";
        private static readonly string UpdaterDir = @"C:\AYF\Updater";
        private static readonly string WorkDir = Path.Combine(UpdaterDir, "work");

        private bool _restart = false;
        private int? _parentPid = null;

        public UpdaterForm(string[] args)
        {
            InitializeComponent();
            ParseArgs(args);

            Directory.CreateDirectory(UpdaterDir);
            Directory.CreateDirectory(WorkDir);

            progressBar.Value = 0;
            btnClose.Enabled = false; // só habilita no fim/erro

            btnCloseAction.Enabled = false;

            borderlessForm.DragForm = true;
            borderlessForm.DockIndicatorTransparencyValue = 0.6;
            borderlessForm.TransparentWhileDrag = true;

        }

        private void ParseArgs(string[] args)
        {
            foreach (var a in args ?? new string[0])
            {
                if (a.Equals("--restart", StringComparison.OrdinalIgnoreCase))
                    _restart = true;

                if (a.StartsWith("--parentPid=", StringComparison.OrdinalIgnoreCase))
                {
                    var s = a.Substring("--parentPid=".Length);
                    if (int.TryParse(s, out var pid))
                        _parentPid = pid;
                }
            }
        }
        private bool _allowClose = false;
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!_allowClose)
            {
                e.Cancel = true;
                return;
            }
            base.OnFormClosing(e);
        }
        private async void UpdaterForm_Shown(object sender, EventArgs e)
        {
            try
            {
                SetStatus("A preparar verificação...", 2);

                // 1) garantir que AYGEST está fechado
                await CloseParentProcessIfAny();
                KillByNameIfStillRunning("AYGEST ERP");

                // 2) ler versão local
                var local = GetLocalVersion();
                SetStatus($"Versão instalada: {local}", 8);

                // 3) buscar latest release
                SetStatus("A consultar GitHub Releases...", 15);
                var latest = await GetLatestReleaseAsync();

                // 4) baixar version.json
                var verAsset = latest.assets?.FirstOrDefault(a => a.name.Equals("version.json", StringComparison.OrdinalIgnoreCase));
                if (verAsset == null) throw new Exception("Asset version.json não encontrado na release latest.");

                SetStatus("A ler política de update...", 20);
                var versionJson = await DownloadStringAsync(verAsset.browser_download_url);

                var info = JsonConvert.DeserializeObject<VersionInfo>(versionJson);
                if (info == null || string.IsNullOrWhiteSpace(info.version))
                    throw new Exception("version.json inválido.");

                var remote = new Version(info.version);
                SetStatus($"Versão disponível: {remote}", 25);

                // 5) sem update → só reinicia app e sai
                if (remote <= local)
                {
                    SetStatus("Sem atualização. A iniciar AYGEST...", 100);
                    if (_restart) StartApp(updatedFlag: true);
                    btnClose.Enabled = true;
                    Close();
                    return;
                }

                // 6) update obrigatório (no teu caso é sempre true, mas validamos)
                if (!info.mandatory)
                {
                    SetStatus("Atualização disponível (não obrigatória). A iniciar AYGEST...", 100);
                    if (_restart) StartApp(updatedFlag: true);
                    btnClose.Enabled = true;
                    Close();
                    return;
                }

                // 7) baixar instalador
                var setupName = info.installerAssetName;
                if (string.IsNullOrWhiteSpace(setupName))
                    throw new Exception("installerAssetName vazio no version.json.");

                var setupAsset = latest.assets?.FirstOrDefault(a => a.name.Equals(setupName, StringComparison.OrdinalIgnoreCase));
                if (setupAsset == null)
                    throw new Exception("Instalador não encontrado na release: " + setupName);

                var setupPath = Path.Combine(WorkDir, setupName);

                SetStatus($"A baixar {setupName}...", 30);
                await DownloadFileWithProgressAsync(setupAsset.browser_download_url, setupPath, 30, 80);

                // 8) instalar silencioso (Inno)
                SetStatus("A aplicar atualização...", 85);
                var code = RunInnoSilent(setupPath);

                if (code != 0)
                    throw new Exception("Instalação falhou. Código: " + code);

                // 9) reiniciar app
                SetStatus("Atualizado com sucesso. A iniciar AYGEST...", 100);
                if (_restart) StartApp(updatedFlag: true);

                btnClose.Enabled = true;
                btnCloseAction.Enabled = true;
                _allowClose = true;
                Close();
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex);
                SetStatus("Falha na atualização: " + ex.Message, 100);

                // Como é obrigatório, podes optar por NÃO abrir o AYGEST aqui.
                // Se quiseres abrir mesmo assim, chama StartApp(updatedFlag:true).
                btnClose.Enabled = true;
                btnCloseAction.Enabled = true;
                _allowClose = true;
            }
        }

        private async Task CloseParentProcessIfAny()
        {
            if (!_parentPid.HasValue) return;

            try
            {
                var p = Process.GetProcessById(_parentPid.Value);
                if (p.HasExited) return;

                SetStatus("A fechar AYGEST...", 12);
                p.CloseMainWindow();
                await Task.Run(() => p.WaitForExit(6000));

                if (!p.HasExited)
                    p.Kill();
            }
            catch { /* ignora */ }
        }

        private void KillByNameIfStillRunning(string name)
        {
            foreach (var p in Process.GetProcessesByName(name))
            {
                try
                {
                    if (!p.HasExited) p.Kill();
                }
                catch { }
            }
        }

        private Version GetLocalVersion()
        {
            var exe = Path.Combine(InstallDir, AppExeName);
            if (!File.Exists(exe)) return new Version(0, 0, 0, 0);

            var fvi = FileVersionInfo.GetVersionInfo(exe);
            if (Version.TryParse(fvi.FileVersion, out var v)) return v;

            return new Version(0, 0, 0, 0);
        }

        private async Task<GithubRelease> GetLatestReleaseAsync()
        {
            var url = $"https://api.github.com/repos/{Owner}/{Repo}/releases/latest";

            using (var http = CreateHttp())
            {
                var json = await http.GetStringAsync(url);
                var rel = JsonConvert.DeserializeObject<GithubRelease>(json);
                if (rel == null) throw new Exception("Não foi possível ler a release latest.");
                return rel;
            }
        }

        private async Task<string> DownloadStringAsync(string url)
        {
            using (var http = CreateHttp())
            using (var resp = await http.GetAsync(url))
            {
                var body = await resp.Content.ReadAsStringAsync();

                if (!resp.IsSuccessStatusCode)
                    throw new Exception($"HTTP {(int)resp.StatusCode} ({resp.ReasonPhrase}) ao baixar: {url}\n{body}");

                return body;
            }
        }


        private async Task DownloadFileWithProgressAsync(string url, string path, int startPercent, int endPercent)
        {
            using (var http = CreateHttp())
            using (var resp = await http.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                if (!resp.IsSuccessStatusCode)
                {
                    var body = await resp.Content.ReadAsStringAsync();
                    throw new Exception($"HTTP {(int)resp.StatusCode} ({resp.ReasonPhrase}) ao baixar: {url}\n{body}");
                }

                var total = resp.Content.Headers.ContentLength ?? -1L;
                var canReport = total > 0;

                using (var input = await resp.Content.ReadAsStreamAsync())
                using (var output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var buffer = new byte[81920];
                    long readTotal = 0;
                    int read;
                    var lastUi = Environment.TickCount;

                    while ((read = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await output.WriteAsync(buffer, 0, read);
                        readTotal += read;

                        if (canReport && Environment.TickCount - lastUi > 120)
                        {
                            lastUi = Environment.TickCount;
                            var raw = (double)readTotal / total;
                            var pct = startPercent + (int)Math.Round(raw * (endPercent - startPercent));
                            pct = Math.Max(startPercent, Math.Min(endPercent, pct));
                            SetProgress(pct);
                        }
                    }
                }
            }

            SetProgress(endPercent);
        }

        private int RunInnoSilent(string installerPath)
        {
            var args =
                "/VERYSILENT /SUPPRESSMSGBOXES /NORESTART " +
                "/DIR=\"C:\\AYF\\AYGEST\" " +
                "/LOG=\"C:\\AYF\\Updater\\work\\install.log\"";

            var psi = new ProcessStartInfo
            {
                FileName = installerPath,
                Arguments = args,
                UseShellExecute = true,
                WorkingDirectory = WorkDir
            };

            var p = Process.Start(psi);
            if (p == null) return 1;

            p.WaitForExit();
            return p.ExitCode;
        }

        private void StartApp(bool updatedFlag)
        {
            var exe = Path.Combine(InstallDir, AppExeName);
            if (!File.Exists(exe)) return;

            var args = updatedFlag ? "--updated" : "";

            Process.Start(new ProcessStartInfo
            {
                FileName = exe,
                Arguments = args,
                UseShellExecute = true
            });
        }
        private HttpClient CreateHttp()
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.UserAgent.ParseAdd("AYGEST-Updater-Net48/1.0");
            http.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
            return http;
        }


        private void SetStatus(string text, int pct)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => SetStatus(text, pct)));
                return;
            }

            lblStatus.Text = text;
            SetProgress(pct);
        }

        private void SetProgress(int pct)
        {
            if (pct < 0) pct = 0;
            if (pct > 100) pct = 100;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => SetProgress(pct)));
                return;
            }

            progressBar.Value = pct;
        }

        private void Log(string msg)
        {
            try
            {
                File.AppendAllText(Path.Combine(WorkDir, "updater.log"),
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + msg + Environment.NewLine);
            }
            catch { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
