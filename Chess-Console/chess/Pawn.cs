using board;
using Microsoft.VisualBasic;

namespace chess
{
    class Pawn:Piece
    {
        public Pawn(Board board, Color color):base(board, color)
        {
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
            }
            return vs;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
