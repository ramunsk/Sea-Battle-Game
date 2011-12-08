using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SeatBattle.CSharp
{
    [DebuggerDisplay("({Location.X},{Location.Y}) {Orientation} x{Length}")]
    public class Ship
    {
        public int Length { get; set; }
        public ShipOrientation Orientation { get; set; }

        public Ship(int length)
        {
            Length = length;
        }

        public bool IsLocatedAt(int x, int y)
        {
            var rect = GetShipRegion();

            return (x >= rect.X && x <= rect.Width && y >= rect.Y && y <= rect.Height);
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

        public int X { get; set; }
        public int Y { get; set; }


    }

    public class DraggableShip : Ship
    {
        private DraggableShip(int length)
            : base(length)
        {
        }

        public Ship Source { get; private set; }

        public static DraggableShip From(Ship ship)
        {
            var draggableShip = new DraggableShip(ship.Length)
                                {
                                    X = ship.X,
                                    Y = ship.Y,
                                    Orientation = ship.Orientation,
                                    Source = ship
                                };

            return draggableShip;
        }
    }


    public enum ShipOrientation
    {
        Horizontal,
        Vertical
    }

    public class RandomPoint
    {
        public int X;
        public int Y;

        public RandomPoint()
        {
            var r = new Random(DateTime.Now.Millisecond);
            X = r.Next(1, 10);
            Y = r.Next(1, 10);
        }

        //public Point ToPoint()
        //{
        //    return new Point(X, Y);
        //}
    }

    public class Player
    {
        public string Name { get; set; }
        public List<Ship> Ships { get; set; }


        public Player(string name)
        {
            Name = name;
            Ships = new List<Ship>
                    {
                        new Ship(4),
                        new Ship(3), new Ship(3),
                        new Ship(2), new Ship(2), new Ship(2),
                        new Ship(1), new Ship(1), new Ship(1), new Ship(1)
                    };
        }


        //public void RandomizeShips()
        //{
        //    foreach (var ship in Ships)
        //    {
        //        ship.Location = new RandomPoint().ToPoint();
        //    }
        //}
    }
}