using static Chess.Model.IPiece;

namespace Chess.Model
{
    internal class Queen : IPiece
    {
        public string Name { get; set; } = "Queen";
        public string Notation { get; set; } = "Q";
        public Pieces Type { get; set; } = IPiece.Pieces.Queen;
        public Colors Color { get; set; }
        public bool IsBeingAttacked { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Queen(Colors color, int row, int col)
        {
            Color = color;
            Row = row;
            Col = col;
        }

        public List<int[]> GetAllMoves(IPiece?[][] board)
        {
            var moves = new List<int[]>();
            
            // Check for vertical moves
            for (int newRow = Row + 1; newRow < Board.ROWS_LENGTH; newRow++)
            {
                if (Board.IsPositionOnTheBoardLimits(board, newRow, Col))
                {
                    if (!Board.IsSquareAlly(board, newRow, Col, Color) && !Game.IsPiecePinned(board, [Row, Col], [newRow, Col]))
                        moves.Add([newRow, Col]);

                    if (!Board.IsSquareEmpty(board, newRow, Col))
                        break;
                }
            }

            for (int newRow = Row - 1; newRow >= 0; newRow--)
            {
                if (Board.IsPositionOnTheBoardLimits(board, newRow, Col))
                {
                    if (!Board.IsSquareAlly(board, newRow, Col, Color) && !Game.IsPiecePinned(board, [Row, Col], [newRow, Col]))
                        moves.Add([newRow, Col]);

                    if (!Board.IsSquareEmpty(board, newRow, Col))
                        break;
                }
            }

            // Check for horizontal moves
            for (int newCol = Col + 1; newCol < Board.COLS_LENGTH; newCol++)
            {
                if (Board.IsPositionOnTheBoardLimits(board, Row, newCol))
                {
                    if (!Board.IsSquareAlly(board, Row, newCol, Color) && !Game.IsPiecePinned(board, [Row, Col], [Row, newCol]))
                        moves.Add([Row, newCol]);

                    if (!Board.IsSquareEmpty(board, Row, newCol))
                        break;
                }
            }

            for (int newCol = Col - 1; newCol >= 0; newCol--)
            {
                if (Board.IsPositionOnTheBoardLimits(board, Row, newCol))
                {
                    if (!Board.IsSquareAlly(board, Row, newCol, Color) && !Game.IsPiecePinned(board, [Row, Col], [Row, newCol]))
                        moves.Add([Row, newCol]);

                    if (!Board.IsSquareEmpty(board, Row, newCol))
                        break;
                }
            }

            bool rightDiagonalPossible = true;
            bool leftDiagonalPossible = true;

            // Check for the diagonals bellow the queen
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
                    if (!Board.IsSquareAlly(board, newRow, newCol, Color) && !Game.IsPiecePinned(board, [Row, Col], [newRow, newCol]))
                        moves.Add([newRow, newCol]);

                    if (!Board.IsSquareEmpty(board, newRow, newCol))
                        leftDiagonalPossible = false;
                }
            }

            rightDiagonalPossible = true;
            leftDiagonalPossible = true;

