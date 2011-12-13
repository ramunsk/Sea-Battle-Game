using System;

namespace SeatBattle.CSharp
{
    public class BoardCellClickEventErgs : EventArgs
    {
        private readonly int _x;
        private readonly int _y;

        public BoardCellClickEventErgs(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int Y
        {
            get { return _y; }
        }

        public int X
        {
            get { return _x; }
        }

    }
}