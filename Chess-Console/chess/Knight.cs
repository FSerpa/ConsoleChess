using board;

namespace chess
{
    class Knight:Piece
    {
        public Knight(Board board, Color color) : base(board, color)
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
            
            position.DefinePosition(Position.Line - 2, Position.Column-1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            
            position.DefinePosition(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            
            position.DefinePosition(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            
            position.DefinePosition(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            
            position.DefinePosition(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            
            position.DefinePosition(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            
            position.DefinePosition(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            
            position.DefinePosition(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMove(position))
            {
                vs[position.Line, position.Column] = true;
            }
            return vs;
        }
        public override string ToString()
        {
            return "N";
        }
    }
}
