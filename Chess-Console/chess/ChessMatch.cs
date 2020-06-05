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
        public Piece EnPassantVul { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            NowPlaying = Color.White;
            GameOver = false;
            EnPassantVul = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            PlacePieces();
        }
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
            //#Short Castling
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position rOrigin = new Position(origin.Line, origin.Column + 3);
                Position rDestination = new Position(origin.Line, origin.Column + 1);
                Piece R = Board.RemovePiece(rOrigin);
                R.MovesAmountIncrease();
                Board.PlacePiece(R, rDestination);
            }

            //#Long Castling
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position rOrigin = new Position(origin.Line, origin.Column - 4);
                Position rDestination = new Position(origin.Line, origin.Column - 1);
                Piece R = Board.RemovePiece(rOrigin);
                R.MovesAmountIncrease();
                Board.PlacePiece(R, rDestination);
            }

            //enPassant
            if(piece is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == null)
                {
                    Position pawnP;
                    if(piece.Color == Color.White)
                    {
                        pawnP = new Position(destination.Line + 1, destination.Column);
                    }
                    else
                    {
                        pawnP = new Position(destination.Line - 1, destination.Column);
                    }
                    capturedPiece = Board.RemovePiece(pawnP);
                    captured.Add(capturedPiece);
                }
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

            //#Short Castling
            if (piece is King && destination.Column == origin.Column + 2)
            {
                Position rOrigin = new Position(origin.Line, origin.Column + 3);
                Position rDestination = new Position(origin.Line, origin.Column + 1);
                Piece R = Board.RemovePiece(rDestination);
                R.MovesAmountIncrease();
                Board.PlacePiece(R, rOrigin);
            }

            //#Long Castling
            if (piece is King && destination.Column == origin.Column - 2)
            {
                Position rOrigin = new Position(origin.Line, origin.Column - 4);
                Position rDestination = new Position(origin.Line, origin.Column - 1);
                Piece R = Board.RemovePiece(rDestination);
                R.MovesAmountIncrease();
                Board.PlacePiece(R, rOrigin);
            }
            if (piece is Pawn)
            {
                if (origin.Column != destination.Column && capturedPiece == EnPassantVul)
                {
                    Piece pawn = Board.RemovePiece(destination);
                    Position pawnP;
                    if (piece.Color == Color.White)
                    {
                        pawnP = new Position(3, destination.Column);
                    }
                    else
                    {
                        pawnP = new Position(4, destination.Column);
                    }
                    Board.PlacePiece(pawn, pawnP);
                }
            }
        }

        public void PlayNow(Position origin, Position destination)
        {
            Piece capturedPiece = PerformMovement(origin, destination);
            if (InCheck(NowPlaying))
            {
                UndoMovement(origin, destination, capturedPiece);
                throw new BoardException("You can't put yourself in Check condition");
            }
            Piece p = Board.Piece(destination);

            //promotion

            if (p is Pawn)
            {
                if ((p.Color == Color.White && destination.Line == 0) || (p.Color == Color.Black && destination.Line == 7))
                {
                    p = Board.RemovePiece(destination);
                    pieces.Remove(p);
                    Piece queen = new Queen(Board, p.Color);
                    Board.PlacePiece(queen, destination);
                    pieces.Add(queen);
                }
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
            

            //enPassant
            if(p is Pawn && destination.Line == origin.Line -2 || destination.Line == origin.Line + 2)
            {
                EnPassantVul = p;
            }
            else
            {
                EnPassantVul = null;
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
 
            PlaceNewPiece('a', 1, new Rook(Board, Color.White));
            PlaceNewPiece('b', 1, new Knight(Board, Color.White));
            PlaceNewPiece('c', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('d', 1, new Queen(Board, Color.White));
            PlaceNewPiece('e', 1, new King(Board, Color.White, this));
            PlaceNewPiece('f', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('g', 1, new Knight(Board, Color.White));
            PlaceNewPiece('h', 1, new Rook(Board, Color.White));
            PlaceNewPiece('a', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('b', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('c', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('d', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('e', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('f', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('g', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('h', 2, new Pawn(Board, Color.White, this));

            PlaceNewPiece('a', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('b', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('c', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('d', 8, new Queen(Board, Color.Black));
            PlaceNewPiece('e', 8, new King(Board, Color.Black, this));
            PlaceNewPiece('f', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('g', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('h', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }

    }
}
