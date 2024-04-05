using System.Diagnostics;
using System.Windows.Input;
using static Chess.Model.IPiece;
using static Chess.Model.King;

namespace Chess.Model
{
    internal class Game
    {
        public IPiece?[][] board;
        public HashSet<string> WhitePiecesPinned = [];
        public HashSet<string> BlackPiecesPinned = [];
        public King WhiteKing;
        public King BlackKing;
        public IPiece? PieceSelected { get; set; } = null;
        public Colors Turn { get; set; } = Colors.White;

        public Game()
        {
            board = new IPiece[Board.ROWS_LENGTH][];

            board[0] = [
                new Rook(Colors.White, 0, 0),
                new Knight(Colors.White, 0, 1),
                new Bishop(Colors.White, 0, 2),
                new Queen(Colors.White, 0, 3),
                new King(Colors.White, 0, 4),
                new Bishop(Colors.White, 0, 5),
                new Knight(Colors.White, 0, 6),
                new Rook(Colors.White, 0, 7)
            ];

            board[1] = [
                new Pawn(Colors.White, 1, 0), 
                new Pawn(Colors.White, 1, 1), 
                new Pawn(Colors.White, 1, 2), 
                new Pawn(Colors.White, 1, 3),
                new Pawn(Colors.White, 1, 4), 
                new Pawn(Colors.White, 1, 5), 
                new Pawn(Colors.White, 1, 6), 
                new Pawn(Colors.White, 1, 7)
            ];

            for (int i = 2; i < Board.ROWS_LENGTH - 2; i++)
            {
                board[i] = [null, null, null, null, null, null, null, null];
            }

            board[6] = [
                new Pawn(Colors.Black, 6, 0),
                new Pawn(Colors.Black, 6, 1),
                new Pawn(Colors.Black, 6, 2),
                new Pawn(Colors.Black, 6, 3),
                new Pawn(Colors.Black, 6, 4),
                new Pawn(Colors.Black, 6, 5),
                new Pawn(Colors.Black, 6, 6),
                new Pawn(Colors.Black, 6, 7)
            ];

            board[7] = [
                new Rook(Colors.Black, 7, 0),
                new Knight(Colors.Black, 7, 1),
                new Bishop(Colors.Black, 7, 2),
                new Queen(Colors.Black, 7, 3),
                new King(Colors.Black, 7, 4),
                new Bishop(Colors.Black, 7, 5),
                new Knight(Colors.Black, 7, 6),
                new Rook(Colors.Black, 7, 7)
            ];

            WhiteKing = (King)board[0][4];
            BlackKing = (King)board[7][4];
        }

        public King GetKing(Colors color)
        {
            return color == Colors.White ? WhiteKing : BlackKing;
        }

        public bool IsGameOver()
        {
            return false;
        }

        public void CheckKings()
        {
            CheckKing(WhiteKing);
            CheckKing(BlackKing);
        }

        private void CheckKing(King king)
        {
            king.IsBeingAttacked = false;
            king.Attackers.Clear();

            CheckAttackersCol(board, king);
            CheckAttackersRow(board, king);
            CheckAttackersDiagonal(board, king);
            CheckKnightsAttakcers(board, king);
        }

        private void CheckAttackersCol(IPiece?[][] board, King king)
        {
            var squaresToBeBlocked = new List<int[]>();
            bool pieceFound;
            int numAllies, i;

            numAllies = 0;
            i = king.Col + 1;
            pieceFound = false;
            while (i < Board.COLS_LENGTH && numAllies < 2 && !pieceFound)
            {
                squaresToBeBlocked.Add([king.Row, i]);

                if (Board.IsSquareAlly(board, king.Row, i, king.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, king.Row, i, king.Color))
                {
                    switch (board[king.Row][i].Type)
                    {
                        case Pieces.Rook:
                        case Pieces.Queen:
                            pieceFound = true;
                            if (numAllies == 0)
                            {
                                king.IsBeingAttacked = true;
                                king.Attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[king.Row][i].Type));
                            }
                            else if (numAllies == 1)
                            {
                                //piecesPinned.Add($"{king.Row},{i}");
                            }
                            break;
                    }
                }

                i++;
            }

