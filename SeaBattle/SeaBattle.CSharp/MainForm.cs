using System.Drawing;
using System.Windows.Forms;

namespace SeatBattle.CSharp
{
    public class MainForm : Form
    {
        private readonly Board _humanBoard;

        public MainForm()
        {
            SuspendLayout();
            
            CreateWindowlayout();
            _humanBoard = new Board();
            Controls.Add(_humanBoard);

            ResumeLayout();

            DoubleClick += OnDoubleClick;
        }

        void OnDoubleClick(object sender, System.EventArgs e)
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
