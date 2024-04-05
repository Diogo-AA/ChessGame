using static Chess.Model.IPiece;

namespace Chess.Model
{
    internal class Pawn : IPiece
    {
        public string Name { get; set; } = "Pawn";
        public string Notation { get; set; } = "P";
        public Pieces Type { get; set; } = IPiece.Pieces.Pawn;
        public Colors Color { get; set; }
        public bool IsBeingAttacked { get; set; }
        public bool FirstMove { get; set; } = true;
        public int Row { get; set; }
        public int Col { get; set; }

        public Pawn(Colors color, int row, int col)
        {
            Color = color;
            Row = row;
            Col = col;
        }

        //TODO: promotion and en passant
        public List<int[]> GetAllMoves(IPiece?[][] board)
        {
            var moves = new List<int[]>();
            var newRow = Color == Colors.White ? Row + 1 : Row - 1;

            if (newRow < 0 || newRow > Board.ROWS_LENGTH - 1)
                return moves;

            if (Board.IsSquareEmpty(board, newRow, Col))
            {
                moves.Add([newRow, Col]);

                var newRowFirstMove = Color == Colors.White ? newRow + 1 : newRow - 1;
                if (FirstMove && Board.IsSquareEmpty(board, newRowFirstMove, Col))
                    moves.Add([newRowFirstMove, Col]);
            }

            if (Col < Board.COLS_LENGTH - 1 && Board.IsSquareEnemy(board, newRow, Col + 1, Color))
                moves.Add([newRow, Col + 1]);

            if (Col > 0 && Board.IsSquareEnemy(board, newRow, Col - 1, Color))
                moves.Add([newRow, Col - 1]);

            return moves;
        }

        public void MovePiece(int newRow, int newCol)
        {
            Row = newRow;
            Col = newCol;
            FirstMove = false;
        }

        public List<int[]> GetAllCheckBlocks(IPiece?[][] board, List<King.Attacker> attackers, int kingRow, int kingCol)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{Color}-{Notation}-{Row}-{Col}";
        }
    }
}
