﻿using board;
using System;

namespace Chess_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            Screen.printBoard(board);

        }
    }
}
