using board;
using chess;
using System;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPosition chessPosition = new ChessPosition('c', 7);
            Console.WriteLine(chessPosition);
            Console.WriteLine(chessPosition.ToPosition());
        }
    }
}
