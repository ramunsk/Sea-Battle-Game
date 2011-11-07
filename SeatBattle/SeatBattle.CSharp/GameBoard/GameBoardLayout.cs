using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SeatBattle.CSharp.GameBoard
{
    public partial class GameBoard
    {
        private readonly TableLayoutPanel _tlpHuman = new TableLayoutPanel();
        private Label[,] _humanGrid = new Label[10, 10];

        private void LayoutHumanPanel()
        {
            _tlpHuman.ColumnCount = 11;
            _tlpHuman.RowCount = 11;
            _tlpHuman.BackColor = Color.LightBlue;

            _tlpHuman.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            _tlpHuman.RowStyles.Add(new RowStyle(SizeType.Percent, 10));

            for (var i = 0; i < 10; i++)
            {
                _tlpHuman.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 9));
                _tlpHuman.RowStyles.Add(new RowStyle(SizeType.Percent, 9));
            }

            _tlpHuman.CellPaint += _tlpHuman_CellPaint;



            for (var i = 1; i < 11; i++)
            {
                for (var j = 1; j < 11; j++)
                {
                    var pb = new Label
                             {
                                 BackColor = Color.Transparent,
                                 AutoSize = false,
                                 Dock = DockStyle.Fill,
                                 TextAlign = ContentAlignment.MiddleCenter,
                                 Text = string.Format("{0}x{1}", i, j)
                             };

                    _humanGrid[i-1, j-1] = pb;
                    _tlpHuman.Controls.Add(pb, i, j);
                }
                
            }


            _tlpHuman.Width = 500;
            _tlpHuman.Height = 500;


            Controls.Add(_tlpHuman);
        }

        void _tlpHuman_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            var r = e.CellBounds;
            var b = new SolidBrush(Color.LightBlue);
            var panel = (TableLayoutPanel)sender;

            using (var pen = new Pen(Color.CornflowerBlue, 0 /*1px width despite of page scale, dpi, page units*/ ))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                // define border style
                pen.DashStyle = DashStyle.Solid;

                // decrease border rectangle height/width by pen's width for last row/column cell
                if (e.Row == (panel.RowCount - 1))
                {
                    r.Height -= 1;
                }

                if (e.Column == (panel.ColumnCount - 1))
                {
                    r.Width -= 1;
                }

                // use graphics mehtods to draw cell's border
                e.Graphics.DrawRectangle(pen, r);
            }
        }
    }
}
