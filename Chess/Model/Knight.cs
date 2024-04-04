using static Chess.Model.IPiece;

namespace Chess.Model
{
    internal class Knight : IPiece
    {
        public string Name { get; set; } = "Knight";
        public string Notation { get; set; } = "N";
        public Pieces Type { get; set; } = IPiece.Pieces.Knight;
        public Colors Color { get; set; }
        public bool IsBeingAttacked { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Knight(Colors color, int row, int col)
        {
            Color = color;
            Row = row;
            Col = col;
        }

        public List<int[]> GetAllMoves(IPiece?[][] board)
        {
            var moves = new List<int[]>();

            if (Board.IsPositionOnTheBoardLimits(board, Row + 2, Col - 1) && !Board.IsSquareAlly(board, Row + 2, Col - 1, Color))
                moves.Add([Row + 2, Col - 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row + 2, Col + 1) && !Board.IsSquareAlly(board, Row + 2, Col + 1, Color))
                moves.Add([Row + 2, Col + 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 2, Col + 1) && !Board.IsSquareAlly(board, Row - 2, Col + 1, Color))
                moves.Add([Row - 2, Col + 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 2, Col - 1) && !Board.IsSquareAlly(board, Row - 2, Col - 1, Color))
                moves.Add([Row - 2, Col - 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col + 2) && !Board.IsSquareAlly(board, Row + 1, Col + 2, Color))
                moves.Add([Row + 1, Col + 2]);

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col - 2) && !Board.IsSquareAlly(board, Row + 1, Col - 2, Color))
                moves.Add([Row + 1, Col - 2]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col + 2) && !Board.IsSquareAlly(board, Row - 1, Col + 2, Color))
                moves.Add([Row - 1, Col + 2]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col - 2) && !Board.IsSquareAlly(board, Row - 1, Col - 2, Color))
                moves.Add([Row - 1, Col - 2]);

            return moves;
        }

        public void MovePiece(int newRow, int newCol)
        {
            Row = newRow;
            Col = newCol;
        }
    }
}
