using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace SeatBattle.CSharp
{
    [DebuggerDisplay("({X},{Y}) {Width}x{Height}")]
    public class Rect
    {
        private int _width;
        private int _height;


        public Rect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width
        {
            get { return _width; }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Width should be possitive number");

                _width = value;
            }
        }

        public int Height
        {
            get { return _height; }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Height should be possitive number");
                _height = value;
            }
        }

        public int Right
        {
            get { return X + Width - 1; }
        }

        public int Bottom
        {
            get { return Y + Height - 1; }
        }

        public void Inflate(int width, int height)
        {
            X -= width;
            Y -= height;
            Width += width * 2;
            Height += height * 2;
        }

        public bool Contains(Rect rect)
        {
            return X <= rect.X && Y <= rect.Y
                && Right >= rect.Right && Bottom >= rect.Bottom;
        }

        public bool Contains(Point point)
        {
            return point.X >= X && point.X <= Right && point.Y >= Y && point.Y <= Bottom;
        }

        public bool IntersectsWith(Rect rect)
        {
            return !(
                X > rect.Right
                || Right < rect.X
                || Y > rect.Bottom
                || Bottom < rect.Y
            );
        }

        public void MoveTo(int x, int y)
        {
            X = x;
            Y = y;
        }

        public IList<Point> GetPoints()
        {
            var points = new List<Point>();

            for (var x = X; x <= Right; x++)
            {
                for (var y = Y; y <= Bottom; y++)
                {
                    points.Add(new Point(x, y));
                }
            }

            return points;
        }
    }
}