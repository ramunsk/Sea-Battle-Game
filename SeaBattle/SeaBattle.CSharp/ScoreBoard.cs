using System.Windows.Forms;

namespace SeatBattle.CSharp
{
    public class ScoreBoard : Control
    {
        private readonly string _player1;
        private readonly string _player2;

        public ScoreBoard(string player1, string player2)
        {
            _player1 = player1;
            _player2 = player2;
        }

        
    }

    
}