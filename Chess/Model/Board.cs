using static Chess.Model.IPiece;

namespace Chess.Model
{
    internal class Board
    {
        public const int ROWS_LENGTH = 8;
        public const int COLS_LENGTH = 8;

        public static void MovePiece(IPiece?[][] board, int row, int col, int newRow, int newCol)
        {
            board[newRow][newCol] = board[row][col];
            board[row][col] = null;
            board[newRow][newCol]?.MovePiece(newRow, newCol);
        }

        public static bool IsSquareEmpty(IPiece?[][] board, int row, int col)
        {
            return board[row][col] is null || board[row][col]?.Type == Pieces.None;
        }

        public static bool IsSquareEnemy(IPiece?[][] board, int row, int col, Colors? color)
        {
            return board[row][col] is not null && board[row][col]?.Color != color && board[row][col]?.Type != Pieces.None;
        }

        public static bool IsSquareAlly(IPiece?[][] board, int row, int col, Colors? color)
        {
            return board[row][col] is not null && board[row][col]?.Color == color && board[row][col]?.Type != Pieces.None;
        }

        public static bool IsPositionOnTheBoardLimits(IPiece?[][] board, int row, int col)
        {
            return row >= 0 && row < Board.ROWS_LENGTH && col >= 0 && col < Board.COLS_LENGTH;
        }
    }
}
