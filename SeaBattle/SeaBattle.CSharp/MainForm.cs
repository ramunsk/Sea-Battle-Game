using System.Drawing;
using System.Windows.Forms;

namespace SeatBattle.CSharp
{
    public class MainForm : Form
    {
        private readonly Player _humanPlayer;
        private readonly Player _computerPlayer;

        private readonly Board _humanBoard;
        private readonly Board _computerBoard;
        
        private readonly GameController _controller;
        
        private readonly ScoreBoard _scoreboard;

        private readonly Button _shuffleButton;
        private readonly Button _startGameButton;
        private readonly Button _newGameButton;

        private static readonly Color ButtonBackColor = Color.FromArgb(65, 133, 243);
        private const char ShuffleCharacter = (char)0x60;
        private const char StartGameCharacter = (char)0x55;
        private const char NewGameCharacter = (char)0x6C;

        public MainForm()
        {
            SuspendLayout();

            _humanBoard = new Board();
            _computerBoard = new Board(false);

            _humanPlayer = new HumanPlayer("You", _computerBoard);
            _computerPlayer = new ComputerPlayer("Computer");


            _scoreboard = new ScoreBoard(_humanPlayer, _computerPlayer, 10, 100);
            _controller = new GameController(_humanPlayer, _computerPlayer, _humanBoard, _computerBoard, _scoreboard);
            
            _shuffleButton = CreateButton(ShuffleCharacter.ToString(), ButtonBackColor);
            _newGameButton = CreateButton(NewGameCharacter.ToString(), ButtonBackColor);
            _startGameButton = CreateButton(StartGameCharacter.ToString(), ButtonBackColor);

            SetupWindow();
            LayoutControls();

            _scoreboard.GameEnded += OnGameEnded;

            _shuffleButton.Click += OnShuffleButtonClick;
            _startGameButton.Click += OnStartGameButtonClick;
            _newGameButton.Click += OnNewGameButtonClick;

            ResumeLayout();

            StartNewGame();
        }

        private void OnNewGameButtonClick(object sender, System.EventArgs e)
        {
            StartNewGame();
        }


        private void StartNewGame()
        {
            _shuffleButton.Visible = true;
            _startGameButton.Visible = true;
            _newGameButton.Visible = false;
            _controller.NewGame();
        }


        private void OnStartGameButtonClick(object sender, System.EventArgs e)
        {
            _shuffleButton.Visible = false;
            _newGameButton.Visible = false;
            _startGameButton.Visible = false;
            _controller.StartGame();
        }

        private void OnShuffleButtonClick(object sender, System.EventArgs e)
        {
            _humanBoard.AddRandomShips();
        }

        private void OnGameEnded(object sender, System.EventArgs e)
        {
            _shuffleButton.Visible = false;
            _startGameButton.Visible = false;
            _newGameButton.Visible = true;
            _computerBoard.ShowShips();
        }




        private void SetupWindow()
        {
            AutoScaleDimensions = new SizeF(8, 19);
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Point, 186);
            Margin = Padding.Empty;
            Text = "SeaBattle.CSharp";
            BackColor = Color.FromArgb(235, 235, 235);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
        }

        private static Button CreateButton(string text, Color backColor)
        {
            var button = new Button
                             {
                                 FlatStyle = FlatStyle.Flat,
                                 ForeColor = Color.White,
                                 BackColor = backColor,
                                 UseVisualStyleBackColor = false,
                                 Size = new Size(40, 40),
                                 Text = text,
                                 Font = new Font("Webdings", 24, FontStyle.Regular, GraphicsUnit.Point),
                                 TextAlign = ContentAlignment.TopCenter,
                             };
            button.FlatAppearance.BorderSize = 0;

            return button;
        }

        private void LayoutControls()
        {
            _humanBoard.Location = new Point(0, 0);
            _computerBoard.Location = new Point(_humanBoard.Right, 0);
            _scoreboard.Location = new Point(25, _humanBoard.Bottom );
            _scoreboard.Width = _computerBoard.Right - 25;
            _newGameButton.Location = new Point(_computerBoard.Right - _newGameButton.Width, _scoreboard.Bottom);
            _startGameButton.Location = _newGameButton.Location;
            _shuffleButton.Location = new Point(_newGameButton.Location.X - _shuffleButton.Width - 25, _newGameButton.Location.Y);

            Controls.AddRange(new Control[]
                                  {
                                      _humanBoard,
                                      _computerBoard,
                                      _scoreboard,
                                      _newGameButton,
                                      _startGameButton,
                                      _shuffleButton
                                  });

            ClientSize = new Size(_computerBoard.Right + 25, _startGameButton.Bottom + 25);
        }
    }
}
