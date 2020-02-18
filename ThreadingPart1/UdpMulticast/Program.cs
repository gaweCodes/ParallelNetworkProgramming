using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Int32;

namespace UdpMulticast
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length > 3)
                throw new ArgumentException("Parameter(s): <Multicast Addr> <Port> [<TTL>]");
            if (!MulticastIpAddress.IsValid(args[0]))
                throw new ArgumentException("Valid MC addr: 224.0.0.0 - 239.255.255.255");
            var destAddr = IPAddress.Parse(args[0]);
            var destPort = args.Length > 1 ? Parse(args[1]) : 7;
            var ttl = args.Length == 3 ? Parse(args[2]) : 100;
            var t1 = Task.Factory.StartNew(() => { UdPReceive(destAddr, destPort); });
            var t2 = Task.Factory.StartNew(() => { UdpSend(destAddr, destPort, ttl); });
            Task.WaitAll(t1, t2);
            Console.ReadLine();
        }

        private static void UdpSend(IPAddress destAddr, int destPort, int ttl)
        {
            const string msg = "Hallo";
            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, ttl);
            var byteBuffer = Encoding.ASCII.GetBytes(msg);
            var endPoint = new IPEndPoint(destAddr, destPort);
            sock.SendTo(byteBuffer, 0, byteBuffer.Length, SocketFlags.None, endPoint);
            sock.Close();
        }

        private static void UdPReceive(IPAddress destAddr, int destPort)
        {
            var sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            var endPoint = new IPEndPoint(IPAddress.Any, destPort);
            sock.Bind(endPoint);
            // Mitgliedschaft in der Multicast Gruppe
            sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership,
                new MulticastOption(destAddr, IPAddress.Any));
            
            var receivePoint = new IPEndPoint(IPAddress.Any, 0);
            var tempReceivePoint = (EndPoint) receivePoint;
            
            var data = new byte[1024];
            var length = sock.ReceiveFrom(data, 0, 1024, SocketFlags.None, ref tempReceivePoint);
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, length));

            // Kündige Mitgliedschaft in der Multicast Gruppe
            sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership,
                new MulticastOption(destAddr, IPAddress.Any));
            sock.Close();
        }
    }

    public class MulticastIpAddress
    {
        // Check for a valid multicast address
        public static bool IsValid(string ip)
        {
            try
            {
                var octet1 = Parse(ip.Split(new[] {'.'}, 4)[0]);
                if (octet1 >= 224 && (octet1 <= 239))
                    return true;
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }
    }
}
