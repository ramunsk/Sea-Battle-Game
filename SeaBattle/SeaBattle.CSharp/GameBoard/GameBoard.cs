using System.Drawing;
using System.Windows.Forms;

namespace SeatBattle.CSharp.GameBoard
{
    public class GameBoard : Form
    {
        private readonly Board _humanBoard;

        public GameBoard()
        {
            SuspendLayout();
            
            CreateWindowlayout();
            _humanBoard = new Board();
            Controls.Add(_humanBoard);

            ResumeLayout();

            DoubleClick += GameBoard_DoubleClick;
        }

        void GameBoard_DoubleClick(object sender, System.EventArgs e)
        {
            _humanBoard.AddShip(new Ship(4) {Orientation = ShipOrientation.Horizontal}, 6, 9);
        }

        private void CreateWindowlayout()
        {
            AutoScaleDimensions = new SizeF(8, 19);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 500);
            Font = new Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Point, 186);
            Margin = new Padding(4, 4, 4, 4);
            Text = "SeaBattle.CSharp";
        }
    }
}
