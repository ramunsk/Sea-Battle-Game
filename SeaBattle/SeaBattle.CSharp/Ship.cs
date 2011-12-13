using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SeatBattle.CSharp
{
    [DebuggerDisplay("({Location.X},{Location.Y}) {Orientation} x{Length}")]
    public class Ship
    {
        public Ship(int length)
        {
            Length = length;
        }

        public int Length { get; set; }
        public ShipOrientation Orientation { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int HitCount { get; set; }

        public bool IsDrowned
        {
            get
            {
                return HitCount == Length;
            }
        }

        public bool IsLocatedAt(int x, int y)
        {
            var rect = GetShipRegion();

            return (x >= rect.X && x <= rect.Right && y >= rect.Y && y <= rect.Bottom);
        }

        public Rect GetShipRegion()
        {
            var width = Orientation == ShipOrientation.Horizontal ? Length : 1;
            var height = Orientation == ShipOrientation.Vertical ? Length : 1;

            return new Rect(X, Y, width, height);
        }

        public bool IsInRegion(Rect rect)
        {
            var r = GetShipRegion();
            return (rect.IntersectsWith(r));
        }

        public void MoveTo(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Rotate()
        {
            Orientation = Orientation == ShipOrientation.Horizontal ? ShipOrientation.Vertical : ShipOrientation.Horizontal;
        }

    }
}