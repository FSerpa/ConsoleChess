using board;
using System.Collections.Generic;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color NowPlaying { get; private set; }
        public bool GameOver { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            NowPlaying = Color.White;
            GameOver = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            PlacePieces();
        }

        public void PerformMovement(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            
            Piece capturedPiece = Board.RemovePiece(destination);

            Board.PlacePiece(piece, destination);
            
            piece.MovesAmountIncrease();

            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
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
        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece p in captured)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece p in pieces)
            {
                if (p.Color == color)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        public void PlaceNewPiece(char column, int line, Piece piece) {
            Board.PlacePiece(piece, new ChessPosition(column, line).ToPosition());
            pieces.Add(piece);
        }


        private void PlacePieces() {
            PlaceNewPiece('c', 1, new Tower(Board, Color.White));
            PlaceNewPiece('c', 2, new Tower(Board, Color.White));
            PlaceNewPiece('d', 2, new Tower(Board, Color.White));
            PlaceNewPiece('e', 1, new Tower(Board, Color.White));
            PlaceNewPiece('e', 2, new Tower(Board, Color.White));
            PlaceNewPiece('d', 1, new King(Board, Color.White));

            PlaceNewPiece('c', 8, new Tower(Board, Color.Black));
            PlaceNewPiece('c', 7, new Tower(Board, Color.Black));
            PlaceNewPiece('d', 7, new Tower(Board, Color.Black));
            PlaceNewPiece('e', 8, new Tower(Board, Color.Black));
            PlaceNewPiece('e', 7, new Tower(Board, Color.Black));
            PlaceNewPiece('d', 8, new King(Board, Color.Black));
        }

    }
}
