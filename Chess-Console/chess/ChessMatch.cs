using board;
using System.Security.Cryptography;
using System.Threading;
using System.Xml;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color NowPlaying { get; private set; }
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

        public void PlayNow(Position origin, Position destination)
        {
            PerformMovement(origin, destination);
            if (NowPlaying == Color.Black)
            {
                Turn++;
            }
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.Piece(position) == null)
            {
                throw new BoardException("There is no piece in this position.");
            }
            if(NowPlaying != Board.Piece(position).Color)
            {
                throw new BoardException("This piece belongs to the other player.");
            }
            if (!Board.Piece(position).BoolPossibleMovements())
            {
                throw new BoardException("This piece is blocked.");
            }
            
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanMoveTo(destination))
            {
                throw new BoardException("Invalid destination.");
            }
        }

        private void ChangePlayer()
        {
            if(NowPlaying == Color.White)
            {
                NowPlaying = Color.Black;
            }
            else
            {
                NowPlaying = Color.White;
            }
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
