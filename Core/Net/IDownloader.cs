namespace Core.Net
{
    public interface IDownloader
    {
        string DownloadToString(string url);
    }
}