            numAllies = 0;
            i = king.Col - 1;
            pieceFound = false;
            squaresToBeBlocked.Clear();
            while (i >= 0 && numAllies < 2 && !pieceFound)
            {
                squaresToBeBlocked.Add([king.Row, i]);

                if (Board.IsSquareAlly(board, king.Row, i, king.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, king.Row, i, king.Color))
                {
                    switch (board[king.Row][i].Type)
                    {
                        case Pieces.Rook:
                        case Pieces.Queen:
                            pieceFound = true;
                            if (numAllies == 0)
                            {
                                king.IsBeingAttacked = true;
                                king.Attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[king.Row][i].Type));
                            }
                            else if (numAllies == 1)
                            {
                                //piecesPinned.Add($"{king.Row},{i}");
                            }
                            break;
                    }
                }

                i--;
            }
        }

        private void CheckAttackersRow(IPiece?[][] board, King king)
        {
            var squaresToBeBlocked = new List<int[]>();
            bool pieceFound;
            int numAllies, i;

            numAllies = 0;
            i = king.Row + 1;
            pieceFound = false;
            while (i < Board.ROWS_LENGTH && numAllies < 2 && !pieceFound)
            {
                squaresToBeBlocked.Add([i, king.Col]);

                if (Board.IsSquareAlly(board, i, king.Col, king.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, i, king.Col, king.Color))
                {
                    switch (board[i][king.Col].Type)
                    {
                        case Pieces.Rook:
                        case Pieces.Queen:
                            pieceFound = true;
                            if (numAllies == 0)
                            {
                                king.IsBeingAttacked = true;
                                king.Attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][king.Col].Type));
                            }
                            else if (numAllies == 1)
                            {
                                //piecesPinned.Add($"{i},{king.Col}");
                            }
                            break;
                    }
                }

                i++;
            }

            numAllies = 0;
            i = king.Row - 1;
            pieceFound = false;
            squaresToBeBlocked.Clear();
            while (i >= 0 && numAllies < 2 && !pieceFound)
            {
                squaresToBeBlocked.Add([i, king.Col]);

                if (Board.IsSquareAlly(board, i, king.Col, king.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, i, king.Col, king.Color))
                {
                    switch (board[i][king.Col].Type)
                    {
                        case Pieces.Rook:
                        case Pieces.Queen:
                            pieceFound = true;
                            if (numAllies == 0)
                            {
                                king.IsBeingAttacked = true;
                                king.Attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][king.Col].Type));
                            }
                            else if (numAllies == 1)
                            {
                                //piecesPinned.Add($"{i},{king.Col}");
                            }
                            break;
                    }
                }

                i--;
            }
        }

        private void CheckAttackersDiagonal(IPiece?[][] board, King king)
        {
            var squaresToBeBlocked = new List<int[]>();
            bool pieceFound = false;
            int numAllies = 0, i;
            
            // Check right down diagonal
            i = king.Row + 1;
            while (i < Board.ROWS_LENGTH && numAllies < 2 && !pieceFound)
            {
                int rightCol = king.Col + (i - king.Row);

                if (!Board.IsPositionOnTheBoardLimits(board, i, rightCol))
                    break;

                squaresToBeBlocked.Add([i, rightCol]);
                if (Board.IsSquareAlly(board, i, rightCol, king.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, i, rightCol, king.Color))
                {
                    pieceFound = true;

                    switch (board[i][rightCol].Type)
                    {
                        case Pieces.Bishop:
                        case Pieces.Queen:
                            if (numAllies == 0)
                            {
                                king.IsBeingAttacked = true;
                                king.Attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][rightCol].Type));
                            }
                            else if (numAllies == 1)
                            {
                                //piecesPinned.Add($"{i},{col}");
                            }
                            break;
                    }
                }

                i++;
            }

            // Check left down diagonal
            squaresToBeBlocked.Clear();
            pieceFound = false;
            numAllies = 0;
            i = king.Row + 1;
            while (i < Board.ROWS_LENGTH && numAllies < 2 && !pieceFound)
            {
                int leftCol = king.Col - (i - king.Row);

                if (!Board.IsPositionOnTheBoardLimits(board, i, leftCol))
                    break;

                squaresToBeBlocked.Add([i, leftCol]);
                if (Board.IsSquareAlly(board, i, leftCol, king.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, i, leftCol, king.Color))
                {
                    pieceFound = true;

                    switch (board[i][leftCol].Type)
                    {
                        case Pieces.Bishop:
                        case Pieces.Queen:
                            if (numAllies == 0)
                            {
                                king.IsBeingAttacked = true;
                                king.Attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][leftCol].Type));
                            }
                            else if (numAllies == 1)
                            {
                                //piecesPinned.Add($"{i},{col}");
                            }
                            break;
                    }
                }

                i++;
            }

            // Check right up diagonal
            squaresToBeBlocked.Clear();
            pieceFound = false;
            numAllies = 0;
            i = king.Row - 1;
            while (i >= 0 && numAllies < 2 && !pieceFound)
            {
                int rightCol = king.Col + (king.Row - i);

                if (!Board.IsPositionOnTheBoardLimits(board, i, rightCol))
                    break;

                squaresToBeBlocked.Add([i, rightCol]);
                if (Board.IsSquareAlly(board, i, rightCol, king.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, i, rightCol, king.Color))
                {
                    pieceFound = true;

                    switch (board[i][rightCol].Type)
                    {
                        case Pieces.Bishop:
                        case Pieces.Queen:
                            if (numAllies == 0)
                            {
                                king.IsBeingAttacked = true;
                                king.Attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][rightCol].Type));
                            }
                            else if (numAllies == 1)
                            {
                                //piecesPinned.Add($"{i},{col}");
                            }
                            break;
                    }
                }

                i--;
            }

            // Check left up diagonal
            squaresToBeBlocked.Clear();
            pieceFound = false;
            numAllies = 0;
            i = king.Row - 1;
            while (i >= 0 && numAllies < 2 && !pieceFound)
            {
                int leftCol = king.Col - (king.Row - i);

                if (!Board.IsPositionOnTheBoardLimits(board, i, leftCol))
                    break;

                squaresToBeBlocked.Add([i, leftCol]);
                if (Board.IsSquareAlly(board, i, leftCol, king.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, i, leftCol, king.Color))
                {
                    pieceFound = true;

                    switch (board[i][leftCol].Type)
                    {
                        case Pieces.Bishop:
                        case Pieces.Queen:
                            if (numAllies == 0)
                            {
                                king.IsBeingAttacked = true;
                                king.Attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][leftCol].Type));
                            }
                            else if (numAllies == 1)
                            {
                                //piecesPinned.Add($"{i},{col}");
                            }
                            break;
                    }
                }

                i--;
            }
        }

        private void CheckKnightsAttakcers(IPiece?[][] board, King king)
        {
            if (Board.IsPositionOnTheBoardLimits(board, king.Row + 2, king.Col - 1)
                && Board.IsSquareEnemy(board, king.Row + 2, king.Col - 1, king.Color)
                    && board[king.Row + 2][king.Col - 1].Type == Pieces.Knight)
            {
                king.IsBeingAttacked = true;
                king.Attackers.Add(new Attacker([[king.Row + 2, king.Col - 1]], Pieces.Knight));
                return;
            }

            if (Board.IsPositionOnTheBoardLimits(board, king.Row + 2, king.Col + 1) 
                && Board.IsSquareEnemy(board, king.Row + 2, king.Col + 1, king.Color)
                    && board[king.Row + 2][king.Col + 1].Type == Pieces.Knight)
            {
                king.IsBeingAttacked = true;
                king.Attackers.Add(new Attacker([[king.Row + 2, king.Col + 1]], Pieces.Knight));
                return;
            }

            if (Board.IsPositionOnTheBoardLimits(board, king.Row - 2, king.Col + 1) 
                && Board.IsSquareEnemy(board, king.Row - 2, king.Col + 1, king.Color)
                    && board[king.Row - 2][king.Col + 1].Type == Pieces.Knight)
            {
                king.IsBeingAttacked = true;
                king.Attackers.Add(new Attacker([[king.Row - 2, king.Col + 1]], Pieces.Knight));
                return;
            }

            if (Board.IsPositionOnTheBoardLimits(board, king.Row - 2, king.Col - 1) 
                && Board.IsSquareEnemy(board, king.Row - 2, king.Col - 1, king.Color)
                    && board[king.Row - 2][king.Col - 1].Type == Pieces.Knight)
            {
                king.IsBeingAttacked = true;
                king.Attackers.Add(new Attacker([[king.Row - 2, king.Col - 1]], Pieces.Knight));
                return;
            }

            if (Board.IsPositionOnTheBoardLimits(board, king.Row + 1, king.Col + 2) 
                && Board.IsSquareEnemy(board, king.Row + 1, king.Col + 2, king.Color)
                    && board[king.Row + 1][king.Col + 2].Type == Pieces.Knight)
                {
                king.IsBeingAttacked = true;
                king.Attackers.Add(new Attacker([[king.Row + 1, king.Col + 2]], Pieces.Knight));
                return;
                                }

            if (Board.IsPositionOnTheBoardLimits(board, king.Row + 1, king.Col - 2) 
                && Board.IsSquareEnemy(board, king.Row + 1, king.Col - 2, king.Color)
                    && board[king.Row + 1][king.Col - 2].Type == Pieces.Knight)
            {
                king.IsBeingAttacked = true;
                king.Attackers.Add(new Attacker([[king.Row + 1, king.Col - 2]], Pieces.Knight));
                return;
            }

            if (Board.IsPositionOnTheBoardLimits(board, king.Row - 1, king.Col + 2) 
                && Board.IsSquareEnemy(board, king.Row - 1, king.Col + 2, king.Color)
                    && board[king.Row - 1][king.Col + 2].Type == Pieces.Knight)
            {
                king.IsBeingAttacked = true;
                king.Attackers.Add(new Attacker([[king.Row - 1, king.Col + 2]], Pieces.Knight));
                return;
            }

            if (Board.IsPositionOnTheBoardLimits(board, king.Row - 1, king.Col - 2) 
                && Board.IsSquareEnemy(board, king.Row - 1, king.Col - 2, king.Color)
                    && board[king.Row - 1][king.Col - 2].Type == Pieces.Knight)
            {
                king.IsBeingAttacked = true;
                king.Attackers.Add(new Attacker([[king.Row - 1, king.Col - 2]], Pieces.Knight));
                return;
            }
        }
    }
}
