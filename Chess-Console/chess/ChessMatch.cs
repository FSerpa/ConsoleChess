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
        public bool Check { get; private set; }

        private Color Opponent(Color color)
        {
            if(color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece King(Color color)
        {
            foreach(Piece piece in piecesInGame(color))
            {
                if(piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool InCheck(Color color)
        {
            Piece K = King(color);
            if (K == null)
            {
                throw new BoardException("There is no King of this color in the board.");
            }
            foreach (Piece piece in piecesInGame(Opponent(color)))
            {
                bool[,] vs = piece.PossibleMovements();
                if (vs[K.Position.Line, K.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }
        public bool InCheckMate(Color color)
        {
            if (!InCheck(color))
            {
                return false;
            }
            foreach(Piece piece in piecesInGame(color))
            {
                bool[,] vs = piece.PossibleMovements();
                for(int i=0; i < Board.Lines; i++)
                {
                    for(int j=0; j < Board.Columns; j++)
                    {
                        if (vs[i, j])
                        {
                            Position origin = piece.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = PerformMovement(origin, destination);
                            bool checkTest = InCheck(color);
                            UndoMovement(origin, destination, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
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

        public Piece PerformMovement(Position origin, Position destination)
        {
            Piece piece = Board.RemovePiece(origin);
            
            Piece capturedPiece = Board.RemovePiece(destination);

            Board.PlacePiece(piece, destination);
            
            piece.MovesAmountIncrease();

            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
            return capturedPiece;
        }
        
        public void UndoMovement(Position origin, Position destination, Piece capturedPiece)
        {
            Piece piece = Board.RemovePiece(destination);
            if (capturedPiece != null)
            {
                Board.PlacePiece(capturedPiece, destination);
                captured.Remove(capturedPiece);
            }
            Board.PlacePiece(piece, origin);
            piece.MovesAmountDecrease();
        }

        public void PlayNow(Position origin, Position destination)
        {
            Piece capturedPiece = PerformMovement(origin, destination);
            if (InCheck(NowPlaying))
            {
                UndoMovement(origin, destination, capturedPiece);
                throw new BoardException("You can't put yourself in Check condition");
            }
            if (InCheck(Opponent(NowPlaying)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }
            if (InCheckMate(Opponent(NowPlaying))){
                GameOver = true;
            }
            else {
                if (NowPlaying == Color.Black)
                {
                    Turn++;
                }
                ChangePlayer();
            }
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
 
            PlaceNewPiece('h', 7, new Tower(Board, Color.White));
            PlaceNewPiece('c', 1, new Tower(Board, Color.White));
            PlaceNewPiece('h', 1, new King(Board, Color.White));

            PlaceNewPiece('b', 8, new Tower(Board, Color.Black));
            PlaceNewPiece('a', 8, new King(Board, Color.Black));
        }

    }
}
