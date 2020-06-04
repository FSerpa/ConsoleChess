using board;
using chess;
using System;
using System.Net.NetworkInformation;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();

                while (!match.GameOver)
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();
                    Console.Write("Destination :");
                    Position destination = Screen.ReadChessPosition().ToPosition();

                    match.PerformMovement(origin, destination);
                }
            }catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}
