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

        private static readonly Color ButtonBackColor = Color.FromArgb(65, 133, 243);
        private const char ShuffleCharacter = (char)0x60;
        private const char StartGameCharacter = (char)0x34;
        private const char NewGameCharacter = (char)0x6C;


        private Button CreateButton(string text, Color backColor)
        {
            var button = new Button
                             {
                                 FlatStyle = FlatStyle.Flat,
                                 ForeColor = Color.White,
                                 BackColor = backColor,
                                 UseVisualStyleBackColor = false,
                                 Size = new Size(40,40),
                                 Text = text,
                                 Font = new Font("Webdings", 24, FontStyle.Regular, GraphicsUnit.Point),
                                 TextAlign = ContentAlignment.TopCenter,
                             };
            button.FlatAppearance.BorderSize = 0;

            return button;
        }

        public MainForm()
        {
            SuspendLayout();

            _humanBoard = new Board();
            _computerBoard = new Board(false);

            _humanPlayer = new HumanPlayer("Human", _computerBoard);
            _computerPlayer = new ComputerPlayer("Computer");


            _scoreboard = new ScoreBoard(_humanPlayer, _computerPlayer, 10, 100);
            _controller = new GameController(_humanPlayer, _computerPlayer, _humanBoard, _computerBoard, _scoreboard);


            CreateWindowlayout();


            Controls.Add(_humanBoard);
            Controls.Add(_computerBoard);

            _computerBoard.Location = new Point(_humanBoard.Right, 0);




            _scoreboard.Location = new Point(25, _humanBoard.Bottom);
            _scoreboard.Width = _humanBoard.Width + _computerBoard.Width - 25;
            Controls.Add(_scoreboard);






            _shuffleButton = CreateButton(NewGameCharacter.ToString(), ButtonBackColor);
            

            _startGameButton = CreateButton(StartGameCharacter.ToString(), ButtonBackColor);
            _shuffleButton.Location = new Point(_scoreboard.Right - 40, _scoreboard.Bottom);
            Controls.Add(_shuffleButton);




            _humanBoard.AddRandomShips();
            _computerBoard.AddRandomShips();

            _humanBoard.Mode = BoardMode.Game;
            _computerBoard.Mode = BoardMode.Game;

            _scoreboard.FullReset();

            _controller.StartGame();

            ResumeLayout();

            //_humanBoard.AddShip(new Ship(4) { Orientation = ShipOrientation.Horizontal }, 6, 9);
            //_humanBoard.AddShip(new Ship(1) {Orientation = ShipOrientation.Vertical}, 0, 0);
            //_humanBoard.AddShip(new Ship(3) { Orientation = ShipOrientation.Vertical }, 5, 5);

            DoubleClick += OnDoubleClick;
        }

        void OnDoubleClick(object sender, System.EventArgs e)
        {
            //_humanBoard.ClearBoard();
            //_humanBoard.AddRandomShips();
            //_humanBoard.Mode = BoardMode.Game;


            //var ship = new Ship(4) {Orientation = ShipOrientation.Horizontal};
            //_humanBoard.AddShip(ship, 5, 5);
        }

        private void CreateWindowlayout()
        {
            AutoScaleDimensions = new SizeF(8, 19);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 500);
            Font = new Font("Calibri", 12, FontStyle.Regular, GraphicsUnit.Point, 186);
            Margin = new Padding(4, 4, 4, 4);
            Text = "SeaBattle.CSharp";
            BackColor = Color.FromArgb(235, 235, 235);
        }
    }
}
