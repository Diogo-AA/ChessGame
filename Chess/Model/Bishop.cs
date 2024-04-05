using System.Windows.Input;
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
            for (int newRow = Row + 1; newRow < Board.ROWS_LENGTH; newRow++)
            {
                int newCol = Col + (newRow - Row);

                if (Board.IsPositionOnTheBoardLimits(board, newRow, newCol) && rightDiagonalPossible)
                {
                    if (!Board.IsSquareAlly(board, newRow, newCol, Color) && !Game.IsPiecePinned(board, [Row, Col], [newRow, newCol]))
                        moves.Add([newRow, newCol]);

                    if (!Board.IsSquareEmpty(board, newRow, newCol))
                        rightDiagonalPossible = false;
                }

                newCol = Col - (newRow - Row);
                if (Board.IsPositionOnTheBoardLimits(board, newRow, newCol) && leftDiagonalPossible)
                {
                    if (!Board.IsSquareAlly(board, newRow, newCol, Color))
                        moves.Add([newRow, newCol]);

                    if (!Board.IsSquareEmpty(board, newRow, newCol))
                        leftDiagonalPossible = false;
                }
            }

            rightDiagonalPossible = true;
            leftDiagonalPossible = true;

            // Check for the diagonals above the bishop
            for (int newRow = Row - 1; newRow >= 0; newRow--)
            {
                if (Board.IsPositionOnTheBoardLimits(board, newRow, Col + (Row - newRow)) && rightDiagonalPossible)
                {
                    if (!Board.IsSquareAlly(board, newRow, Col + (Row - newRow), Color))
                        moves.Add([newRow, Col + (Row - newRow)]);

                    if (!Board.IsSquareEmpty(board, newRow, Col + (Row - newRow)))
                        rightDiagonalPossible = false;
                }

                if (Board.IsPositionOnTheBoardLimits(board, newRow, Col - (Row - newRow)) && leftDiagonalPossible)
                {
                    if (!Board.IsSquareAlly(board, newRow, Col - (Row - newRow), Color))
                        moves.Add([newRow, Col - (Row - newRow)]);

                    if (!Board.IsSquareEmpty(board, newRow, Col - (Row - newRow)))
                        leftDiagonalPossible = false;
                }
            }

            return moves;
        }

        public List<int[]> GetAllCheckBlocks(IPiece?[][] board, List<King.Attacker> attackers)
        {
            var moves = new List<int[]>();

            if (attackers.Count == 2)
                return moves;

            foreach (var pos in attackers[0].SquaresToBeBlocked)
            {
                bool rightDiagonalPossible = true;
                bool leftDiagonalPossible = true;

                // Check for the diagonals bellow the queen
                for (int newRow = Row + 1; newRow < Board.ROWS_LENGTH; newRow++)
                {
                    int newCol = Col + (newRow - Row);
                    if (Board.IsPositionOnTheBoardLimits(board, newRow, Col + (newRow - Row)) && rightDiagonalPossible)
                    {
                        if (Board.IsSquareAlly(board, newRow, Col + (newRow - Row), Color))
                            break;

                        if (pos[0] == newRow && pos[1] == Col + (newRow - Row) && !Game.IsPiecePinned(board, [Row, Col], [newRow, newCol]))
                            moves.Add([newRow, Col + (newRow - Row)]);

                        if (!Board.IsSquareEmpty(board, newRow, Col + (newRow - Row)))
                            rightDiagonalPossible = false;
                    }
                }

                for (int newRow = Row + 1; newRow < Board.ROWS_LENGTH; newRow++)
                {
                    int newCol = Col - (newRow - Row);
                    if (Board.IsPositionOnTheBoardLimits(board, newRow, Col - (newRow - Row)) && leftDiagonalPossible)
                    {
                        if (Board.IsSquareAlly(board, newRow, Col - (newRow - Row), Color))
                            break;

                        if (pos[0] == newRow && pos[1] == Col - (newRow - Row) && !Game.IsPiecePinned(board, [Row, Col], [newRow, newCol]))
                            moves.Add([newRow, Col - (newRow - Row)]);

                        if (!Board.IsSquareEmpty(board, newRow, Col - (newRow - Row)))
                            leftDiagonalPossible = false;
                    }
                }

                rightDiagonalPossible = true;
                leftDiagonalPossible = true;

                // Check for the diagonals above the queen
                for (int newRow = Row - 1; newRow >= 0; newRow--)
                {
                    int newCol = Col + (Row - newRow);
                    if (Board.IsPositionOnTheBoardLimits(board, newRow, Col + (Row - newRow)) && rightDiagonalPossible)
                    {
                        if (Board.IsSquareAlly(board, newRow, Col + (Row - newRow), Color))
                            break;

                        if (pos[0] == newRow && pos[1] == Col + (Row - newRow) && !Game.IsPiecePinned(board, [Row, Col], [newRow, newCol]))
                            moves.Add([newRow, Col + (Row - newRow)]);

                        if (!Board.IsSquareEmpty(board, newRow, Col + (Row - newRow)))
                            rightDiagonalPossible = false;
                    }
                }

                for (int newRow = Row - 1; newRow >= 0; newRow--)
                {
                    int newCol = Col - (Row - newRow);
                    if (Board.IsPositionOnTheBoardLimits(board, newRow, Col - (Row - newRow)) && leftDiagonalPossible)
                    {
                        if (Board.IsSquareAlly(board, newRow, Col - (Row - newRow), Color))
                            break;

                        if (pos[0] == newRow && pos[1] == Col - (Row - newRow) && !Game.IsPiecePinned(board, [Row, Col], [newRow, newCol]))
                            moves.Add([newRow, Col - (Row - newRow)]);

                        if (!Board.IsSquareEmpty(board, newRow, Col - (Row - newRow)))
                            leftDiagonalPossible = false;
                    }
                }
            }

            return moves;
        }

        public void MovePiece(int newRow, int newCol)
        {
            Row = newRow;
            Col = newCol;
        }

        public override string ToString()
        {
            return $"{Color}-{Notation}-{Row}-{Col}";
        }
    }
}
