using static Chess.Model.IPiece;

namespace Chess.Model
{
    internal class Bishop : IPiece
    {
        public string Name { get; set; } = "Bishop";
        public string Notation { get; set; } = "B";
        public Pieces Type { get; set; } = IPiece.Pieces.Bishop;
        public Colors Color { get; set; }
        public bool IsBeingAttacked { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Bishop(Colors color, int row, int col)
        {
            Color = color;
            Row = row;
            Col = col;
        }

        public List<int[]> GetAllMoves(IPiece?[][] board)
        {
            var moves = new List<int[]>();
            
            bool rightDiagonalPossible = true;
            bool leftDiagonalPossible = true;

            // Check for the diagonals bellow the bishop
            for (int i = Row + 1; i < Board.ROWS_LENGTH; i++)
            {
                if (Board.IsPositionOnTheBoardLimits(board, i, Col + (i - Row)) && rightDiagonalPossible)
                {
                    if (!Board.IsSquareAlly(board, i, Col + (i - Row), Color))
                        moves.Add([i, Col + (i - Row)]);

                    if (!Board.IsSquareEmpty(board, i, Col + (i - Row)))
                        rightDiagonalPossible = false;
                }

                if (Board.IsPositionOnTheBoardLimits(board, i, Col - (i - Row)) && leftDiagonalPossible)
                {
                    if (!Board.IsSquareAlly(board, i, Col - (i - Row), Color))
                        moves.Add([i, Col - (i - Row)]);

                    if (!Board.IsSquareEmpty(board, i, Col - (i - Row)))
                        rightDiagonalPossible = false;
                }
            }

            rightDiagonalPossible = true;
            leftDiagonalPossible = true;

            // Check for the diagonals above the bishop
            for (int i = Row - 1; i > 0; i--)
            {
                if (Board.IsPositionOnTheBoardLimits(board, i, Col + (Row - i)) && rightDiagonalPossible)
                {
                    if (!Board.IsSquareAlly(board, i, Col + (Row - i), Color))
                        moves.Add([i, Col + (Row - i)]);

                    if (!Board.IsSquareEmpty(board, i, Col + (Row - i)))
                        rightDiagonalPossible = false;
                }

                if (Board.IsPositionOnTheBoardLimits(board, i, Col - (Row - i)) && leftDiagonalPossible)
                {
                    if (!Board.IsSquareAlly(board, i, Col - (Row - i), Color))
                        moves.Add([i, Col - (Row - i)]);

                    if (!Board.IsSquareEmpty(board, i, Col - (Row - i)))
                        leftDiagonalPossible = false;
                }
            }

            return moves;
        }

        public void MovePiece(int newRow, int newCol)
        {
            Row = newRow;
            Col = newCol;
        }
    }
}
