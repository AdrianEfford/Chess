using System;
using System.Collections.Generic;

namespace Chess
{
    public class King : Piece
    {
        readonly List<Tuple<int, int>> _kingOffsets = new List<Tuple<int, int>>()
        {
			//Tuple<X-Offset-Value, Y-Offset-Value>
			new Tuple<int, int>(-1,1),
            new Tuple<int, int>(-1,0),
            new Tuple<int, int>(-1,-1),
            new Tuple<int, int>(0,1),
            new Tuple<int, int>(0,-1),
            new Tuple<int, int>(1,1),
            new Tuple<int, int>(1,0),
            new Tuple<int, int>(1,-1),
        };

        public King(EPieceColor color, PiecePos position, double value) : base(color, position, value)
        {
        }

        public override List<PiecePos> PossibleMoves()
        {
            return OffsetMovementHandle(_kingOffsets);
        }
    }
}