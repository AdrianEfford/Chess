using System.Collections.Generic;
using System.Linq;

namespace Chess.Usercontrols.ClassicChess
{
	public class ClassicChessGame
	{
		private readonly double _pawnValue = 1d;
		private readonly double _rookValue = 5d;
		private readonly double _knightValue = 3d;
		private readonly double _bishopValue = 3d;
		private readonly double _queenValue = 10d;
		private readonly double _kingValue = 1000d; //just a simbolic value --> When the king dies, the game ends

		public List<Piece> Pieces;

		public ClassicChessGame()
		{
			Model.MainWindowViewModel.TurnColor = EPieceColor.White; //set who is going to start

			Pieces = new List<Piece>()
			{

			
				
			};
		}
			
	}
}