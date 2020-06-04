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
            ChessMatch match = new ChessMatch();

            while (!match.GameOver)
            {
                try
                {
                    Console.Clear();
                    Screen.PrintBoard(match.Board);

                    Console.WriteLine();
                    Console.WriteLine("Turn #" + match.Turn);
                    Console.WriteLine("Now Playing: " + match.NowPlaying + "s");
                    Console.Write("Origin: ");
                    Position origin = Screen.ReadChessPosition().ToPosition();
                    match.ValidateOriginPosition(origin);

                    bool[,] possiblePositions = match.Board.Piece(origin).PossibleMovements();

                    Console.Clear();
                    Screen.PrintBoard(match.Board, possiblePositions);

                    Console.WriteLine();
                    Console.WriteLine("Turn #" + match.Turn);
                    Console.WriteLine("Now Playing: " + match.NowPlaying + "s");
                    Console.Write("Destination :");
                    Position destination = Screen.ReadChessPosition().ToPosition();
                    match.ValidateDestinationPosition(origin, destination);

                    match.PlayNow(origin, destination);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
                catch (Exception)
                {
                    Console.WriteLine("This position doesn't exists");
                    Console.ReadLine();
                }
            }
        }
    }
}
