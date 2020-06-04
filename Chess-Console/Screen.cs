using board;
using chess;
using System;
using System.Collections.Generic;

namespace Chess_Console
{
    class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCaptured(match);
            Console.WriteLine();
            Console.WriteLine("Turn #" + match.Turn);
            Console.WriteLine("Now Playing: " + match.NowPlaying + "s");
            if (match.Check)
            {
                Console.WriteLine("CHECK");
            }
        }
        public static void PrintMatch(ChessMatch match, bool[,] possiblePositions)
        {
            PrintBoard(match.Board, possiblePositions);
            Console.WriteLine();
            PrintCaptured(match);
            Console.WriteLine();
            Console.WriteLine("Turn #" + match.Turn);
            Console.WriteLine("Now Playing: " + match.NowPlaying + "s");

        }
        public static void PrintCaptured(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces");
            Console.Write("Whites: ");
            PrintHashSet(match.capturedPieces(Color.White));
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Blacks: ");
            PrintHashSet(match.capturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            
        }
        public static void PrintHashSet(HashSet<Piece> pieces)
        {
            Console.Write("[");
            foreach(Piece piece in pieces)
            {
                Console.Write(piece + " ");
            }
            Console.WriteLine("]");
        }
        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.Piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void PrintBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBG = Console.BackgroundColor;
            ConsoleColor alteredBG = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = alteredBG;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBG;
                    }
                    PrintPiece(board.Piece(i, j));
                    Console.BackgroundColor = originalBG;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBG;
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece + " ");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece + " ");
                    Console.ForegroundColor = aux;
                }
            }
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }
    }
}
