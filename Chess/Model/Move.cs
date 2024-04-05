using static Chess.Model.IPiece;

namespace Chess.Model
{
    internal class Move : IPiece
    {
        public static string IMAGE_PATH = "C:\\Users\\Usuario\\source\\repos\\ChessGame\\Chess\\Resources\\Images\\Move.png";
        public string Name { get; set; } = "";
        public string Notation { get; set; } = "";
        public Colors Color { get; set; }
        public bool IsBeingAttacked { get; set; } = false;
        public bool WatchingTheEnemyKing { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public Pieces Type { get; set; } = Pieces.None;

        public Move(int row, int col)
        {
            Color = Colors.None;
            Type = Pieces.None;
            Row = row;
            Col = col;
        }

        public Move(Colors? color, Pieces? type, int row, int col)
        {
            Color = color ?? Colors.None;
            Type = type ?? Pieces.None;
            Row = row;
            Col = col;
        }

        public override string ToString()
        {
            return $"{Color}-{Notation}-{Row}-{Col}";
        }

        public List<int[]> GetAllMoves(IPiece?[][] board)
        {
            throw new NotImplementedException();
        }

        public void MovePiece(int row, int col)
        {
            throw new NotImplementedException();
        }

        public void CheckIsWatchingTheEnemyKing(int enemyKingRow, int enemyKingCol)
        {
            throw new NotImplementedException();
        }

        public List<int[]> GetAllCheckBlocks(IPiece?[][] board, List<King.Attacker> attackers)
        {
            throw new NotImplementedException();
        }
    }
}
