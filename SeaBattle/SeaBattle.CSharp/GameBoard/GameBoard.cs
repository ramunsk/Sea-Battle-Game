using System.Drawing;
using System.Windows.Forms;

namespace SeatBattle.CSharp.GameBoard
{
    public class GameBoard : Form
    {
        //private readonly PlayerBoard _humanBoard;

        public GameBoard()
        {
            SuspendLayout();
            
            CreateWindowlayout();
            //_humanBoard = new PlayerBoard();
            //Controls.Add(_humanBoard);

            ResumeLayout();
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
