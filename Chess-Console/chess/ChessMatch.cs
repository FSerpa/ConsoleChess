using board;
using System.Security.Cryptography;
using System.Xml;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        private int Turn;
        private Color NowPlaying;
        public bool GameOver { get; private set; }

        public ChessMatch()
        {

            Board = new Board(8, 8);
            Turn = 1;
            NowPlaying = Color.White;
            GameOver = false;
            SetPieces();
        }

        public void PerformMovement(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            
            Piece capturedPiece = Board.RemovePiece(destination);

            Board.SetPiece(piece, destination);
            
            piece.MovesAmountIncrease();
        }

        private void SetPieces() { 
            Board.SetPiece(new Tower(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.SetPiece(new Tower(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.SetPiece(new Tower(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.SetPiece(new Tower(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.SetPiece(new Tower(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.SetPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.SetPiece(new Tower(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.SetPiece(new Tower(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.SetPiece(new Tower(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.SetPiece(new Tower(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.SetPiece(new Tower(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.SetPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }

    }
}
