using System;
using System.Net;
using System.Net.Sockets;

namespace DemoTCPEchoServerTcpListener 
{
    internal static class Program 
    {
        private const int Bufsize = 32;
        private static void Main(string[] args) 
        {
            if (args.Length > 1) throw new ArgumentException("Parameters: [<Port>]");
            var servPort = args.Length == 1 ? int.Parse(args[0]) : 7;

            TcpListener listener = null;

            try 
            {
                listener = new TcpListener(IPAddress.IPv6Any, servPort);
                listener.Start();
            } 
            catch (SocketException se) 
            {
                Console.WriteLine(se.ErrorCode + ": " + se.Message);
                Environment.Exit(se.ErrorCode);
            }
            var rcvBuffer = new byte[Bufsize];
            while(true) 
            {
                NetworkStream netStream = null;
                try 
                {
                    var client = listener.AcceptTcpClient();
                    netStream = client.GetStream();
                    Console.Write("Handling client - ");
                    var totalBytesEchoed = 0;
                    int bytesRcvd;
                    while ((bytesRcvd = netStream.Read(rcvBuffer, 0, rcvBuffer.Length)) > 0) 
                    {
                        netStream.Write(rcvBuffer, 0, bytesRcvd);
                        totalBytesEchoed += bytesRcvd;
                    }
                    Console.WriteLine("echoed {0} bytes.", totalBytesEchoed);
                    netStream.Close();
                    client.Close();
                } 
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    netStream?.Close();
                }
            }
        }
    }
}
