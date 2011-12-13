namespace SeatBattle.CSharp
{
    public class GameController
    {
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly Board _board1;
        private readonly Board _board2;
        private readonly ScoreBoard _scoreBoard;

        public GameController(Player player1, Player player2, Board board1, Board board2, ScoreBoard scoreBoard)
        {
            _player1 = player1;
            _player2 = player2;
            _board1 = board1;
            _board2 = board2;
            _scoreBoard = scoreBoard;

            _player1.Shooting += OnPlayerShooting;
            _player2.Shooting += OnPlayerShooting;

        }

        private void OnPlayerShooting(object sender, ShootingEventArgs e)
        {
            var shooter = (Player)sender;
            Board oponentBoard;
            Player openent;
            if (shooter == _player1)
            {
                openent = _player2;
                oponentBoard = _board2;
            }
            else
            {
                openent = _player1;
                oponentBoard = _board1;
            }

            var shotResult = oponentBoard.OpenentShotAt(e.X, e.Y);
            e.Result = shotResult;

            if (shotResult != ShotResult.Missed)
            {
                shooter.Shoot();
            }
            else
            {
                openent.Shoot();
            }
        }



        public void NewGame()
        {

        }

        public void StartGame()
        {
            _scoreBoard.StartGate();
            _player1.Shoot();
        }
    }
}