using board;

namespace chess
{
    class King : Piece
    {
        private ChessMatch Match;
        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            Match = match;
        }

        private bool CanMove(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        private bool IsCastlingRook(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece is Rook && piece.Color == Color && piece.MovesAmount == 0;
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
            //#special movement Short Castling
            if(MovesAmount == 0 && !Match.Check)
            {
                Position RookPosition = new Position(Position.Line, Position.Column + 3);
                if (IsCastlingRook(RookPosition))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);

                    if(Board.Piece(p1) == null && Board.Piece(p2) == null)
                    {
                        vs[Position.Line, Position.Column + 2] = true;
                    }
                }

            }
            //#special movement Long Castling
            if (MovesAmount == 0 && !Match.Check)
            {
                Position RookPosition = new Position(Position.Line, Position.Column - 4);
                if (IsCastlingRook(RookPosition))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (Board.Piece(p1) == null && Board.Piece(p2) == null && Board.Piece(p3)==null)
                    {
                        vs[Position.Line, Position.Column - 2] = true;
                    }
                }
            }


            return vs;
        }
        public override string ToString()
        {
            return "K";
        }
    }
}
