using static Chess.Model.IPiece;

namespace Chess.Model
{
    internal class Game
    {
        public IPiece?[][] board;
        public List<int> possibleMoves = new List<int>();
        public IPiece? PieceSelected { get; set; } = null;
        public Colors Turn { get; set; } = Colors.White;

        public Game()
        {
            board = new IPiece[Board.ROWS_LENGTH][];

            /*board[0] = [
                new Rook(Colors.White, 0, 0),
                new Knight(Colors.White, 0, 1),
                new Bishop(Colors.White, 0, 2),
                new Queen(Colors.White, 0, 3),
                new King(Colors.White, 0, 4),
                new Bishop(Colors.White, 0, 5),
                new Knight(Colors.White, 0, 6),
                new Rook(Colors.White, 0, 7)
            ];*/

            board[0] = [
                new Pawn(Colors.White, 0, 0),
                new Pawn(Colors.White, 0, 1),
                new Pawn(Colors.White, 0, 2),
                new Pawn(Colors.White, 0, 3),
                new Pawn(Colors.White, 0, 4),
                new Pawn(Colors.White, 0, 5),
                new Pawn(Colors.White, 0, 6),
                new Pawn(Colors.White, 0, 7)
            ];

            board[1] = [
                new Pawn(Colors.White, 1, 0), 
                new Pawn(Colors.White, 1, 1), 
                new Pawn(Colors.White, 1, 2), 
                new Pawn(Colors.White, 1, 3),
                new Pawn(Colors.White, 1, 4), 
                new Pawn(Colors.White, 1, 5), 
                new Pawn(Colors.White, 1, 6), 
                new Pawn(Colors.White, 1, 7)
            ];

            for (int i = 2; i < Board.ROWS_LENGTH - 2; i++)
            {
                board[i] = [null, null, null, null, null, null, null, null];
            }

            board[6] = [
                new Pawn(Colors.Black, 6, 0),
                new Pawn(Colors.Black, 6, 1),
                new Pawn(Colors.Black, 6, 2),
                new Pawn(Colors.Black, 6, 3),
                new Pawn(Colors.Black, 6, 4),
                new Pawn(Colors.Black, 6, 5),
                new Pawn(Colors.Black, 6, 6),
                new Pawn(Colors.Black, 6, 7)
            ];

            /*board[7] = [
                new Rook(Colors.Black, 7, 0),
                new Knight(Colors.Black, 7, 1),
                new Bishop(Colors.Black, 7, 2),
                new Queen(Colors.Black, 7, 3),
                new King(Colors.Black, 7, 4),
                new Bishop(Colors.Black, 7, 5),
                new Knight(Colors.Black, 7, 6),
                new Rook(Colors.Black, 7, 7)
            ];*/

            board[7] = [
                new Pawn(Colors.Black, 7, 0),
                new Pawn(Colors.Black, 7, 1),
                new Pawn(Colors.Black, 7, 2),
                new Pawn(Colors.Black, 7, 3),
                new Pawn(Colors.Black, 7, 4),
                new Pawn(Colors.Black, 7, 5),
                new Pawn(Colors.Black, 7, 6),
                new Pawn(Colors.Black, 7, 7)
            ];
        }


    }
}
