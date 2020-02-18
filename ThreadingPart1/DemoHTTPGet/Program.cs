using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DemoHTTPGet 
{
    internal static class Program 
    {
        private static void Main(string[] args) 
        {
            const int port = 80;
            var host = args.Length == 0 ? Dns.GetHostName() : args[0];
            var result = SocketSendReceive(host, port);
            Console.WriteLine(result);
            File.WriteAllText("DemoHTTPGet.html", result);
        }
        private static Socket ConnectSocket(string server, int port) 
        {
            Socket sock = null;
            var hostEntry = Dns.GetHostEntry(server);
            foreach (var address in hostEntry.AddressList) 
            {
                var ipo = new IPEndPoint(address, port);
                var tempSocket = new Socket(ipo.AddressFamily, SocketType.Stream,  ProtocolType.Tcp);
                tempSocket.Connect(ipo);
                if (!tempSocket.Connected) continue;
                sock = tempSocket;
                break;
            }
            return sock;
        }
        private static string SocketSendReceive(string server, int port) 
        {
            var request = "GET / HTTP/1.1\r\nHost: " + server + "\r\nConnection: Close\r\n\r\n";
            var bytesSent = Encoding.ASCII.GetBytes(request);
            var bytesReceived = new byte[4096];
            var sock = ConnectSocket(server, port);
            if (sock == null) return "Connection failed!";

            sock.Send(bytesSent, bytesSent.Length, SocketFlags.None);
            int bytes;
            var page = "Default HTML page on " + server + ":\r\n";

            do 
            {
                bytes = sock.Receive(bytesReceived, bytesReceived.Length, SocketFlags.None);
                page += Encoding.ASCII.GetString(bytesReceived, 0, bytes);
            } while (bytes > 0);

            // Unterbinde alle weiteren Send() und Receive() Aktivitäten am Socket
            sock.Shutdown(SocketShutdown.Both);
            sock.Close();

            return page;
        }
    }
}