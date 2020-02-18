using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Int32;

namespace DemoTCPEchoClient 
{
    internal static class Program 
    {
        private static void Main(string[] args) 
        {
            if (args.Length > 2) throw new ArgumentException("Parameters: [<Server>] [<Port>]");
            while (true) 
            {
                Console.WriteLine("Enter input:");
                var line = Console.ReadLine();
                if (line == "exit" || line == null) break;
                var byteBuffer = Encoding.ASCII.GetBytes(line);
                var server = args.Length >= 1 ? args[0] : Dns.GetHostName();
                var servPort = args.Length >= 2 ? Parse(args[1]) : 7;
                Socket clientSock = null;
                try 
                {
                    var serverEndPoint = new IPEndPoint(Dns.GetHostEntry(server).AddressList[0], servPort);
                    if (serverEndPoint.AddressFamily != AddressFamily.InterNetworkV6)
                    { 
                        Console.WriteLine("Not a valid IPv6 address.");
                        return;
                    }
                    clientSock = new Socket(serverEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    clientSock.Connect(serverEndPoint);
                    Console.WriteLine("Connected to server... sending echo string");

                    clientSock.Send(byteBuffer, 0, byteBuffer.Length, SocketFlags.None);

                    Console.WriteLine("Sent {0} bytes to server...", byteBuffer.Length);

                    var totalBytesRcvd = 0;

                    while (totalBytesRcvd < byteBuffer.Length) 
                    {
                        int bytesRcvd;
                        if ((bytesRcvd = clientSock.Receive(byteBuffer, totalBytesRcvd, byteBuffer.Length - totalBytesRcvd, SocketFlags.None)) == 0) 
                        {
                            Console.WriteLine("Connection closed preaturely.");
                            break;
                        }

                        totalBytesRcvd += bytesRcvd;
                    }

                    Console.WriteLine("Received {0} bytes from server: {1}", totalBytesRcvd, Encoding.ASCII.GetString(byteBuffer, 0, totalBytesRcvd));

                    clientSock.Shutdown(SocketShutdown.Both);
                } 
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                } 
                finally 
                {
                    clientSock?.Close();
                }
            }
        }

    }
}
