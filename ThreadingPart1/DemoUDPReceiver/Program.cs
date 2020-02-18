using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Int32;

namespace DemoUDPReceiver 
{
    internal static class Program 
    {
        private static void Main(string[] args) 
        {
            if (args.Length > 1) throw new ArgumentException("Parameter(s): [<Port>]");
            var port = args.Length == 1 ? Parse(args[0]) : 7;
            var client = new UdpClient(port);
            var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, port);
            var data = client.Receive(ref remoteIpEndPoint);
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, data.Length));
            client.Close();
            Console.ReadKey();
        }
    }
}
