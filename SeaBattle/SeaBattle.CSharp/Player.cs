using System;
using System.Drawing;

namespace SeatBattle.CSharp
{
    public abstract class Player
    {
        protected Player(string name)
        {
            Name = name;
            Board = new Board();
        }

        public string Name { get; set; }

        public Board Board { get; set; }








        //public event EventHandler<ShotEventArgs> OnShotMade;

        //protected void OnReadyToShootAt(int x, int y)
        //{
        //    var handler = OnShotMade;

        //    if (handler == null) 
        //        return;

        //    var e = new ShotEventArgs(x, y);
            
        //    handler(this, e);
        //}

    }

    public class GameController
    {
        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public Point Score { get; set; }

        public void NewGame()
        {
        }

        public void StartGame()
        {
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