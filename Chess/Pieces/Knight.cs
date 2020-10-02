using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    public class Knight : Piece
    {
        public static readonly List<Tuple<int, int>> KnightOffsets = new List<Tuple<int, int>>()
        {
			//Tuple<X-Offset-Value, Y-Offset-Value>
			new Tuple<int, int>(-1,2),
            new Tuple<int, int>(1,2),
            new Tuple<int, int>(-2,1),
            new Tuple<int, int>(2,1),
            new Tuple<int, int>(-2,-1),
            new Tuple<int, int>(2,-1),
            new Tuple<int, int>(-1,-2),
            new Tuple<int, int>(1,-2),
        };

        public Knight(EPieceColor color, PiecePos position, double value) : base(color, position, value)
        {

        }

        public override List<PiecePos> PossibleMoves()
        {
            return OffsetMovementHandle(KnightOffsets, true);
        }

    }
}