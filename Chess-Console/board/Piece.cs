﻿namespace board
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovesAmount { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Position position, Board board, Color color)
        {
            Position = position;
            Board = board;
            Color = color;
            MovesAmount = 0;
        }

    }
}
