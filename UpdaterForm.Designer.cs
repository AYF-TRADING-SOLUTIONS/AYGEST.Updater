namespace AYGEST.Updater
{
    partial class UpdaterForm
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2BorderlessForm borderlessForm;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.contentPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.picIcon = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.progressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.lblPercent = new System.Windows.Forms.Label();
            this.btnCloseAction = new Guna.UI2.WinForms.Guna2Button();
            this.lbNovaVersao = new System.Windows.Forms.Label();
            this.contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // borderlessForm
            // 
            this.borderlessForm.BorderRadius = 18;
            this.borderlessForm.ContainerControl = this;
            this.borderlessForm.DockIndicatorTransparencyValue = 0.6D;
            this.borderlessForm.ResizeForm = false;
            this.borderlessForm.TransparentWhileDrag = true;
            // 
            // contentPanel
            // 
            this.contentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentPanel.BackColor = System.Drawing.Color.Transparent;
            this.contentPanel.BorderRadius = 18;
            this.contentPanel.Controls.Add(this.lblStatus);
            this.contentPanel.Controls.Add(this.picIcon);
            this.contentPanel.Controls.Add(this.lblTitle);
            this.contentPanel.Controls.Add(this.progressBar);
            this.contentPanel.Controls.Add(this.lblPercent);
            this.contentPanel.Controls.Add(this.btnCloseAction);
            this.contentPanel.Location = new System.Drawing.Point(24, 44);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(22);
            this.contentPanel.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.contentPanel.ShadowDecoration.Depth = 18;
            this.contentPanel.ShadowDecoration.Enabled = true;
            this.contentPanel.Size = new System.Drawing.Size(961, 415);
            this.contentPanel.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(35, 363);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 30);
            this.lblStatus.TabIndex = 6;
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.Transparent;
            this.picIcon.Image = global::AYGEST.Updater.Properties.Resources.AYGEST_WHITE;
            this.picIcon.ImageRotate = 0F;
            this.picIcon.Location = new System.Drawing.Point(704, 25);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(232, 108);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(333, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(353, 37);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Atualização do AYGEST ERP";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar.BorderRadius = 6;
            this.progressBar.FillColor = System.Drawing.Color.Transparent;
            this.progressBar.Location = new System.Drawing.Point(172, 161);
            this.progressBar.Name = "progressBar";
            this.progressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.progressBar.ProgressColor2 = System.Drawing.Color.Navy;
            this.progressBar.Size = new System.Drawing.Size(638, 38);
            this.progressBar.TabIndex = 3;
            this.progressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // lblPercent
            // 
            this.lblPercent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPercent.AutoSize = true;
            this.lblPercent.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercent.ForeColor = System.Drawing.Color.White;
            this.lblPercent.Location = new System.Drawing.Point(168, 219);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(47, 31);
            this.lblPercent.TabIndex = 4;
            this.lblPercent.Text = "0%";
            // 
            // btnCloseAction
            // 
            this.btnCloseAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseAction.BorderColor = System.Drawing.Color.White;
            this.btnCloseAction.BorderRadius = 10;
            this.btnCloseAction.BorderThickness = 1;
            this.btnCloseAction.Enabled = false;
            this.btnCloseAction.FillColor = System.Drawing.Color.Empty;
            this.btnCloseAction.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F);
            this.btnCloseAction.ForeColor = System.Drawing.Color.White;
            this.btnCloseAction.Location = new System.Drawing.Point(799, 353);
            this.btnCloseAction.Name = "btnCloseAction";
            this.btnCloseAction.Size = new System.Drawing.Size(140, 36);
            this.btnCloseAction.TabIndex = 5;
            this.btnCloseAction.Text = "Fechar";
            // 
            // lbNovaVersao
            // 
            this.lbNovaVersao.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbNovaVersao.AutoSize = true;
            this.lbNovaVersao.BackColor = System.Drawing.Color.Transparent;
            this.lbNovaVersao.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lbNovaVersao.ForeColor = System.Drawing.Color.White;
            this.lbNovaVersao.Location = new System.Drawing.Point(32, 9);
            this.lbNovaVersao.Name = "lbNovaVersao";
            this.lbNovaVersao.Size = new System.Drawing.Size(0, 28);
            this.lbNovaVersao.TabIndex = 7;
            // 
            // UpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.BackgroundImage = global::AYGEST.Updater.Properties.Resources.Ativo_2dasf;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1010, 503);
            this.Controls.Add(this.lbNovaVersao);
            this.Controls.Add(this.contentPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdaterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AYGEST Updater";
            this.Shown += new System.EventHandler(this.UpdaterForm_Shown);
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Guna.UI2.WinForms.Guna2Panel contentPanel;
        private Guna.UI2.WinForms.Guna2PictureBox picIcon;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2ProgressBar progressBar;
        private System.Windows.Forms.Label lblPercent;
        private Guna.UI2.WinForms.Guna2Button btnCloseAction;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lbNovaVersao;
    }
}
