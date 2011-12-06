using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace SeatBattle.CSharp
{
    [DebuggerDisplay("({Location.X},{Location.Y}) {Orientation}")]
    public class Ship
    {
        public Point Location { get; set; }
        public int Length { get; set; }
        public ShipOrientation Orientation { get; set; }

        public Ship(int length)
        {
            Length = length;
            Location = new Point(-1, -1);
        }

        public bool IsLocatedAt(int x, int y)
        {
            var rect = GetShipRegion();

            return (x >= rect.X && x <= rect.Width && y >= rect.Y && y <= rect.Height);
        }


        public Rectangle GetShipRegion()
        {
            var dx = Orientation == ShipOrientation.Horizontal ? Location.X + Length - 1 : Location.X;
            var dy = Orientation == ShipOrientation.Vertical ? Location.Y + Length - 1 : Location.Y;

            return new Rectangle(Location, new Size(dx, dy));
        }

        public void MoveTo(int x, int y)
        {
            Location = new Point(x, y);
        }
    }

    public class DraggableShip : Ship
    {
        private DraggableShip(int length) : base(length)
        {
        }

        public Ship Source { get; private set; }

        public static DraggableShip From(Ship ship)
        {
            var draggableShip = new DraggableShip(ship.Length)
                                {
                                    Location = ship.Location,
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

        public Point ToPoint()
        {
            return new Point(X, Y);
        }
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


        public void RandomizeShips()
        {
            foreach(var ship in Ships)
            {
                ship.Location = new RandomPoint().ToPoint();
            }
        }
    }
}