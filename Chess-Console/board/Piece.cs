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

        public abstract bool[,] PossibleMovements();
    }
}
