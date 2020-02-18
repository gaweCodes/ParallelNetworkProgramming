using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientTcpListener
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length > 2) throw new ArgumentException("Parameters: [<Server>] [<Port>]");
            var server = args.Length == 1 ? args[0] : Dns.GetHostName();
            var serverPort = args.Length == 2 ? int.Parse(args[1]) : 7;
            Task.Factory.StartNew(() => Listen(serverPort));
            var t2 = Task.Factory.StartNew(() => Send(server, serverPort));
            t2.Wait();
            Console.ReadKey();
        }
        private static void Listen(int serverPort)
        { 
            const int bufsize = 32;
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.IPv6Any, serverPort);
                listener.Start();
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.ErrorCode + ": " + se.Message);
                Environment.Exit(se.ErrorCode);
            }
            var rcvBuffer = new byte[bufsize];
            while (true)
            {
                NetworkStream netStream = null;
                try
                {
                    var client = listener.AcceptTcpClient();
                    netStream = client.GetStream();
                    var totalBytesEchoed = 0;
                    int bytesRcvd;
                    while ((bytesRcvd = netStream.Read(rcvBuffer, 0, rcvBuffer.Length)) > 0)
                    {
                        netStream.Write(rcvBuffer, 0, bytesRcvd);
                        totalBytesEchoed += bytesRcvd;
                    }
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
        private static void Send(string server, int serverPort)
        {
            while (true)
            {
                Console.WriteLine("Enter input:");
                var line = Console.ReadLine();
                if (line == "exit" || line == null) break;

                var byteBuffer = Encoding.ASCII.GetBytes(line);
                TcpClient client = null;
                NetworkStream netStream = null;
                try
                {
                    client = new TcpClient(server, serverPort);
                    netStream = client.GetStream();
                    netStream.Write(byteBuffer, 0, byteBuffer.Length);
                    var totalBytesReceived = 0;
                    while (totalBytesReceived < byteBuffer.Length)
                    {
                        int bytesRcvd;
                        if ((bytesRcvd = netStream.Read(byteBuffer, totalBytesReceived, byteBuffer.Length - totalBytesReceived)) == 0)
                        {
                            Console.WriteLine("Connection closed prematurely.");
                            break;
                        }

                        totalBytesReceived += bytesRcvd;
                    }

                    Console.WriteLine(
                        $"Received {totalBytesReceived} bytes from server: {Encoding.ASCII.GetString(byteBuffer, 0, totalBytesReceived)}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    netStream?.Close();
                    client?.Close();
                }
            }
        }
    }
}
