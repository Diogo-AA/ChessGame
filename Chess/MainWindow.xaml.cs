﻿using Chess.Model;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using static Chess.Model.IPiece;
using static Chess.PromoteWindow;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly string IMAGE_PATH = "C:\\Users\\Usuario\\source\\repos\\ChessGame\\Chess\\Resources\\Images";
        private readonly Game game = new Game();

        public MainWindow()
        {
            InitializeComponent();

            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < Board.ROWS_LENGTH; i++)
            {
                for (int j = 0; j < Board.COLS_LENGTH; j++)
                {
                    var piece = game.board[i][j];
                    var image = new Image
                    {
                        Source = piece is null ? null : new BitmapImage(new Uri($"{IMAGE_PATH}\\{piece.Color}-{piece.Type}.png")),
                        Width = 90,
                        Height = 90,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    var border = new Border()
                    {
                        Background = (i + j) % 2 == 0 ? System.Windows.Media.Brushes.White : System.Windows.Media.Brushes.Gray,
                        Child = image
                    };
                    border.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnPieceClick));


                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    GridBoard.Children.Add(border);
                }
            }
        }

        private void OnPieceClick(object sender, MouseButtonEventArgs e)
        {
            var border = (Border)sender;
            var row = Grid.GetRow(border);
            var col = Grid.GetColumn(border);
            var piece = game.board[row][col];

            if (game.IsGameOver())
                return;

            if (piece is null || ((piece is not Move && !piece.IsBeingAttacked) && piece.Color != game.Turn))
                return;

            if (game.PieceSelected == piece)
            {
                RemoveMarksOfAllPossibleMoves();
                game.PieceSelected = null;
                return;
            }

            if (piece is Move || (piece.IsBeingAttacked && piece.Type != Pieces.King))
            {
                MovePiece(border, row, col);
                RemoveMarksOfAllPossibleMoves();
            }
            else
            {
                game.PieceSelected = piece;
                RemoveMarksOfAllPossibleMoves();
                MarkAllPossibleMoves();
            }

        }

        private void RemoveMarksOfAllPossibleMoves()
        {
            for (int i = 0; i < GridBoard.Children.Count; i++)
            {
                if (GridBoard.Children[i] is Border border)
                {
                    var row = Grid.GetRow(border);
                    var col = Grid.GetColumn(border);
                    var piece = game.board[row][col];

                    if (border.Child is null || ((Image)border.Child).Source is null || piece is null)
                        continue;                    

                    if (piece is Move)
                    {
                        game.board[row][col] = null;
                        border.Child = new Image
                        {
                            Source = null,
                            Width = 90,
                            Height = 90,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                    }
                    else if (piece.IsBeingAttacked && piece.Type != Pieces.King)
                    {
                        piece.IsBeingAttacked = false;
                        border.Child = new Image
                        {
                            Source = new BitmapImage(new Uri($"{IMAGE_PATH}\\{piece.Color}-{piece.Type}.png")),
                            Width = 90,
                            Height = 90,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                    }
                }
            }
        }

        private void MovePiece(Border newPiecePositionBorder, int row, int col)
        {
            if (game.PieceSelected is null)
                return;
             
            var oldPiecePositionBorder = (Border)GridBoard.Children.Cast<UIElement>().First(
                e => Grid.GetRow(e) == game.PieceSelected.Row && Grid.GetColumn(e) == game.PieceSelected.Col
            );

            oldPiecePositionBorder.Child = new Image
            {
                Source = null,
                Width = 90,
                Height = 90,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Board.MovePiece(game.board, game.PieceSelected.Row, game.PieceSelected.Col, row, col);

            if (game.PieceSelected.Type == Pieces.Pawn)
            {
                if ((game.PieceSelected.Color == Colors.White && row == 7) || (game.PieceSelected.Color == Colors.Black && row == 0))
                {
                    var promoteWindow = new PromoteWindow(game.PieceSelected.Color);
                    promoteWindow.ShowDialog();

                    switch (promoteWindow.PromotedType)
                    {
                        case Pieces.Queen:
                            game.board[row][col] = new Queen(game.PieceSelected.Color, row, col);
                            game.PieceSelected = game.board[row][col];
                            break;
                        case Pieces.Rook:
                            game.board[row][col] = new Rook(game.PieceSelected.Color, row, col);
                            game.PieceSelected = game.board[row][col];
                            break;
                        case Pieces.Knight:
                            game.board[row][col] = new Knight(game.PieceSelected.Color, row, col);
                            game.PieceSelected = game.board[row][col];
                            break;
                        case Pieces.Bishop:
                            game.board[row][col] = new Bishop(game.PieceSelected.Color, row, col);
                            game.PieceSelected = game.board[row][col];
                            break;
                    }
                }
            }

            newPiecePositionBorder.Child = new Image
            {
                Source = new BitmapImage(new Uri($"{IMAGE_PATH}\\{game.PieceSelected.Color}-{game.PieceSelected.Type}.png")),
                Width = 90,
                Height = 90,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            game.CheckKings();

            if (game.IsGameOver())
            {
                MessageBox.Show($"{game.PieceSelected.Color} Won!");
                return;
            }

            game.PieceSelected = null;
            game.Turn = game.Turn == Colors.White ? Colors.Black : Colors.White;
        }

        private void MarkAllPossibleMoves()
        {
            King king = game.GetKing(game.Turn);
            List<int[]> moves;

            if (king.IsBeingAttacked)
            {
                moves = game.PieceSelected.GetAllCheckBlocks(game.board, king.Attackers);
            }
            else
            {
                moves = game.PieceSelected.GetAllMoves(game.board);
            }

            foreach (int[] pos in moves)
            {
                var piece = game.board[pos[0]][pos[1]];
                
                if (Board.IsSquareEnemy(game.board, pos[0], pos[1], game.PieceSelected.Color))
                {
                    game.board[pos[0]][pos[1]].IsBeingAttacked = true;
                }
                else if (Board.IsSquareEmpty(game.board, pos[0], pos[1]))
                {
                    game.board[pos[0]][pos[1]] = new Move(pos[0], pos[1]);
                }

                var border = (Border)GridBoard.Children.Cast<UIElement>().First(
                    e => Grid.GetRow(e) == pos[0] && Grid.GetColumn(e) == pos[1]
                );

                var imagePath = (piece is null || piece.Type == Pieces.None) ? Move.IMAGE_PATH : $"{IMAGE_PATH}\\{piece.Color}-{piece.Type}-Attacked.png";
                border.Child = new Image
                {
                    Source = new BitmapImage(new Uri(imagePath)),
                    Width = 90,
                    Height = 90,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
            }
        }
    }
}