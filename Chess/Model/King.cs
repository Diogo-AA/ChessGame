using static Chess.Model.IPiece;

namespace Chess.Model
{
    internal class King : IPiece
    {
        public string Name { get; set; } = "King";
        public string Notation { get; set; } = "K";
        public Pieces Type { get; set; } = IPiece.Pieces.King;
        public Colors Color { get; set; }
        public bool IsBeingAttacked { get; set; } = false;
        public List<Attacker> Attackers { get; set; } = new List<Attacker>();
        public int Row { get; set; }
        public int Col { get; set; }
        
        public struct Attacker
        {
            public List<int[]> SquaresToBeBlocked { get; set; }
            public Pieces Type { get; set; }

            public Attacker(List<int[]> squaresToBeBlocked, Pieces type)
            {
                SquaresToBeBlocked = squaresToBeBlocked;
                Type = type;
            }
        }

        public King(Colors color, int row, int col)
        {
            Color = color;
            Row = row;
            Col = col;
        }

        public List<int[]> GetAllMoves(IPiece?[][] board)
        {
            var moves = new List<int[]>();

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col - 1) 
                && !Board.IsSquareAlly(board, Row + 1, Col - 1, Color) 
                    && !Game.IsSquareAttacked(board, Row + 1, Col - 1, Color))
                moves.Add([Row + 1, Col - 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col) 
                && !Board.IsSquareAlly(board, Row + 1, Col, Color) 
                    && !Game.IsSquareAttacked(board, Row + 1, Col, Color))
                moves.Add([Row + 1, Col]);

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col + 1) 
                && !Board.IsSquareAlly(board, Row + 1, Col + 1, Color) 
                    && !Game.IsSquareAttacked(board, Row + 1, Col + 1, Color))
                moves.Add([Row + 1, Col + 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row, Col - 1) 
                && !Board.IsSquareAlly(board, Row, Col - 1, Color) 
                    && !Game.IsSquareAttacked(board, Row, Col - 1, Color))
                moves.Add([Row, Col - 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row, Col + 1) 
                && !Board.IsSquareAlly(board, Row, Col + 1, Color) 
                    && !Game.IsSquareAttacked(board, Row, Col + 1, Color))
                moves.Add([Row, Col + 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col - 1) 
                && !Board.IsSquareAlly(board, Row - 1, Col - 1, Color) 
                    && !Game.IsSquareAttacked(board, Row - 1, Col - 1, Color))
                moves.Add([Row - 1, Col - 1]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col) 
                && !Board.IsSquareAlly(board, Row - 1, Col, Color) 
                    && !Game.IsSquareAttacked(board, Row - 1, Col, Color))
                moves.Add([Row - 1, Col]);

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col + 1) 
                && !Board.IsSquareAlly(board, Row - 1, Col + 1, Color) 
                    && !Game.IsSquareAttacked(board, Row - 1, Col + 1, Color))
                moves.Add([Row - 1, Col + 1]);

            return moves;
        }

        public List<int[]> GetAllCheckBlocks(IPiece?[][] board, List<Attacker> attackers)
        {
            var moves = new List<int[]>();

            if (attackers.Count == 2)
                return moves;

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col - 1) && !Board.IsSquareAlly(board, Row + 1, Col - 1, Color))
            {
                if (!Game.IsSquareAttacked(board, Row + 1, Col - 1, Color))
                    moves.Add([Row + 1, Col - 1]);
            }

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col) && !Board.IsSquareAlly(board, Row + 1, Col, Color))
            {
                if (!Game.IsSquareAttacked(board, Row + 1, Col, Color))
                    moves.Add([Row + 1, Col]);
            }

            if (Board.IsPositionOnTheBoardLimits(board, Row + 1, Col + 1) && !Board.IsSquareAlly(board, Row + 1, Col + 1, Color))
            {
                if (!Game.IsSquareAttacked(board, Row + 1, Col + 1, Color))
                    moves.Add([Row + 1, Col + 1]);
            }

            if (Board.IsPositionOnTheBoardLimits(board, Row, Col - 1) && !Board.IsSquareAlly(board, Row, Col - 1, Color))
            {
                if (!Game.IsSquareAttacked(board, Row, Col - 1, Color))
                    moves.Add([Row, Col - 1]);
            }

            if (Board.IsPositionOnTheBoardLimits(board, Row, Col + 1) && !Board.IsSquareAlly(board, Row, Col + 1, Color))
            {
                if (!Game.IsSquareAttacked(board, Row, Col + 1, Color))
                    moves.Add([Row, Col + 1]);
            }

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col - 1) && !Board.IsSquareAlly(board, Row - 1, Col - 1, Color))
            {
                if (!Game.IsSquareAttacked(board, Row - 1, Col - 1, Color))
                    moves.Add([Row - 1, Col - 1]);
            }

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col) && !Board.IsSquareAlly(board, Row - 1, Col, Color))
            {
                if (!Game.IsSquareAttacked(board, Row - 1, Col, Color))
                    moves.Add([Row - 1, Col]);
            }

            if (Board.IsPositionOnTheBoardLimits(board, Row - 1, Col + 1) && !Board.IsSquareAlly(board, Row - 1, Col + 1, Color))
            {
                if (!Game.IsSquareAttacked(board, Row - 1, Col + 1, Color))
                    moves.Add([Row - 1, Col + 1]);
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
