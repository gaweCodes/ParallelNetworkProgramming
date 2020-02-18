using System;
using System.Net;
using System.Net.Sockets;

namespace DemoTCPEchoServer 
{
    internal static class Program
    {
        private const int Bufsize = 32;
        private const int Backlog = 5;
        private static void Main(string[] args) 
        {
            if (args.Length > 1) throw new ArgumentException("Parameters: [<Port>]");
            var servPort = args.Length == 1 ? int.Parse(args[0]) : 7;

            Socket servSock = null;
            try 
            {
                servSock = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                servSock.Bind(new IPEndPoint(IPAddress.IPv6Any, servPort));
                servSock.Listen(Backlog);
            } 
            catch (SocketException se) 
            {
                Console.WriteLine(se.ErrorCode + ": " + se.Message);
                Environment.Exit(se.ErrorCode);
            }
            var rcvBuffer = new byte[Bufsize];
            while(true) 
            {
                Socket client = null;
                try 
                {
                    client = servSock.Accept();
                    Console.Write("Handling client at " + client.RemoteEndPoint + " - ");
                    var totalbytesEchoed = 0;
                    int bytesRcvd;
                    while ((bytesRcvd = client.Receive(rcvBuffer, 0, rcvBuffer.Length, SocketFlags.None)) > 0) 
                    {
                        client.Send(rcvBuffer, 0, bytesRcvd, SocketFlags.None);
                        totalbytesEchoed += bytesRcvd;
                    }

                    Console.WriteLine("echoed {0} bytes.", totalbytesEchoed);

                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                } 
                catch (Exception e) 
                {
                    Console.WriteLine(e.Message);
                    client?.Close();
                }
            }
        }
    }
}