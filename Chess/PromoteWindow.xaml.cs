using Chess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess
{
    /// <summary>
    /// Lógica de interacción para PromoteWindow.xaml
    /// </summary>
    public partial class PromoteWindow : Window
    {
        public IPiece.Pieces PromotedType { get; set; } = IPiece.Pieces.Queen;

        public PromoteWindow(IPiece.Colors color)
        {
            InitializeComponent();

            ImgQueen.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler((sender, e) => { PromotedType = IPiece.Pieces.Queen; Close(); }));
            ImgQueen.Source = new BitmapImage(new Uri($"{MainWindow.IMAGE_PATH}\\{color}-Queen.png"));

            ImgRook.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler((sender, e) => { PromotedType = IPiece.Pieces.Rook; Close(); }));
            ImgRook.Source = new BitmapImage(new Uri($"{MainWindow.IMAGE_PATH}\\{color}-Rook.png"));
            
            ImgKnight.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler((sender, e) => { PromotedType = IPiece.Pieces.Knight; Close(); }));
            ImgKnight.Source = new BitmapImage(new Uri($"{MainWindow.IMAGE_PATH}\\{color}-Knight.png"));

            ImgBishop.AddHandler(MouseLeftButtonDownEvent, new MouseButtonEventHandler((sender, e) => { PromotedType = IPiece.Pieces.Bishop; Close(); }));
            ImgBishop.Source = new BitmapImage(new Uri($"{MainWindow.IMAGE_PATH}\\{color}-Bishop.png"));
        }
    }
}
