using System.Diagnostics;
using System.Net.NetworkInformation;
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
        public static King WhiteKing;
        public static King BlackKing;
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
            CheckKingAttackers(WhiteKing);
            CheckKingAttackers(BlackKing);
        }

        private void CheckKingAttackers(King king)
        {
            king.IsBeingAttacked = false;
            king.Attackers.Clear();

            GetAttackersCol(board, king).ForEach(p => king.Attackers.Add(p));
            GetAttackersRow(board, king).ForEach(p => king.Attackers.Add(p));
            GetAttackersDiagonal(board, king).ForEach(p => king.Attackers.Add(p));
            GetKnightsAttakcers(board, king).ForEach(p => king.Attackers.Add(p));

            if (king.Attackers.Count > 0)
            {
                king.IsBeingAttacked = true;
            }
        }

        public static bool IsSquareAttacked(IPiece?[][] board, int row, int col, Colors color)
        {
            if (GetAttackersCol(board, new Move(color, Pieces.King, row, col)).Count > 0)
                return true;
            if (GetAttackersRow(board, new Move(color, Pieces.King, row, col)).Count > 0)
                return true;
            if (GetAttackersDiagonal(board, new Move(color, Pieces.King, row, col)).Count > 0)
                return true;
            if (GetKnightsAttakcers(board, new Move(color, Pieces.King, row, col)).Count > 0)
                return true;
            return false;
        }

        public static bool IsPiecePinned(IPiece?[][] board, int[] oldPos, int[] newPos)
        {
            var piece = board[oldPos[0]][oldPos[1]];

            if (piece == null)
            {
                return false;
            }

            IPiece?[][] boardCopy = new IPiece[Board.ROWS_LENGTH][];
            for (int i = 0; i < Board.ROWS_LENGTH; i++)
            {
                boardCopy[i] = new IPiece[Board.COLS_LENGTH];
                for (int j = 0; j < Board.COLS_LENGTH; j++)
                {
                    boardCopy[i][j] = board[i][j];
                }
            }

            boardCopy[oldPos[0]][oldPos[1]] = null;
            boardCopy[newPos[0]][newPos[1]] = piece;

            var king = piece.Color == Colors.White ? WhiteKing : BlackKing;

            return IsSquareAttacked(boardCopy, king.Row, king.Col, piece.Color);
        }

        private static List<Attacker> GetAttackersRow(IPiece?[][] board, IPiece piece)
        {
            var attackers = new List<Attacker>();
            var squaresToBeBlocked = new List<int[]>();
            bool pieceFound;
            int numAllies, i;

            numAllies = 0;
            i = piece.Col + 1;
            pieceFound = false;
            while (i < Board.COLS_LENGTH && numAllies < 2 && !pieceFound)
            {
                squaresToBeBlocked.Add([piece.Row, i]);

                if (Board.IsSquareAlly(board, piece.Row, i, piece.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, piece.Row, i, piece.Color))
                {
                    pieceFound = true;

                    switch (board[piece.Row][i].Type)
                    {
                        case Pieces.Rook:
                        case Pieces.Queen:
                            if (numAllies == 0)
                                attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[piece.Row][i].Type));
                            else if (numAllies == 1)
                            {
                                //piecesPinned.Add($"{piece.Row},{i}");
                            }
                            break;
                    }
                }

                i++;
            }

            numAllies = 0;
            i = piece.Col - 1;
            pieceFound = false;
            squaresToBeBlocked.Clear();
            while (i >= 0 && numAllies < 2 && !pieceFound)
            {
                squaresToBeBlocked.Add([piece.Row, i]);

                if (Board.IsSquareAlly(board, piece.Row, i, piece.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, piece.Row, i, piece.Color))
                {
                    pieceFound = true;

                    switch (board[piece.Row][i].Type)
                    {
                        case Pieces.Rook:
                        case Pieces.Queen:
                            if (numAllies == 0)
                                attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[piece.Row][i].Type));
                            else if (numAllies == 1)
                            {
                                //piecesPinned.Add($"{piece.Row},{i}");
                            }
                            break;
                    }
                }

                i--;
            }

            return attackers;
        }

        private static List<Attacker> GetAttackersCol(IPiece?[][] board, IPiece piece)
        {
            var attackers = new List<Attacker>();
            var squaresToBeBlocked = new List<int[]>();
            bool pieceFound;
            int numAllies, i;

            numAllies = 0;
            i = piece.Row + 1;
            pieceFound = false;
            while (i < Board.ROWS_LENGTH && numAllies < 2 && !pieceFound)
            {
                squaresToBeBlocked.Add([i, piece.Col]);

                if (Board.IsSquareAlly(board, i, piece.Col, piece.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, i, piece.Col, piece.Color))
                {
                    pieceFound = true;

                    switch (board[i][piece.Col].Type)
                    {
                        case Pieces.Rook:
                        case Pieces.Queen:
                            if (numAllies == 0)
                                attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][piece.Col].Type));
                            else if (numAllies == 1)
                            {
                                //piecesPinned.Add($"{i},{piece.Col}");
                            }
                            break;
                    }
                }

                i++;
            }

            numAllies = 0;
            i = piece.Row - 1;
            pieceFound = false;
            squaresToBeBlocked.Clear();
            while (i >= 0 && numAllies < 2 && !pieceFound)
            {
                squaresToBeBlocked.Add([i, piece.Col]);

                if (Board.IsSquareAlly(board, i, piece.Col, piece.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, i, piece.Col, piece.Color))
                {
                    pieceFound = true;

                    switch (board[i][piece.Col].Type)
                    {
                        case Pieces.Rook:
                        case Pieces.Queen:
                            if (numAllies == 0)
                                attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][piece.Col].Type));
                            else if (numAllies == 1)
                            {
                                //piecesPinned.Add($"{i},{piece.Col}");
                            }
                            break;
                    }
                }

                i--;
            }

            return attackers;
        }

        private static List<Attacker> GetAttackersDiagonal(IPiece?[][] board, IPiece piece)
        {
            var attackers = new List<Attacker>();
            var squaresToBeBlocked = new List<int[]>();
            bool pieceFound = false;
            int numAllies = 0, i;
            
            // Check right down diagonal
            i = piece.Row + 1;
            while (i < Board.ROWS_LENGTH && numAllies < 2 && !pieceFound)
            {
                int rightCol = piece.Col + (i - piece.Row);

                if (!Board.IsPositionOnTheBoardLimits(board, i, rightCol))
                    break;

                squaresToBeBlocked.Add([i, rightCol]);
                if (Board.IsSquareAlly(board, i, rightCol, piece.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, i, rightCol, piece.Color))
                {
                    pieceFound = true;

                    switch (board[i][rightCol].Type)
                    {
                        case Pieces.Bishop:
                        case Pieces.Queen:
                            if (numAllies == 0)
                                attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][rightCol].Type));
                            break;
                        case Pieces.Pawn:
                            if (numAllies == 0 && i == piece.Row + 1)
                                attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][rightCol].Type));
                            break;
                    }
                }

                i++;
            }

            // Check left down diagonal
            squaresToBeBlocked.Clear();
            pieceFound = false;
            numAllies = 0;
            i = piece.Row + 1;
            while (i < Board.ROWS_LENGTH && numAllies < 2 && !pieceFound)
            {
                int leftCol = piece.Col - (i - piece.Row);

                if (!Board.IsPositionOnTheBoardLimits(board, i, leftCol))
                    break;

                squaresToBeBlocked.Add([i, leftCol]);
                if (Board.IsSquareAlly(board, i, leftCol, piece.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, i, leftCol, piece.Color))
                {
                    pieceFound = true;

                    switch (board[i][leftCol].Type)
                    {
                        case Pieces.Bishop:
                        case Pieces.Queen:
                            if (numAllies == 0)
                                attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][leftCol].Type));
                            break;
                        case Pieces.Pawn:
                            if (numAllies == 0 && i == piece.Row + 1)
                                attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][leftCol].Type));
                            break;
                    }
                }

                i++;
            }

            // Check right up diagonal
            squaresToBeBlocked.Clear();
            pieceFound = false;
            numAllies = 0;
            i = piece.Row - 1;
            while (i >= 0 && numAllies < 2 && !pieceFound)
            {
                int rightCol = piece.Col + (piece.Row - i);

                if (!Board.IsPositionOnTheBoardLimits(board, i, rightCol))
                    break;

                squaresToBeBlocked.Add([i, rightCol]);
                if (Board.IsSquareAlly(board, i, rightCol, piece.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, i, rightCol, piece.Color))
                {
                    pieceFound = true;

                    switch (board[i][rightCol].Type)
                    {
                        case Pieces.Bishop:
                        case Pieces.Queen:
                            if (numAllies == 0)
                                attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][rightCol].Type));
                            break;
                        case Pieces.Pawn:
                            if (numAllies == 0 && i == piece.Row - 1)
                                attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][rightCol].Type));
                            break;
                    }
                }

                i--;
            }

            // Check left up diagonal
            squaresToBeBlocked.Clear();
            pieceFound = false;
            numAllies = 0;
            i = piece.Row - 1;
            while (i >= 0 && numAllies < 2 && !pieceFound)
            {
                int leftCol = piece.Col - (piece.Row - i);

                if (!Board.IsPositionOnTheBoardLimits(board, i, leftCol))
                    break;

                squaresToBeBlocked.Add([i, leftCol]);
                if (Board.IsSquareAlly(board, i, leftCol, piece.Color))
                {
                    numAllies++;
                }
                else if (Board.IsSquareEnemy(board, i, leftCol, piece.Color))
                {
                    pieceFound = true;

                    switch (board[i][leftCol].Type)
                    {
                        case Pieces.Bishop:
                        case Pieces.Queen:
                            if (numAllies == 0)
                                attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][leftCol].Type));
                            break;
                        case Pieces.Pawn:
                            if (numAllies == 0 && i == piece.Row - 1)
                                attackers.Add(new Attacker(squaresToBeBlocked.ToList(), board[i][leftCol].Type));
                            break;
                    }
                }

                i--;
            }

            return attackers;
        }

        private static List<Attacker> GetKnightsAttakcers(IPiece?[][] board, IPiece piece)
        {
            if (Board.IsPositionOnTheBoardLimits(board, piece.Row + 2, piece.Col - 1)
                && Board.IsSquareEnemy(board, piece.Row + 2, piece.Col - 1, piece.Color)
                    && board[piece.Row + 2][piece.Col - 1].Type == Pieces.Knight)
            {
                return[new Attacker([[piece.Row + 2, piece.Col - 1]], Pieces.Knight)];
            }

            if (Board.IsPositionOnTheBoardLimits(board, piece.Row + 2, piece.Col + 1) 
                && Board.IsSquareEnemy(board, piece.Row + 2, piece.Col + 1, piece.Color)
                    && board[piece.Row + 2][piece.Col + 1].Type == Pieces.Knight)
            {
                return[new Attacker([[piece.Row + 2, piece.Col + 1]], Pieces.Knight)];
            }

            if (Board.IsPositionOnTheBoardLimits(board, piece.Row - 2, piece.Col + 1) 
                && Board.IsSquareEnemy(board, piece.Row - 2, piece.Col + 1, piece.Color)
                    && board[piece.Row - 2][piece.Col + 1].Type == Pieces.Knight)
            {
                return[new Attacker([[piece.Row - 2, piece.Col + 1]], Pieces.Knight)];
            }

            if (Board.IsPositionOnTheBoardLimits(board, piece.Row - 2, piece.Col - 1) 
                && Board.IsSquareEnemy(board, piece.Row - 2, piece.Col - 1, piece.Color)
                    && board[piece.Row - 2][piece.Col - 1].Type == Pieces.Knight)
            {
                return[new Attacker([[piece.Row - 2, piece.Col - 1]], Pieces.Knight)];
            }

            if (Board.IsPositionOnTheBoardLimits(board, piece.Row + 1, piece.Col + 2) 
                && Board.IsSquareEnemy(board, piece.Row + 1, piece.Col + 2, piece.Color)
                    && board[piece.Row + 1][piece.Col + 2].Type == Pieces.Knight)
                {
                return[new Attacker([[piece.Row + 1, piece.Col + 2]], Pieces.Knight)];
                                }

            if (Board.IsPositionOnTheBoardLimits(board, piece.Row + 1, piece.Col - 2) 
                && Board.IsSquareEnemy(board, piece.Row + 1, piece.Col - 2, piece.Color)
                    && board[piece.Row + 1][piece.Col - 2].Type == Pieces.Knight)
            {
                return[new Attacker([[piece.Row + 1, piece.Col - 2]], Pieces.Knight)];
            }

            if (Board.IsPositionOnTheBoardLimits(board, piece.Row - 1, piece.Col + 2) 
                && Board.IsSquareEnemy(board, piece.Row - 1, piece.Col + 2, piece.Color)
                    && board[piece.Row - 1][piece.Col + 2].Type == Pieces.Knight)
            {
                return[new Attacker([[piece.Row - 1, piece.Col + 2]], Pieces.Knight)];
            }

            if (Board.IsPositionOnTheBoardLimits(board, piece.Row - 1, piece.Col - 2) 
                && Board.IsSquareEnemy(board, piece.Row - 1, piece.Col - 2, piece.Color)
                    && board[piece.Row - 1][piece.Col - 2].Type == Pieces.Knight)
            {
                return[new Attacker([[piece.Row - 1, piece.Col - 2]], Pieces.Knight)];
            }

            return [];
        }
    }
}
