namespace Pitfall2 
{
    internal class Program 
    {
        private static void Main() => new Downloader().DownloadAsync().Wait();
    }
}
