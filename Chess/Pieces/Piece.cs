using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chess
{
	public abstract class Piece
	{
		public EPieceColor Color { get; private set; }
		public PiecePos Position { get; set; }

		public delegate void PassPiece(Piece piece);
		public static event PassPiece PieceDie;
		public static event PassPiece ConvertPawn; //When pawn reaches the last row

		private bool isAlive;
		public bool IsAlive
		{
			get { return isAlive; }
			set
			{
				isAlive = value;
				if ( !value)
				{
					PieceDie?.Invoke(this);
				}
			}
		}
		public double Value { get; private set; }
		public int MovesCount { get; set; }

		protected Piece(EPieceColor color, PiecePos position, double value)
		{
			this.Color = color;
			this.Position = position;
			this.IsAlive = true;
			this.Value = value;
		}
		
		public abstract List<PiecePos> PossibleMoves();

		public virtual ImageSource GetImage()
		{
			string color;
			string filePath = string.Empty;

			switch (this.Color)
			{
				case EPieceColor.White:
					color = "white";
					break;
				case EPieceColor.Black:
					color = "dark";
					break;
				case EPieceColor.SUPER:
					return Helper.SuperPiece;
				default:
					//TODO LOG
					throw new NotImplementedException();
			}
			
			filePath = "pack://application:,,,/Chess;component/Resources/" + this.ToString().Split(new char[] { '.' }).Last().ToLower() + "_" + color + ".png";
			
			try
			{
				return new BitmapImage(new Uri(filePath));
			}
			catch (Exception e)
			{
				//TODO LOG
			}

			return Helper.EmptyImage;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Piece))
			{
				return false;
			}

			Piece thisObj = (Piece) obj;
			return (this.Color == thisObj.Color) && (this.Position.Equals(thisObj.Position)) &&
			       (this.IsAlive == thisObj.IsAlive) && (this.Value == thisObj.Value)
			       && (this.MovesCount == thisObj.MovesCount);
		}

		public void Move(PiecePos moveTo)
		{
			
		}
		
		



	}
}