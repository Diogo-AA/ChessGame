using static Chess.Model.IPiece;

namespace Chess.Model
{
    internal class Rook : IPiece
    {
        public string Name { get; set; } = "Rook";
        public string Notation { get; set; } = "R";
        public Pieces Type { get; set; } = IPiece.Pieces.Rook;
        public Colors Color { get; set; }
        public bool IsBeingAttacked { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Rook(Colors color, int row, int col)
        {
            Color = color;
            Row = row;
            Col = col;
        }

        public List<int[]> GetAllMoves(IPiece?[][] board)
        {
            var moves = new List<int[]>();
            
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

            return moves;
        }

        public List<int[]> GetAllCheckBlocks(IPiece?[][] board, List<King.Attacker> attackers)
        {
            var moves = new List<int[]>();

            if (attackers.Count == 2)
                return moves;

            foreach (var pos in attackers[0].SquaresToBeBlocked)
            {
                for (int newRow = Row + 1; newRow < Board.ROWS_LENGTH; newRow++)
                {
                    if (Board.IsSquareAlly(board, newRow, Col, Color))
                        break;

                    if (Board.IsPositionOnTheBoardLimits(board, newRow, Col) && pos[0] == newRow && pos[1] == Col && !Game.IsPiecePinned(board, [Row, Col], [newRow, Col]))
                        moves.Add([newRow, Col]);
                }

                for (int newRow = Row - 1; newRow >= 0; newRow--)
                {
                    if (Board.IsSquareAlly(board, newRow, Col, Color))
                        break;

                    if (Board.IsPositionOnTheBoardLimits(board, newRow, Col) && pos[0] == newRow && pos[1] == Col && !Game.IsPiecePinned(board, [Row, Col], [newRow, Col]))
                        moves.Add([newRow, Col]);
                }

                for (int newCol = Col + 1; newCol < Board.COLS_LENGTH; newCol++)
                {
                    if (Board.IsSquareAlly(board, Row, newCol, Color))
                        break;

                    if (Board.IsPositionOnTheBoardLimits(board, Row, newCol) && pos[0] == Row && pos[1] == newCol && !Game.IsPiecePinned(board, [Row, Col], [Row, newCol]))
                        moves.Add([Row, newCol]);
                }

                for (int newCol = Col - 1; newCol >= 0; newCol--)
                {
                    if (Board.IsSquareAlly(board, Row, newCol, Color))
                        break;

                    if (Board.IsPositionOnTheBoardLimits(board, Row, newCol) && pos[0] == Row && pos[1] == newCol && !Game.IsPiecePinned(board, [Row, Col], [Row, newCol]))
                        moves.Add([Row, newCol]);
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
