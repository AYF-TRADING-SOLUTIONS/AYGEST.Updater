namespace AYGEST.Updater
{
    public class VersionInfo
    {
        public string version { get; set; }
        public bool mandatory { get; set; }
        public string installerAssetName { get; set; }
    }

    public class GithubRelease
    {
        public string tag_name { get; set; }
        public GithubAsset[] assets { get; set; }
    }

    public class GithubAsset
    {
        public string name { get; set; }
        public string browser_download_url { get; set; }
        public long size { get; set; }
    }
}
