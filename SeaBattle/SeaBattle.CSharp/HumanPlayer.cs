using System.Drawing;

namespace SeatBattle.CSharp
{
    public class HumanPlayer : Player
    {
        private readonly Board _board;

        public HumanPlayer(string name, Board board)
            : base(name)
        {
            _board = board;
            _board.OnClick += OnBoardClick;

        }

        private void OnBoardClick(object sender, BoardCellClickEventErgs e)
        {
            if (PastShots.ContainsKey(new Point(e.X, e.Y)))
                return;

            ShotTargetChosen(e.X, e.Y);
        }

    }
}