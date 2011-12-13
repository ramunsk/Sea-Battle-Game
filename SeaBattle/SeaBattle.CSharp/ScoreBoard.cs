using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SeatBattle.CSharp
{
    public class ScoreBoard : TableLayoutPanel
    {
        private readonly Player _player1;
        private readonly Player _player2;
        private readonly int _shipsPerGame;
        private readonly int _shotsPerGame;
        private Label _player1Label;
        private Label _player2Label;
        private Label _scoreLabel;
        private Label _player1Stats;
        private Label _player2Stats;

        private const string PlayerStatsTemplate = "Ships left: {0}, Shots left: {1}";
        private const string ScoreTemplate = "{0} : {1}";

        private readonly Color _playerNameColor = Color.Black;
        private readonly Color _activePlayerNameColor = Color.FromArgb(0,159,0);

        private Pair<Label, Label> _playerNames;
        private Pair<Label, Label> _playerStats;

        private Point _score;
        private Point _shipsLeft;
        private Point _shotsLeft;

      

        public ScoreBoard(Player player1, Player player2, int shipsPerGame, int shotsPerGame)
        {
            _player1 = player1;
            _player2 = player2;
            _shipsPerGame = shipsPerGame;
            _shotsPerGame = shotsPerGame;

            _player1.MyTurn += OnPlayerTurnChanged;
            _player2.MyTurn += OnPlayerTurnChanged;

            _player1.Shot += OnPlayerMadeShot;
            _player2.Shot += OnPlayerMadeShot;

            SuspendLayout();


            CreateLayout();

            ResumeLayout();
            
        }

        private void OnPlayerMadeShot(object sender, ShootingEventArgs e)
        {
            if (sender == _player1)
            {
                _shotsLeft.X--;
                if (e.Result == ShotResult.ShipDrowned)
                    _shipsLeft.Y--;
            }
            else
            {
                _shotsLeft.Y--;
                if (e.Result == ShotResult.ShipDrowned)
                    _shipsLeft.X--;
            }
            TrackResult();
            RefreshPlayerStats();
        }

        private void RefreshPlayerStats()
        {
            _player1Stats.Text = string.Format(PlayerStatsTemplate, _shipsLeft.X, _shotsLeft.X);
            _player2Stats.Text = string.Format(PlayerStatsTemplate, _shipsLeft.Y, _shotsLeft.Y);
        }


        private void OnPlayerTurnChanged(object sender, EventArgs e)
        {
            var color1 = sender == _player1 ? _activePlayerNameColor : _playerNameColor;
            var color2 = sender == _player2 ? _activePlayerNameColor : _playerNameColor;

            _player1Label.ForeColor = color1;
            _player2Label.ForeColor = color2;
        }

        public void SetActivePlayer(int player)
        {
            var color1 = player == 1 ? _activePlayerNameColor : _playerNameColor;
            var color2 = player == 2 ? _activePlayerNameColor : _playerNameColor;

            _player1Label.ForeColor = color1;
            _player2Label.ForeColor = color2;
        }

        private void TrackResult()
        {
            if (_shipsLeft.X != 0 && _shipsLeft.Y != 0) 
                return;

            if (_shipsLeft.X == 0)
            {
                _score.X++;
            }
            else
            {
                _score.Y++;
            }

            var handler = GameEnded;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public event EventHandler GameEnded;


        public void FullReset()
        {
            _score = new Point(0, 0);
            _scoreLabel.Text = string.Format(ScoreTemplate, _score.X, _score.Y);
            Reset();
        }

        public void Reset()
        {
            _shipsLeft = new Point(_shipsPerGame, _shipsPerGame);
            _shotsLeft = new Point(_shotsPerGame, _shotsPerGame);
            _player1Stats.Visible = false;
            _player2Stats.Visible = false;
        }

        public void StartGate()
        {
            _player1Stats.Visible = true;
            _player2Stats.Visible = true;
            RefreshPlayerStats();
        }



        #region Layout
        protected override void InitLayout()
        {
            base.InitLayout();
            RowCount = 2;
            ColumnCount = 3;
            Padding = Margin = Padding.Empty;
            Font = Parent.Font;

            RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));
            RowStyles.Add(new RowStyle(SizeType.AutoSize, 0));

            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));



            _player1Label = new Label
            {
                AutoSize = true,
                Font = new Font("Calibri", 30, FontStyle.Regular, GraphicsUnit.Pixel, 186),
                Text = "Human",
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            Controls.Add(_player1Label, 0, 0);

            _player2Label = new Label
            {
                AutoSize = true,
                Font = new Font("Calibri", 30, FontStyle.Regular, GraphicsUnit.Pixel, 186),
                TextAlign = ContentAlignment.TopRight,
                Text = "Computer",
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            Controls.Add(_player2Label, 2, 0);

            _scoreLabel = new Label
            {
                AutoSize = true,
                Font = new Font("Calibri", 16, FontStyle.Bold, GraphicsUnit.Point, 186),
                TextAlign = ContentAlignment.TopCenter,
                Text = "0 : 0",
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            Controls.Add(_scoreLabel, 1, 0);

            _player1Stats = new Label
            {
                AutoSize = true,
                TextAlign = ContentAlignment.TopLeft,
                Text = "Ships left: 0, Shots Left: 1",
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            Controls.Add(_player1Stats, 0, 1);

            _player2Stats = new Label
            {
                AutoSize = true,
                TextAlign = ContentAlignment.TopRight,
                Text = "Ships left: 0, Shots Left: 1",
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            Debug.WriteLine(_player2Stats.Font.Name);
            Controls.Add(_player2Stats, 2, 1);


            Height = _player1Label.Height + _player1Stats.PreferredHeight;
        }

        private void CreateLayout()
        {
            //BackColor = Color.Green;

            

        }
        
        #endregion

    }
}