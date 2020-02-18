using System;
using System.Net;

namespace DemoDns 
{
    /// <summary>
    /// Das folgende Kommandozeilenprogramm illustriert einige
    /// Methoden der .NET Framework Klassen Dns und IPAddress.
    /// </summary>
    internal static class Program 
    {
        public static void Main(string[] args) 
        {
            try 
            {
                Console.WriteLine("Local Host:\n");
                var localHostName = Dns.GetHostName();
                Console.WriteLine("\tHost Name: " + localHostName);
                PrintHostInfo(localHostName);
                
                Console.WriteLine();

                Console.WriteLine("www.symas-design.ch Host:\n");
                PrintHostInfo("www.symas-design.ch");
                PrintHostInfo("217.26.54.14");
            } 
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }

            if (args.Length <= 0) return;
            foreach (var arg in args)
            {
                Console.WriteLine(arg + ":");
                PrintHostInfo(arg);
            }
        }
        private static void PrintHostInfo(string host) 
        {
            try
            {
                var hostInfo = Dns.GetHostEntry(host);
                Console.WriteLine("\tCanonical Name (CNAME): " + hostInfo.HostName);
                Console.Write("\tIP Addresses: ");
                foreach (IPAddress ipaddr in hostInfo.AddressList)
                    Console.Write(ipaddr + " ");
                Console.Write("\n\tAliases: ");
                foreach (String alias in hostInfo.Aliases)
                    Console.Write(alias + " ");
                Console.WriteLine("\n");
            } 
            catch (Exception)
            {
                Console.WriteLine("\tUnable to resolve host: " + host + "\n");
            }
        }
    }
}
