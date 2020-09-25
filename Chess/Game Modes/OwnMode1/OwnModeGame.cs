using System.Collections.Generic;

namespace Chess.Game_Modes.OwnMode1
{
	public class OwnModeGame:GameModeBaseClass
	{
		public readonly int BoardSize = 10;
		public List<Piece> Pieces;
		private readonly double _pawnValue = 1d;
		private readonly double _rookValue = 5d;
		private readonly double _knightValue = 3d;
		private readonly double _bishopValue = 3d;
		private readonly double _queenValue = 10d;
		private readonly double _kingValue = 10d;

		public OwnModeGame()
		{
			Piece.PieceDies += PieceDies;
			Piece.ConvertPawn += ConvertPawn;
			Model.GameWindowModel.TurnColor = EPieceColor.White; //set who is going to start

			Pieces = new List<Piece>()
			{
				//White pieces
				new Pawn(EPieceColor.White, new PiecePos(1,2), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(2,1), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(2,3), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(3,2), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(4,1), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(4,3), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(5,2), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(6,1), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(6,3), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(7,2), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(8,1), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(8,3), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(9,2), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(10,1), _pawnValue),
				new Pawn(EPieceColor.White, new PiecePos(10,3), _pawnValue),

				//Darker peices
				new Pawn(EPieceColor.Black, new PiecePos(2,8), _pawnValue),
				new Pawn(EPieceColor.Black, new PiecePos(3,8), _pawnValue),
				new Pawn(EPieceColor.Black, new PiecePos(8,8), _pawnValue),
				new Pawn(EPieceColor.Black, new PiecePos(9,8), _pawnValue),

				new Rook(EPieceColor.Black, new PiecePos(2,9),_rookValue),
				new Knight(EPieceColor.Black, new PiecePos(3,9), _knightValue),
				new Bishop(EPieceColor.Black, new PiecePos(4,9), _bishopValue),
				new Queen(EPieceColor.Black, new PiecePos(5,9), _queenValue),
				new King(EPieceColor.Black, new PiecePos(6,9), _kingValue),
				new Bishop(EPieceColor.Black, new PiecePos(7,9), _bishopValue),
				new Knight(EPieceColor.Black, new PiecePos(8,9), _knightValue),
				new Rook(EPieceColor.Black, new PiecePos(9,9),_rookValue),
			};
			
		}

		private void ConvertPawn(Piece piece)
		{
			Knight newQueen = new Knight(piece.Color, new PiecePos(piece.Position.X, piece.Position.Y), _queenValue);
			Board.PiecesOnTheBoard.Add(newQueen);
			Board.PiecesOnTheBoard.Remove(piece);

			InvokePieceChanged();
		}

		private void PieceDies(Piece piece)
		{
			//TODO Increment captured pieces
			//throw new System.NotImplementedException();
		}
	}
}