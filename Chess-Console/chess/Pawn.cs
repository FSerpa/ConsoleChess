using board;

namespace chess
{
    class Pawn:Piece
    {
        private ChessMatch Match;
        public Pawn(Board board, Color color,ChessMatch match):base(board, color)
        {
            Match = match;
        }
        private bool EnemyChecking(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }
        private bool FreeSpot(Position position)
        {
            return Board.Piece(position) == null;
        }
        public override bool[,] PossibleMovements()
        {
            bool[,] vs = new bool[Board.Lines, Board.Columns];

            Position position = new Position(0, 0);

            if (Color == Color.White)
            {
                position.DefinePosition(Position.Line - 1, Position.Column);
                if(Board.ValidPosition(position)&& FreeSpot(position))
                {
                    vs[position.Line, position.Column] = true;
                }
                position.DefinePosition(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(position) && FreeSpot(position) && MovesAmount == 0)
                {
                    vs[position.Line, position.Column] = true;
                }
                position.DefinePosition(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(position) && EnemyChecking(position))
                {
                    vs[position.Line, position.Column] = true;
                }
                position.DefinePosition(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(position) && EnemyChecking(position))
                {
                    vs[position.Line, position.Column] = true;
                }

                //enPassant
                if(Position.Line == 3)
                {
                    Position west = new Position(Position.Line, Position.Column - 1);
                    if(Board.ValidPosition(west)&&EnemyChecking(west)&&Board.Piece(west) == Match.EnPassantVul)
                    {
                        vs[west.Line-1, west.Column] = true;
                    }
                }
                if (Position.Line == 3)
                {
                    Position east = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(east) && EnemyChecking(east) && Board.Piece(east) == Match.EnPassantVul)
                    {
                        vs[east.Line-1, east.Column] = true;
                    }
                }
            }
            else
            {
                position.DefinePosition(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(position) && FreeSpot(position))
                {
                    vs[position.Line, position.Column] = true;
                }
                position.DefinePosition(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(position) && FreeSpot(position) && MovesAmount == 0)
                {
                    vs[position.Line, position.Column] = true;
                }
                position.DefinePosition(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(position) && EnemyChecking(position))
                {
                    vs[position.Line, position.Column] = true;
                }
                position.DefinePosition(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(position) && EnemyChecking(position))
                {
                    vs[position.Line, position.Column] = true;
                }
                //enPassant
                if (Position.Line == 4)
                {
                    Position west = new Position(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(west) && EnemyChecking(west) && Board.Piece(west) == Match.EnPassantVul)
                    {
                        vs[west.Line+1, west.Column] = true;
                    }
                }
                if (Position.Line == 4)
                {
                    Position east = new Position(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(east) && EnemyChecking(east) && Board.Piece(east) == Match.EnPassantVul)
                    {
                        vs[east.Line+1, east.Column] = true;
                    }
                }
            }
            return vs;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
