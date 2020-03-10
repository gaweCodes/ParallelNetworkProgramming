using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatSystem.Server
{
    internal static class Program
    {
        private static readonly List<TcpClient> Clients = new List<TcpClient>();
        private static void Main()
        {
            var listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            while (true)
            {
                var client = listener.AcceptTcpClient();
                lock (Clients)
                {
                    Clients.Add(client);
                }

                Console.WriteLine("Client connected");

                var t = new Thread(HandleClient);
                t.Start(client);
            }
        }

        private static void HandleClient(object o)
        {
            var client = (TcpClient)o;

            var buffer = new byte[1024];
            var stream = client.GetStream();
            while (true)
            {
                var bytes = stream.Read(buffer, 0, buffer.Length);

                if (bytes <= 0) break;

                var data = Encoding.ASCII.GetString(buffer, 0, bytes);
                Broadcast(data, client);
            }

            lock (Clients)
            {
                Clients.Remove(client);
            }
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        private static void Broadcast(string data, TcpClient client)
        {
            Console.WriteLine(data);

            var buffer = Encoding.ASCII.GetBytes(data);
            lock (Clients)
            {
                Clients.Where(c => c != client).ToList().ForEach(c => c.GetStream()
                    .Write(buffer, 0, buffer.Length));
            }
        }
    }
}
