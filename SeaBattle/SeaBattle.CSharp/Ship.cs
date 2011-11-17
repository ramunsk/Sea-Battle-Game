using System;
using System.Collections.Generic;
using System.Drawing;

namespace SeatBattle.CSharp
{
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
    }


    public class ShipCollection : List<Ship>
    {
        public void Randomize()
        {
            foreach (var ship in this)
            {
                //var rnd = new Random(DateTime.Now.Millisecond);
                //ship.Orientation = 
            }
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