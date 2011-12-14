using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SeatBattle.CSharp
{
    public class ComputerPlayer : Player
    {
        private readonly Random _rnd;
        private readonly Timer _timer;
        private readonly List<Point> _currentTarget;

        public ComputerPlayer(string name)
            : base(name)
        {
            _rnd = new Random(DateTime.Now.Millisecond);
            _currentTarget = new List<Point>();
            _timer = new Timer
                         {
                             Enabled = false,
                         };
            _timer.Tick += OnTimer;
        }

        private void OnTimer(object sender, EventArgs e)
        {
            _timer.Stop();
            if (_currentTarget.Count == 0)
            {
                ShootRandom();
                return;
            }

            TryDownShip();
        }

        public override void Shoot()
        {
            base.Shoot();
            _timer.Interval = _rnd.Next(100, 1000);
            _timer.Start();
        }

        protected override void AddShotResult(int x, int y, ShotResult result)
        {
            base.AddShotResult(x, y, result);
            if (result == ShotResult.ShipDrowned)
            {
                _currentTarget.Add(new Point(x, y));
                ShipDrowned();
                return;
            }

            if (result == ShotResult.ShipHit)
            {
                _currentTarget.Add(new Point(x, y));
            }

        }

        public override void Reset()
        {
            base.Reset();
            _currentTarget.Clear();
        }


        private void ShootRandom()
        {
            int x;
            int y;
            do
            {
                x = _rnd.Next(0, 10);
                y = _rnd.Next(0, 10);
            } while (PastShots.ContainsKey(new Point(x, y)));

            ShotTargetChosen(x, y);
        }

        private bool IsValidShot(Point p)
        {
            return !PastShots.ContainsKey(p) && new Rect(0, 0, 10, 10).Contains(p);
        }

        private Point GetRandomNeighbour(Point p)
        {
            int x;
            int y;
            do
            {
                x = _rnd.Next(-1, 2);
                y = _rnd.Next(-1, 2);
            } while (Math.Abs(x + y) != 1);

            return new Point(p.X + x, p.Y + y);
        }

        private void TryDownShip()
        {
            Point lastHit;
            Point prevHit;
            Point nextShot;
            
            if (_currentTarget.Count == 1)
            {
                lastHit = _currentTarget[0];

                do
                {
                    nextShot = GetRandomNeighbour(lastHit);
                } while(!IsValidShot(nextShot));
                Debug.WriteLine("Shot chosen in if");
            }
            else
            {
                lastHit = _currentTarget[_currentTarget.Count - 1];
                prevHit = _currentTarget[_currentTarget.Count - 2];

                var x = lastHit.X - prevHit.X;
                var y = lastHit.Y - prevHit.Y;

                nextShot = new Point(lastHit.X + x, lastHit.Y + y);

                if (!IsValidShot(nextShot))
                {
                    x = _currentTarget[0].X - _currentTarget[1].X;
                    y = _currentTarget[0].Y - _currentTarget[1].Y;

                    nextShot = new Point(_currentTarget[0].X + x, _currentTarget[0].Y + y);

                    if (!IsValidShot(nextShot))
                        throw new Exception("Your logic just failed");
                }
                Debug.WriteLine("Shot chosen in else");
            }

            ShotTargetChosen(nextShot.X, nextShot.Y);
        }

        private void ShipDrowned()
        {
            foreach (var p in _currentTarget)
            {
                PastShots[new Point(p.X - 1, p.Y - 1)] = ShotResult.ShipDrowned;
                PastShots[new Point(p.X - 1, p.Y)] = ShotResult.ShipDrowned;
                PastShots[new Point(p.X - 1, p.Y + 1)] = ShotResult.ShipDrowned;

                PastShots[new Point(p.X, p.Y - 1)] = ShotResult.ShipDrowned;
                PastShots[new Point(p.X, p.Y)] = ShotResult.ShipDrowned;
                PastShots[new Point(p.X, p.Y + 1)] = ShotResult.ShipDrowned;

                PastShots[new Point(p.X + 1, p.Y - 1)] = ShotResult.ShipDrowned;
                PastShots[new Point(p.X + 1, p.Y)] = ShotResult.ShipDrowned;
                PastShots[new Point(p.X + 1, p.Y + 1)] = ShotResult.ShipDrowned;
            }

            _currentTarget.Clear();
        }
    }
}