using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Pitfall2 
{
    internal class Downloader
    {
        private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        public async Task DownloadAsync()
        {
            await SemaphoreSlim.WaitAsync();
            try
            {
                Console.WriteLine("BEFORE " + Thread.CurrentThread.ManagedThreadId);
                var client = new HttpClient();
                var task = client.GetStringAsync("https://msdn.microsoft.com");
                await task;
                Console.WriteLine("AFTER " + Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                SemaphoreSlim.Release();
                Console.ReadLine();
            }
        }
    }
}