            // Check for the diagonals above the queen
            for (int newRow = Row - 1; newRow >= 0; newRow--)
            {
                int newCol = Col + (Row - newRow);
                if (Board.IsPositionOnTheBoardLimits(board, newRow, newCol) && rightDiagonalPossible)
                {
                    if (!Board.IsSquareAlly(board, newRow, newCol, Color) && !Game.IsPiecePinned(board, [Row, Col], [newRow, newCol]))
                        moves.Add([newRow, newCol]);

                    if (!Board.IsSquareEmpty(board, newRow, newCol))
                        rightDiagonalPossible = false;
                }

                newCol = Col - (Row - newRow);
                if (Board.IsPositionOnTheBoardLimits(board, newRow, newCol) && leftDiagonalPossible)
                {
                    if (!Board.IsSquareAlly(board, newRow, newCol, Color) && !Game.IsPiecePinned(board, [Row, Col], [newRow, newCol]))
                        moves.Add([newRow, newCol]);

                    if (!Board.IsSquareEmpty(board, newRow, newCol))
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
                // Check for vertical moves
                for (int newRow = Row + 1; newRow < Board.ROWS_LENGTH; newRow++)
                {
                    if (Board.IsPositionOnTheBoardLimits(board, newRow, Col))
                    {
                        if (Board.IsSquareAlly(board, newRow, Col, Color) && !Game.IsPiecePinned(board, [Row, Col], [newRow, Col]))
                            break;

                        if (pos[0] == newRow && pos[1] == Col)
                            moves.Add([newRow, Col]);
                    }
                }

                for (int newRow = Row - 1; newRow >= 0; newRow--)
                {
                    if (Board.IsPositionOnTheBoardLimits(board, newRow, Col))
                    {
                        if (Board.IsSquareAlly(board, newRow, Col, Color) && !Game.IsPiecePinned(board, [Row, Col], [newRow, Col]))
                            break;

                        if (pos[0] == newRow && pos[1] == Col)
                            moves.Add([newRow, Col]);
                    }
                }

                // Check for horizontal moves
                for (int newCol = Col + 1; newCol < Board.COLS_LENGTH; newCol++)
                {
                    if (Board.IsPositionOnTheBoardLimits(board, Row, newCol))
                    {
                        if (Board.IsSquareAlly(board, Row, newCol, Color) && !Game.IsPiecePinned(board, [Row, Col], [Row, newCol]))
                            break;

                        if (pos[0] == Row && pos[1] == newCol)
                            moves.Add([Row, newCol]);
                    }
                }

                for (int newCol = Col - 1; newCol >= 0; newCol--)
                {
                    if (Board.IsPositionOnTheBoardLimits(board, Row, newCol))
                    {
                        if (Board.IsSquareAlly(board, Row, newCol, Color) && !Game.IsPiecePinned(board, [Row, Col], [Row, newCol]))
                            break;

                        if (pos[0] == Row && pos[1] == newCol)
                            moves.Add([Row, newCol]);
                    }
                }

                bool rightDiagonalPossible = true;
                bool leftDiagonalPossible = true;

                // Check for the diagonals bellow the queen
                for (int newRow = Row + 1; newRow < Board.ROWS_LENGTH; newRow++)
                {
                    int newCol = Col + (newRow - Row);
                    if (Board.IsPositionOnTheBoardLimits(board, newRow, newCol) && rightDiagonalPossible)
                    {
                        if (Board.IsSquareAlly(board, newRow, newCol, Color))
                            break;

                        if (pos[0] == newRow && pos[1] == newCol && !Game.IsPiecePinned(board, [Row, Col], [newRow, newCol]))
                            moves.Add([newRow, newCol]);

                        if (!Board.IsSquareEmpty(board, newRow, newCol))
                            rightDiagonalPossible = false;
                    }
                }

                for (int newRow = Row + 1; newRow < Board.ROWS_LENGTH; newRow++)
                {
                    int newCol = Col - (newRow - Row);
                    if (Board.IsPositionOnTheBoardLimits(board, newRow, newCol) && leftDiagonalPossible)
                    {
                        if (Board.IsSquareAlly(board, newRow, newCol, Color))
                            break;

                        if (pos[0] == newRow && pos[1] == newCol && !Game.IsPiecePinned(board, [Row, Col], [newRow, newCol]))
                            moves.Add([newRow, newCol]);

                        if (!Board.IsSquareEmpty(board, newRow, newCol))
                            leftDiagonalPossible = false;
                    }
                }

                rightDiagonalPossible = true;
                leftDiagonalPossible = true;

                // Check for the diagonals above the queen
                for (int newRow = Row - 1; newRow >= 0; newRow--)
                {
                    int newCol = Col + (Row - newRow);
                    if (Board.IsPositionOnTheBoardLimits(board, newRow, newCol) && rightDiagonalPossible)
                    {
                        if (Board.IsSquareAlly(board, newRow, newCol, Color))
                            break;

                        if (pos[0] == newRow && pos[1] == newCol && !Game.IsPiecePinned(board, [Row, Col], [newRow, newCol]))
                            moves.Add([newRow, newCol]);

                        if (!Board.IsSquareEmpty(board, newRow, newCol))
                            rightDiagonalPossible = false;
                    }
                }

                for (int newRow = Row - 1; newRow >= 0; newRow--)
                {
                    int newCol = Col - (Row - newRow);
                    if (Board.IsPositionOnTheBoardLimits(board, newRow, newCol) && leftDiagonalPossible)
                    {
                        if (Board.IsSquareAlly(board, newRow, newCol, Color))
                            break;

                        if (pos[0] == newRow && pos[1] == newCol && !Game.IsPiecePinned(board, [Row, Col], [newRow, newCol]))
                            moves.Add([newRow, newCol]);

                        if (!Board.IsSquareEmpty(board, newRow, newCol))
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
