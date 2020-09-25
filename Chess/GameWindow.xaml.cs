using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Chess.Game_Modes;
using Chess.Game_Modes.OwnMode1;
using Chess.Usercontrols.ClassicChess;

namespace Chess.Resources
{
	/// <summary>
	/// Interaction logic for UserControl1.xaml
	/// </summary>
	public partial class GameWindowUC : UserControl
	{
		private Timer _gameTimer;
		private Board _board;
		private List<Piece> pieces;
		private List<ChessField> _chessFields;

		public GameWindowUC()
		{
			InitializeComponent();

			DataContext = Model.GameWindowModel;
			Model.GameWindowModel.GameVisibility = Visibility.Visible;

			//Set events
			GameModeBaseClass.GameEndEvent += GameOver;
			GameModeBaseClass.PieceChangedEvent += PieceChanged;
			Model.OnGameModeChanged += OnGameModeChanged;
		}

		private void OnGameModeChanged(GameMode changedto)
		{
			//Time handle
			Model.GameWindowModel.Seconds = 1; //reset time
			_gameTimer = new Timer(Helper.OneSecond);
			_gameTimer.Elapsed += (sender, e) => { Model.GameWindowModel.Seconds++; };
			_gameTimer.Start();

			#region Set window acording to the game mode

			UserControl capturedPiecesUc = null;
			switch (changedto)
			{
				case GameMode.ClassicChess:
					capturedPiecesUc = new ClassicChessCapturedPieces();
					var classicChessGame = new ClassicChessGame();
					pieces = new List<Piece>(classicChessGame.Pieces);
					Helper.BoardSize = classicChessGame.BoardSize;
					break;
				case GameMode.OwnGameMode1:
					var ownGameMode1 = new OwnModeGame();
					pieces = new List<Piece>(ownGameMode1.Pieces);
					Helper.BoardSize = ownGameMode1.BoardSize;
					break;
				default:
					//TODO LOG
					//this.Close();
					break;
			}

			if (capturedPiecesUc != null)
			{
				UC_CapturedPieces.Children.RemoveRange(0, UC_CapturedPieces.Children.Count); //Remove all
				UC_CapturedPieces.Children.Add(capturedPiecesUc);
			}

			#endregion

			PopulateFields(pieces); //Populate & bindings
		}

		


		private void PieceChanged()
		{
			PopulateFields(Board.PiecesOnTheBoard);
		}

		private void GameOver(EPieceColor winner)
		{
			_gameTimer.Stop();
			BoardGrid.Children.RemoveRange(0, BoardGrid.Children.Count);

			GameModeBaseClass.GameEndEvent -= GameOver;
			GameModeBaseClass.PieceChangedEvent -= PieceChanged;
			Model.OnGameModeChanged -= OnGameModeChanged;

			Model.State = State.MainMenu;
		}

		private void Field_Clicked(object sender, MouseButtonEventArgs e)
		{
			_board.FieldClick(sender, e);
		}

		
		private void PopulateFields(List<Piece> pieces)
		{
			BoardGrid.Children.RemoveRange(0, BoardGrid.Children.Count); //Remove all children

			if (BoardGrid.ColumnDefinitions.Count > 0)
			{
				BoardGrid.ColumnDefinitions.RemoveRange(0, BoardGrid.ColumnDefinitions.Count);
			}
			if (BoardGrid.RowDefinitions.Count > 0)
			{
				BoardGrid.RowDefinitions.RemoveRange(0, BoardGrid.RowDefinitions.Count);
			}

			#region Bindings & Field population

			_chessFields = new List<ChessField>();
			bool darkerColor = false;

			//Create grid
			for (int i = 0; i < Helper.BoardSize; i++)
			{
				var column = new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) };
				var row = new RowDefinition { Height = new GridLength(1, GridUnitType.Star) };
				BoardGrid.ColumnDefinitions.Add(column);
				BoardGrid.RowDefinitions.Add(row);
			}

			//Create fields
			for (int y = 1; y <= Helper.BoardSize; y++)
			{

				darkerColor = !darkerColor; //Change color
				for (int x = 1; x <= Helper.BoardSize; x++)
				{
					var pieceOnField = pieces.FirstOrDefault(piece => piece.Position.X == x && piece.Position.Y == y); //Get piece on field

					#region Create Chess Field
					SolidColorBrush color;
					ImageSource image;
					if (darkerColor)
					{
						color = Helper.DarkerFieldsColor;
					}
					else
					{
						color = Helper.ClearerFieldsColor;
					}

					if (pieceOnField == null)
					{
						image = Helper.EmptyImage;
					}
					else
					{
						image = pieceOnField.GetImage();
					}
					ChessField chessField = new ChessField(x, y, color, image); //Create field

					#endregion

					//Each field is a single grid
					Grid fieldGrid = new Grid();
					fieldGrid.SetValue(Grid.RowProperty, Helper.BoardSize - y);
					fieldGrid.SetValue(Grid.ColumnProperty, x - 1);

					//Each grid contains a rectangle (the color of the field)
					Rectangle rectangleField = new Rectangle();
					rectangleField.HorizontalAlignment = HorizontalAlignment.Stretch;
					rectangleField.Stroke = Brushes.Black;
					rectangleField.VerticalAlignment = VerticalAlignment.Stretch;
					rectangleField.DataContext = chessField;
					rectangleField.SetBinding(Rectangle.FillProperty, "ColorProperty"); //Bind the color property

					//Each grid also contains an image (the image of the piece or just an empty image)
					Image imageField = new Image();
					imageField.DataContext = chessField;
					imageField.SetBinding(Image.SourceProperty, "ImageSource"); //Binding
					imageField.MouseDown += Field_Clicked; //Click event
					imageField.HorizontalAlignment = HorizontalAlignment.Stretch;
					imageField.VerticalAlignment = VerticalAlignment.Stretch;
					imageField.Tag = x + "," + y; //Set the image tag (needed for the click event)

					//Add dynamically created rectangle and image to field-grid
					fieldGrid.Children.Add(rectangleField);
					fieldGrid.Children.Add(imageField);

					BoardGrid.Children.Add(fieldGrid); //Add field-grid to the main-grid in the XAML

					_chessFields.Add(chessField); //Add to list --> needed for the initialization of the board
					darkerColor = !darkerColor; //Change color for the next field
				}
			}

			_board = new Board(pieces, _chessFields);

			#endregion
		}
	}
}
