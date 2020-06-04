using board;

namespace chess
{
    class Bishop:Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
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

            //ne
            position.DefinePosition(Position.Line - 1, Position.Column+1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Line = position.Line - 1;
                position.Column = position.Column + 1;
            }
            //se
            position.DefinePosition(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Line = position.Line + 1;
                position.Column = position.Column + 1;
            }
            //sw
            position.DefinePosition(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Line = position.Line + 1;
                position.Column = position.Column - 1;
            }
            //nw
            position.DefinePosition(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Line = position.Line - 1;
                position.Column = position.Column - 1;
            }

            return vs;
        }
        public override string ToString()
        {
            return "B";
        }
    }
}

