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
            
            for (int i = Row + 1; i < Board.ROWS_LENGTH; i++)
            {
                if (Board.IsPositionOnTheBoardLimits(board, i, Col))
                {
                    if (!Board.IsSquareAlly(board, i, Col, Color))
                        moves.Add([i, Col]);

                    if (!Board.IsSquareEmpty(board, i, Col))
                        break;
                }
            }

            for (int i = Row - 1; i >= 0; i--)
            {
                if (Board.IsPositionOnTheBoardLimits(board, i, Col))
                {
                    if (!Board.IsSquareAlly(board, i, Col, Color))
                        moves.Add([i, Col]);

                    if (!Board.IsSquareEmpty(board, i, Col))
                        break;
                }
            }

            for (int i = Col + 1; i < Board.COLS_LENGTH; i++)
            {
                if (Board.IsPositionOnTheBoardLimits(board, Row, i))
                {
                    if (!Board.IsSquareAlly(board, Row, i, Color))
                        moves.Add([Row, i]);

                    if (!Board.IsSquareEmpty(board, Row, i))
                        break;
                }
            }

            for (int i = Col - 1; i >= 0; i--)
            {
                if (Board.IsPositionOnTheBoardLimits(board, Row, i))
                {
                    if (!Board.IsSquareAlly(board, Row, i, Color))
                        moves.Add([Row, i]);

                    if (!Board.IsSquareEmpty(board, Row, i))
                        break;
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

        public List<int[]> GetAllCheckBlocks(IPiece?[][] board, List<King.Attacker> attackers, int kingRow, int kingCol)
        {
            throw new NotImplementedException();
        }
    }
}
