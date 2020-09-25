using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Chess.Usercontrols;
using Chess.Usercontrols.ClassicChess;
using Path = System.IO.Path;
using Timer = System.Timers.Timer;

namespace Chess
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private GameMode _gameMode = GameMode.ClassicChess;
		private Timer _gameTimer;
		private Board _board;
		private readonly List<Piece> pieces;
		private List<ChessField> _chessFields;



		public MainWindow()
		{
			InitializeComponent();
			
			DataContext = Model.MainWindowViewModel;

			//Time handle
			_gameTimer = new Timer(Helper.OneSecond);
			_gameTimer.Elapsed += (sender, e) => { Model.MainWindowViewModel.Seconds++;};
			_gameTimer.Start();

			#region Set window acording to the game mode
			
			UserControl capturedPiecesUc = null;
			switch (_gameMode)
			{
				case GameMode.ClassicChess:
					var classicChessGame = new ClassicChessGame();
					pieces = new List<Piece>(classicChessGame.Pieces);
					break;
				default:
					this.Close();
					break;
			}
			

			#endregion

			PopulateFields(pieces); //Populate & bindings
	
		}
		

		private void Field_Clicked(object sender, MouseButtonEventArgs e)
		{
			
		}

		private void PopulateFields(List<Piece> pieces)
		{
			BoardGrid.Children.RemoveRange(0, BoardGrid.Children.Count); //Remove all children

			#region Bindings & Field population

			_chessFields = new List<ChessField>();
			bool darkerColor = false;
			
			for (int y = 1; y <= Helper.BoardSize; y++)
			{
				darkerColor = !darkerColor; //Change color
				for (int x = 1; x <= Helper.BoardSize; x++)
				{
					//Create field
					ChessField field;
					if (darkerColor)
					{
						field = new ChessField(x, y, Helper.DarkerFieldsColor);
					}
					else
					{
						field = new ChessField(x, y, Helper.ClearerFieldsColor);
					}

					//Each field is a single grid
					Grid fieldGrid = new Grid();
					fieldGrid.SetValue(Grid.RowProperty, Helper.BoardSize - y);
					fieldGrid.SetValue(Grid.ColumnProperty, x - 1);

					//Each grid contains a rectangle (the color of the field)
					Rectangle rectangleField = new Rectangle();
					rectangleField.HorizontalAlignment = HorizontalAlignment.Stretch;
					rectangleField.Stroke = Brushes.Black;
					rectangleField.VerticalAlignment = VerticalAlignment.Stretch;
					rectangleField.DataContext = field;
					rectangleField.SetBinding(Rectangle.FillProperty, "ColorProperty"); //Bind the color property

					//Each grid also contains an image (the image of the piece or just an empty image)
					Image imageField = new Image();
					imageField.MouseDown += Field_Clicked; //Click event

					var pieceOnField = pieces.FirstOrDefault(piece => piece.Position.X == x && piece.Position.Y == y); //Get piece on field
					if (pieceOnField != null)
					{
						imageField.Source = pieceOnField.GetImage(); //Set image if there was any piece on the field
					}
					else
					{
						imageField.Source = Helper.EmptyImage; //If no piece on the field --> Empty image source
					}

					imageField.HorizontalAlignment = HorizontalAlignment.Stretch;
					imageField.VerticalAlignment = VerticalAlignment.Stretch;
					imageField.Tag = x + "," + y; //Set the image tag (needed for the click event)

					//Add dynamically created rectangle and image to field-grid
					fieldGrid.Children.Add(rectangleField);
					fieldGrid.Children.Add(imageField);

					BoardGrid.Children.Add(fieldGrid); //Add field-grid to the main-grid in the XAML

					_chessFields.Add(field); //Add to list --> needed for the initialization of the board
					darkerColor = !darkerColor; //Change color for the next field
				}
			}

			

			#endregion
		}
	}
}
