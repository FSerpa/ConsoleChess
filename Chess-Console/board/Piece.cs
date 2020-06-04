using System.Text.RegularExpressions;

namespace board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int MovesAmount { get; protected set; }
        public Board Board { get; protected set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Board = board;
            Color = color;
            MovesAmount = 0;
        }

        public void MovesAmountIncrease()
        {
            MovesAmount++;
        }
        public void MovesAmountDecrease()
        {
            MovesAmount--;
        }

        public bool BoolPossibleMovements()
        {
            bool[,] vs = PossibleMovements();
            for (int i=0; i<Board.Lines; i++)
            {
                for(int j=0; j<Board.Columns; j++)
                {
                    if (vs[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
            
        }

        public bool CanMoveTo(Position position)
        {
            return PossibleMovements()[position.Line, position.Column];
        }

        public abstract bool[,] PossibleMovements();
    }
}
