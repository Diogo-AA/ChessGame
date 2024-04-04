using static Chess.Model.IPiece;

namespace Chess.Model
{
    internal class King : IPiece
    {
        public string Name { get; set; } = "King";
        public string Notation { get; set; } = "K";
        public Pieces Type { get; set; } = IPiece.Pieces.King;
        public Colors Color { get; set; }
        public bool IsBeingAttacked { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public King(Colors color, int row, int col)
        {
            Color = color;
            Row = row;
            Col = col;
        }

        public List<int[]> GetAllMoves(IPiece?[][] board)
        {
            var moves = new List<int[]>();

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col - 1) && !Board.IsSquareAlly(board, Row + 1, Col - 1, Color))
                moves.Add([Row + 1, Col - 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col) && !Board.IsSquareAlly(board, Row + 1, Col, Color))
                moves.Add([Row + 1, Col]);

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col + 1) && !Board.IsSquareAlly(board, Row + 1, Col + 1, Color))
                moves.Add([Row + 1, Col + 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row, Col - 1) && !Board.IsSquareAlly(board, Row, Col - 1, Color))
                moves.Add([Row, Col - 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row, Col + 1) && !Board.IsSquareAlly(board, Row, Col + 1, Color))
                moves.Add([Row, Col + 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col - 1) && !Board.IsSquareAlly(board, Row - 1, Col - 1, Color))
                moves.Add([Row - 1, Col - 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col) && !Board.IsSquareAlly(board, Row - 1, Col, Color))
                moves.Add([Row - 1, Col]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col + 1) && !Board.IsSquareAlly(board, Row - 1, Col + 1, Color))
                moves.Add([Row - 1, Col + 1]);

            return moves;
        }

        public void MovePiece(int newRow, int newCol)
        {
            Row = newRow;
            Col = newCol;
        }
    }
}
