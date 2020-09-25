using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess
{
	public class Board
	{

		private ChessField _lastSelectedField = null;
		private Image _lastSelectedImage = null;


		public static List<Piece> PiecesOnTheBoard { get; private set; }

		public Board(List<Piece> piecesOnTheBoard)
		{
			PiecesOnTheBoard = new List<Piece>(piecesOnTheBoard);

		}

		/// <summary>
	}
}