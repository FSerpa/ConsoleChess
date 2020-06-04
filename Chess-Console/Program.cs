﻿using board;
using chess;
using System;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            board.SetPiece(new Tower(board, Color.Black), new Position(0, 0));
            board.SetPiece(new Tower(board, Color.Black), new Position(1, 3));
            board.SetPiece(new King(board, Color.Black), new Position(0, 2));

            board.SetPiece(new Tower(board, Color.White), new Position(3, 5));
            board.SetPiece(new Tower(board, Color.White), new Position(3, 3));
            board.SetPiece(new King(board, Color.White), new Position(7, 2));

            Screen.PrintBoard(board);
        }
    }
}
