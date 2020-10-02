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
                throw e;
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
            List<PiecePos> possibleMoves = PossibleMoves();
            if (!possibleMoves.Contains(moveTo))
            {  
                return;
            }

            this.MovesCount++;


            if (!Board.PiecesOnTheBoard.Contains(this))
            {  
                throw new NotImplementedException();
            }

            Piece thisPiece = Board.PiecesOnTheBoard.FirstOrDefault(p => p.Equals(this));

            if (thisPiece == null)
            { 
                throw new NotImplementedException();
            }

            var otherPieceInField = Board.PiecesOnTheBoard.FirstOrDefault(p => p.Position.Equals(moveTo));
            if (otherPieceInField != null)
            {
                //Capture enemy piece
                otherPieceInField.IsAlive = false;
                Board.PiecesOnTheBoard.Remove(otherPieceInField);
            }
        }

        /// <summary>
		/// Pieces which move using a pattern (offsets) should use this method
		/// </summary>
		/// <param name="offsetList"></param>
		/// <param name="canJump"></param>
		/// <returns></returns>
		public List<PiecePos> OffsetMovementHandle(List<Tuple<int, int>> offsetList)
        {
            /*AXIOM: The pieces that are not allowed to jump will never take a diagonal as alternative route to reach the endField. They are always taking the straight way.
			 *		 The way could be eighter vertical first and then horizontal or otherwise !!
			 *
			 * EXAMPLE:
			 *
			 * Knight (In this example the piece CAN'T jump) is at B1
			 * It should reach A3 or C3
			 * The path to reach those fields would be:
			 *		B1 --> B2 --> B3 ---> A3//C3
			 *				OR
			 *		B1 --> A1 --> A2 --> A3
			 *		B1 --> C1 --> C2 --> C3
			 *
			 *
			 * In case the AXIOM is not guaranteed the path would be: (NOT VALID!!!)
			 *		B1 --> A2 --> A3 // B1 --> B2 --> A3 // B1 --> B2 --> B3 --> A3
			 *		B1 --> C2 --> C3 // B1 --> B2 --> C3 // B1 --> B2 --> B3 --> C3
			 *
			 */


            List<PiecePos> allMoves = new List<PiecePos>();
            foreach (var offset in offsetList)
            {
                PiecePos moveToField = new PiecePos(this.Position.X + offset.Item1, this.Position.Y + offset.Item2);

                if (moveToField.X > Helper.BoardSize || moveToField.X < 1 || moveToField.Y > Helper.BoardSize || moveToField.Y < 1)
                {
                    //Invalid field (out of bounds)
                    continue;
                }

                allMoves.Add(moveToField);
            }

            foreach (var movement in new List<PiecePos>(allMoves))
            {
                //If the color of the pieces is different, the otherPieceInField result will be null and the field will be validated
                var otherPieceInField = Board.PiecesOnTheBoard
                    .FirstOrDefault(p => p.IsAlive && p.Position.Equals(movement) && p.Color == this.Color);

                if (otherPieceInField != null)
                {
                    allMoves.Remove(movement); //The piece at the reachable field is from the same color than this piece --> Can't overload = Can't recah
                }
            }

        


            return allMoves;
        }





    }
}