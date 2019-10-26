using System;
using System.Threading;

namespace Task2
{
    class Program
    {
        private static char[] _file1;
        private static char[] _file2;
        private static void Main()
        {
            var fileReader1 = new Thread(ReadFile1);
            var fileReader2 = new Thread(ReadFile2);
            fileReader1.Start();
            fileReader2.Start();
            fileReader1.Join();
            fileReader2.Join();
            if (_file1.Length == _file2.Length)
            {
                var i = 0;
                while (i < _file1.Length && _file1[i] == _file2[i]) i++;
                Console.WriteLine(i == _file1.Length ? "Files are equal" : "Files are not equal");
            }
            else
                Console.WriteLine("Files are not equal");
            Console.Read();
        }

        private static void ReadFile1()
        {
            _file1 = "Das ist ein Test Textdatei Input.".ToCharArray();
        }
        private static void ReadFile2()
        {
            _file2 = "Das ist ein Test Textdatei Input.".ToCharArray();
        }
    }
}
