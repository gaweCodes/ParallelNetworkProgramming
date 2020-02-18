using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Int32;

namespace DemoUDPSender 
{
    internal static class Program 
    {
        private static void Main(string[] args) 
        {
            const string msg = "Hallo";
            if (args.Length > 2) throw new ArgumentException("Parameter(s): <Destination> [<Port>]");
            var server = args.Length >= 1 ? args[0] : Dns.GetHostName();

            var destPort = (args.Length > 1) ? Parse(args[1]) : 7;

            var client = new UdpClient();

            var byteBuffer = Encoding.ASCII.GetBytes(msg);

            client.Send(byteBuffer, byteBuffer.Length, server, destPort);

            client.Close();
        }
    }
}