using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Chess
{
	public struct PiecePos
	{
		public int X { get; set; }
		public int Y { get; set; }

		public PiecePos(int X, int Y)
		{
			this.X = X;
			this.Y = Y;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is PiecePos))
			{
				return false;
			}

			PiecePos toCompare = (PiecePos)obj;
			return (this.X == toCompare.X) && (this.Y == toCompare.Y);
		}
	}
}