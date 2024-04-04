namespace Chess.Model
{
    internal interface IPiece
    {
        public string Name { get; set; }
        public string Notation { get; set; }
        public Pieces Type { get; set; }
        public Colors Color { get; set; }
        public bool IsBeingAttacked { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public List<int[]> GetAllMoves(IPiece?[][] board);

        public void MovePiece(int newRow, int newCol);

        public enum Pieces
        {
            None,
            Pawn,
            Knight,
            Bishop,
            Rook,
            Queen,
            King
        }

        public enum Colors
        {
            None,
            White,
            Black
        }
    }
}
