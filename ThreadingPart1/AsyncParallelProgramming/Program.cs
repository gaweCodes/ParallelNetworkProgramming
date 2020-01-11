using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncParallelProgramming
{
    internal class Program
    {
        private static readonly int delay = 100;
        private static void Main()
        {
            var x = new GameOfLife {N = 1000};
            var board = x.NewBoard();
            x.Print(board);
            const int generations = 1;
            for (var i = 0; i < generations; i++)
            {
                board = x.NextGeneration(board);
                x.Print(board);
            }

            return;
            var sw1 = new Stopwatch();
            var sw2 = new Stopwatch();
            var sw3 = new Stopwatch();
            var sw4 = new Stopwatch();

            sw1.Start();
            DoAsync();
            sw1.Stop();

            sw2.Start();
            DoParallel();
            sw2.Start();
            
            sw3.Start();
            DoNormal();
            sw3.Stop();

            sw4.Start();
            DoBoth();
            sw4.Stop();

            Console.WriteLine();
            Console.WriteLine($"Async: {sw1.ElapsedMilliseconds} Parallel: {sw2.ElapsedMilliseconds} Normal: {sw3.ElapsedMilliseconds} Both: {sw4.ElapsedMilliseconds}");
        }
        private static void DoBoth()
        {
            try
            {
                var dir = new DirectoryInfo(@"C:\Users\Gabriel\source\repos\GitHub\ParallelNetworkProgramming");
                var files = dir.GetFiles("*.cs", SearchOption.AllDirectories);
                Console.WriteLine("Files in " + dir.FullName + " containing 'Using'");
                Parallel.ForEach(files, async file => {
                    var r = new StreamReader(file.FullName);
                    var s = await r.ReadToEndAsync();
                    Thread.Sleep(delay);
                    if (s.IndexOf("Using", StringComparison.Ordinal) >= 0) Console.WriteLine(" " + file.Name);
                });
            }
            catch (ArgumentException)
            {
                Console.WriteLine("-- invalid directory name");
            }
        }
        private static void DoNormal()
        {
            try
            {
                var dir = new DirectoryInfo(@"C:\Users\Gabriel\source\repos\GitHub\ParallelNetworkProgramming");
                var files = dir.GetFiles("*.cs", SearchOption.AllDirectories);
                Console.WriteLine("Files in " + dir.FullName + " containing 'Using'");
                foreach(var file in files) 
                {
                    var r = new StreamReader(file.FullName);
                    var s = r.ReadToEnd();
                    Thread.Sleep(delay);
                    if (s.IndexOf("Using", StringComparison.Ordinal) >= 0) Console.WriteLine(" " + file.Name);
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("-- invalid directory name");
            }
        }
        private static void DoParallel()
        {
            try
            {
                var dir = new DirectoryInfo(@"C:\Users\Gabriel\source\repos\GitHub\ParallelNetworkProgramming");
                var files = dir.GetFiles("*.cs", SearchOption.AllDirectories);
                Console.WriteLine("Files in " + dir.FullName + " containing 'Using'");
                Parallel.ForEach(files, file => {
                    var r = new StreamReader(file.FullName);
                    var s = r.ReadToEnd();
                    Thread.Sleep(delay);
                    if (s.IndexOf("Using", StringComparison.Ordinal) >= 0) Console.WriteLine(" " + file.Name);
                });
            }
            catch (ArgumentException)
            {
                Console.WriteLine("-- invalid directory name");
            }
        }
        private static void DoAsync()
        {
            try
            {
                var dir = new DirectoryInfo(@"C:\Users\Gabriel\source\repos\GitHub\ParallelNetworkProgramming");
                var files = dir.GetFiles("*.cs", SearchOption.AllDirectories);
                var t = SearchFilesAsync(@"C:\Users\Gabriel\source\repos\GitHub\ParallelNetworkProgramming", files, "using");
                t.Wait();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("-- invalid directory name");
            }
        }
        private static async Task SearchFilesAsync(string dirName, FileInfo[] files, string pattern)
        {
            Console.WriteLine($"Files in {dirName} containing '{pattern}'");
            foreach (var file in files)
            {
                var r = new StreamReader(file.FullName);
                if ((await r.ReadToEndAsync()).IndexOf(pattern, StringComparison.Ordinal) >= 0)
                {
                    Thread.Sleep(delay);
                    Console.WriteLine(file.DirectoryName + " " + file.Name);
                }
            }
        }
    }
}
