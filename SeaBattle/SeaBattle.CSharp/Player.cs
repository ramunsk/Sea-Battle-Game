using System;
using System.Collections.Generic;
using System.Drawing;

namespace SeatBattle.CSharp
{
    public abstract class Player
    {
        protected readonly Dictionary<Point, ShotResult> PastShots; 

        protected Player(string name)
        {
            Name = name;
            PastShots = new Dictionary<Point, ShotResult>();
        }

        public string Name { get; set; }

        public abstract void Shoot(Action<int, int> makeShot);

        public void SetShotResult(int x, int y, ShotResult result)
        {
            PastShots[new Point(x, y)] = result;
        }
    }


    public class HumanPlayer : Player
    {
        private readonly Board _board;
        private Action<int, int> _makeShot;

        public HumanPlayer(string name, Board board):base(name)
        {
            _board = board;
            _board.OnClick += OnBoardClick;

        }

        private void OnBoardClick(object sender, BoardCellClickEventErgs e)
        {
            if (PastShots.ContainsKey(new Point(e.X, e.Y)))
                return;

            _makeShot(e.X, e.Y);
        }

        public override void Shoot(Action<int, int> makeShot)
        {
            _makeShot = makeShot;
        }
    }

    public class AiPlayer : Player
    {
        private readonly Random _rnd;

        public AiPlayer(string name) : base(name)
        {
            _rnd = new Random(DateTime.Now.Millisecond);
        }

        public override void Shoot(Action<int, int> makeShot)
        {
            int x;
            int y;
            do
            {
                x = _rnd.Next(10);
                y = _rnd.Next(10);
            } while (PastShots.ContainsKey(new Point(x, y)));

            makeShot(x, y);
        }
    }




    public class GameController
    {
        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public Board Player1Board { get; set; }
        public Board Player2Board { get; set; }

        public Point Score { get; set; }


        public void OnPlayer1Shot(int x, int y)
        {
            var result = Player2Board.OpenentShotAt(x, y);
            Player1.SetShotResult(x, y, result);
            if (result != ShotResult.Missed)
                Player1.Shoot(OnPlayer1Shot);
            else
                Player2.Shoot(OnPlayer2Shot);
        }

        public void OnPlayer2Shot(int x, int y)
        {
            var result = Player1Board.OpenentShotAt(x, y);
            Player2.SetShotResult(x, y, result);
            if (result != ShotResult.Missed)
                Player2.Shoot(OnPlayer2Shot);
            else
                Player1.Shoot(OnPlayer1Shot);
        }


        public void NewGame()
        {

        }

        public void StartGame()
        {
            Player1.Shoot(OnPlayer1Shot);
        }
    }

    public enum ShotResult
    {
        Missed,
        ShipHit,
        ShipDrowned
    }


    //internal class ShotEventArgs : EventArgs
    //{
    //    public ShotEventArgs(int x, int y)
    //    {
    //        ShotAt = new Point(x, y);
    //    }

    //    public Point ShotAt { get; private set; }
    //}
}