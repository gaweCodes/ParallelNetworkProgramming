using System;
using System.Net;
using System.Net.Sockets;

namespace DemoSocketConnect 
{
    internal static class Program 
    {
        private static void Main() 
        {
            try 
            {
                const string host = "zbw.ch"; // Uri
                const int port = 80;

                var hostEntry = Dns.GetHostEntry(host);
                var ipAddresses = hostEntry.AddressList;

                Console.WriteLine(host + " is mapped to the IP-Address(es): ");

                foreach (IPAddress ipAddress in ipAddresses)
                    Console.Write(ipAddress.ToString());

                var ipEo = new IPEndPoint(ipAddresses[0], port);
                
                var sock = new Socket(ipEo.AddressFamily,
                    SocketType.Stream,
                    ProtocolType.Tcp);
                sock.Connect(ipEo);
                if (!sock.Connected) return;
                Console.WriteLine(" - Connection established!\n");
                sock.Close();
            } 
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
