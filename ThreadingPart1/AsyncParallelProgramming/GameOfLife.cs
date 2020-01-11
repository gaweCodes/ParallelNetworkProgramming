using System;
using System.Threading.Tasks;

namespace AsyncParallelProgramming
{
    internal class GameOfLife
    {
        public int N;
        public int[,] NewBoard()
        {
            var board = new int[N, N];
            var rand = new Random();
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N; j++)  board[i, j] = rand.NextDouble() > 0.5 ? 1 : 0;
            }
            return board;
        }
        public void Print(int[,] board)
        {
            for (var i = 0; i < N; i++)
            {
                for (var j = 0; j < N; j++)  Console.Write(board[i, j] == 1 ? "*" : ".");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public int Neighbors(int[,] board, int i, int j)
        {
            var top = i == 0 ? N - 1 : i - 1;
            var bot = i == N - 1 ? 0 : i + 1;
            var left = j == 0 ? N - 1 : j - 1;
            var right = j == N - 1 ? 0 : j + 1;
            return board[top, left] + board[top, j] + board[top, right]
                   + board[i, left] + board[i, right]
                   + board[bot, left] + board[bot, j] + board[bot, right];
        }
        public int[,] NextGeneration(int[,] board)
        {
            var newBoard = new int[N, N];
            Parallel.For(0, N, i => {
                Parallel.For(0, N, j => {
                    var neighbors = Neighbors(board, i, j);
                    if (board[i, j] == 1) newBoard[i, j] = neighbors < 2 || neighbors > 3 ? 0 : 1;
                    else newBoard[i, j] = neighbors == 3 ? 1 : 0;
                });
            });
            return newBoard;
        }
    }
}
