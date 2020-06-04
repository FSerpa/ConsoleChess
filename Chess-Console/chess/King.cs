using board;

namespace chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
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
            if(Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            //ne
            position.DefinePosition(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            //e
            position.DefinePosition(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            //se
            position.DefinePosition(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            //s
            position.DefinePosition(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            //sw
            position.DefinePosition(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            //w
            position.DefinePosition(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            //nw
            position.DefinePosition(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            return vs;
        }
        public override string ToString()
        {
            return "K";
        }
    }
}
