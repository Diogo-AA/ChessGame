using static Chess.Model.IPiece;
using static Chess.Model.King;

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

            if (Board.IsPositionOnTheBoardLimits(board, Row + 2, Col - 1) 
                && !Board.IsSquareAlly(board, Row + 2, Col - 1, Color) 
                    && !Game.IsPiecePinned(board, [Row, Col], [Row + 2, Col - 1]))
                moves.Add([Row + 2, Col - 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row + 2, Col + 1) 
                && !Board.IsSquareAlly(board, Row + 2, Col + 1, Color) 
                    && !Game.IsPiecePinned(board, [Row, Col], [Row + 2, Col + 1]))
                moves.Add([Row + 2, Col + 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 2, Col + 1) 
                && !Board.IsSquareAlly(board, Row - 2, Col + 1, Color) 
                    && !Game.IsPiecePinned(board, [Row, Col], [Row - 2, Col + 1]))
                moves.Add([Row - 2, Col + 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 2, Col - 1) 
                && !Board.IsSquareAlly(board, Row - 2, Col - 1, Color) 
                    && !Game.IsPiecePinned(board, [Row, Col], [Row - 2, Col - 1]))
                moves.Add([Row - 2, Col - 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col + 2) 
                && !Board.IsSquareAlly(board, Row + 1, Col + 2, Color) 
                    && !Game.IsPiecePinned(board, [Row, Col], [Row + 1, Col + 2]))
                moves.Add([Row + 1, Col + 2]);

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col - 2) 
                && !Board.IsSquareAlly(board, Row + 1, Col - 2, Color) 
                    && !Game.IsPiecePinned(board, [Row, Col], [Row + 1, Col - 2]))
                moves.Add([Row + 1, Col - 2]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col + 2) 
                && !Board.IsSquareAlly(board, Row - 1, Col + 2, Color) 
                    && !Game.IsPiecePinned(board, [Row, Col], [Row - 1, Col + 2]))
                moves.Add([Row - 1, Col + 2]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col - 2) 
                && !Board.IsSquareAlly(board, Row - 1, Col - 2, Color) 
                    && !Game.IsPiecePinned(board, [Row, Col], [Row - 1, Col - 2]))
                moves.Add([Row - 1, Col - 2]);

            return moves;
        }

        public List<int[]> GetAllCheckBlocks(IPiece?[][] board, List<Attacker> attackers)
        {
            var moves = new List<int[]>();

            if (attackers.Count == 2)
                return moves;

            foreach (var pos in attackers[0].SquaresToBeBlocked)
            {
                if (Board.IsPositionOnTheBoardLimits(board, Row + 2, Col - 1) 
                    && pos[0] == Row + 2 && pos[1] == Col - 1 
                        && !Game.IsPiecePinned(board, [Row, Col], [Row + 2, Col - 1]))
                    moves.Add([Row + 2, Col - 1]);

                if (Board.IsPositionOnTheBoardLimits(board, Row + 2, Col + 1) 
                    && pos[0] == Row + 2 && pos[1] == Col + 1 
                        && !Game.IsPiecePinned(board, [Row, Col], [Row + 2, Col + 1]))
                    moves.Add([Row + 2, Col + 1]);

                if (Board.IsPositionOnTheBoardLimits(board, Row - 2, Col + 1) 
                    && pos[0] == Row - 2 && pos[1] == Col + 1 
                        && !Game.IsPiecePinned(board, [Row, Col], [Row - 2, Col + 1]))
                    moves.Add([Row - 2, Col + 1]);

                if (Board.IsPositionOnTheBoardLimits(board, Row - 2, Col - 1) 
                    && pos[0] == Row - 2 && pos[1] == Col - 1 
                        && !Game.IsPiecePinned(board, [Row, Col], [Row - 2, Col - 1]))
                    moves.Add([Row - 2, Col - 1]);

                if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col + 2) 
                    && pos[0] == Row + 1 && pos[1] == Col + 2 
                        && !Game.IsPiecePinned(board, [Row, Col], [Row + 1, Col + 2]))
                    moves.Add([Row + 1, Col + 2]);

                if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col - 2) 
                    && pos[0] == Row + 1 && pos[1] == Col - 2 
                        && !Game.IsPiecePinned(board, [Row, Col], [Row + 1, Col - 2]))
                    moves.Add([Row + 1, Col - 2]);

                if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col + 2) 
                    && pos[0] == Row - 1 && pos[1] == Col + 2 
                        && !Game.IsPiecePinned(board, [Row, Col], [Row - 1, Col + 2]))
                    moves.Add([Row - 1, Col + 2]);

                if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col - 2) 
                    && pos[0] == Row - 1 && pos[1] == Col - 2 
                        && !Game.IsPiecePinned(board, [Row, Col], [Row - 1, Col - 2]))
                    moves.Add([Row - 1, Col - 2]);
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
