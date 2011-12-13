using System;

namespace SeatBattle.CSharp
{
    public class ShootingEventArgs : EventArgs
    {
        private readonly int _x;
        private readonly int _y;

        public ShootingEventArgs(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get { return _y; }
        }

        public ShotResult Result { get; set; }
    }
}