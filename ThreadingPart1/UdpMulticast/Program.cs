using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Int32;

namespace UdpMulticast
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string msg = "Hallo";
            if (args.Length < 2 || args.Length > 3) throw new ArgumentException("Parameter(s): <Multicast Addr> <Port> [<TTL>]");
            if (!MulticastIpAddress.IsValid(args[0])) throw new ArgumentException("Valid MC addr: 224.0.0.0 - 239.255.255.255");
            var destAddr = IPAddress.Parse(args[0]);
            var destPort = args.Length > 1 ? Parse(args[1]) : 7;
            var ttl = args.Length == 3 ? Parse(args[2]) : 1;
            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
            var byteBuffer = Encoding.ASCII.GetBytes(msg);
            var endPoint = new IPEndPoint(destAddr, destPort);
            sock.SendTo(byteBuffer, 0, byteBuffer.Length, SocketFlags.None, endPoint);
            sock.Close();
        }
    }
}
