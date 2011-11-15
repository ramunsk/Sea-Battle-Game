using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SeatBattle.CSharp.GameBoard
{
    public class Board : Control
    {

    }

    public class BoardCell : Label
    {
        private readonly Color _borderColor;

        public BoardCell()
        {
            base.AutoSize = false;
            base.TextAlign = ContentAlignment.MiddleCenter;
            base.BackColor = Color.LightBlue;
            _borderColor = Color.CornflowerBlue;
            base.TextAlign = ContentAlignment.MiddleCenter;
            base.Font = new Font("Webdings", 10);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using(var pen = new Pen(_borderColor, 0))
            {
                pen.Alignment = PenAlignment.Inset;
                pen.DashStyle = DashStyle.Solid;

                var rect = ClientRectangle;
                rect.Height -= 1;
                rect.Width -= 1;

                e.Graphics.DrawRectangle(pen, rect);
            }
        }
    }
}