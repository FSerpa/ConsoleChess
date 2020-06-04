using board;
using Microsoft.VisualBasic;

namespace chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(board, color)
        {
        }
        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }
        public override bool[,] PossibleMovements()
        {
            bool[,] vs = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            //n
            position.DefinePosition(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
                if(Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Line = position.Line - 1;
            }
            //s
            position.DefinePosition(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Line = position.Line + 1;
            }
            //e
            position.DefinePosition(Position.Line, Position.Column+1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Column = position.Column + 1;
            }
            //w
            position.DefinePosition(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Column = position.Column - 1;
            }

            return vs;
        }
        public override string ToString()
        {
            return "T";
        }
    }
}
