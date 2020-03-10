using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatSystem.Client
{
    internal static class Program
    {
        private static void Main()
        {
            var ip = IPAddress.Parse("127.0.0.1");
            var ipe = new IPEndPoint(ip, 5000);
            using var client = new TcpClient();
            client.Connect(ipe);
            var t = new Thread(ReceiveData) { IsBackground = true };
            t.Start(client);

            var stream = client.GetStream();
            while (true)
            {
                var s = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(s)) break;

                var buffer = Encoding.ASCII.GetBytes(s);
                stream.Write(buffer, 0, buffer.Length);
            }
            client.Client.Shutdown(SocketShutdown.Send);
            t.Join();
        }
        private static void ReceiveData(object o)
        {
            var client = (TcpClient)o;
            var stream = client.GetStream();
            var buffer = new byte[1024];
            while (true)
            {
                var bytes = stream.Read(buffer, 0, buffer.Length);
                if (bytes <= 0) break;
                
                var s = Encoding.ASCII.GetString(buffer, 0, bytes);
                Console.WriteLine(s);
            }
        }
    }
}
