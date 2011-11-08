using System.Drawing;
using System.Windows.Forms;

namespace SeatBattle.CSharp.GameBoard
{
    public class PlayerBoard : TableLayoutPanel
    {
        private const int DefaultCellHeight = 30;
        private const int DefaultCellWidth = 30;
        private readonly Color DefaultCellBackgroundColor;
        private readonly Color DefaultCellBorderColor;

        //private int _cellWidth;
        //public int CellWidth
        //{
        //    get
        //    {
        //        return _cellWidth;
        //    }
        //    set
        //    {
        //        _cellWidth = value;
        //        Invalidate();
        //    }
        //}

        //private int _cellHeight;
        //public int CellHeight
        //{
        //    get
        //    {
        //        return _cellHeight;
        //    }
        //    set
        //    {
        //        _cellHeight = value;
        //        Invalidate();
        //    }
        //}

        //private Color _cellBorderColor;
        //public Color CellBorderColor
        //{
        //    get
        //    {
        //        return _cellBorderColor;
        //    }
        //    set
        //    {
        //        _cellBorderColor = value;
        //        Invalidate();
        //    }
        //}

        //private Color _cellBackgroundColor;
        //public Color CellBackgroundColor
        //{
        //    get
        //    {
        //        return _cellBackgroundColor;
        //    }
        //    set
        //    {
        //        _cellBackgroundColor = value;
        //        Invalidate();
        //    }
        //}

        public PlayerBoard()
        {
            DefaultCellBackgroundColor = Color.LightBlue;
            DefaultCellBorderColor = Color.CornflowerBlue;

            //CellWidth = DefaultCellWidth;
            //CellHeight = DefaultCellHeight;

            //CellBackgroundColor = DefaultCellBackgroundColor;
            //CellBorderColor = DefaultCellBorderColor;

            ColumnCount = 11;
            RowCount = 11;

            AddCellStyles();
            AddBoardHeaders();

            Width = DefaultCellWidth * 11;
            Height = DefaultCellHeight * 11;

        }

        private void AddBoardHeaders()
        {
            for (var i = 1; i < 11; i++)
            {
                var lblLeft = new Label
                              {
                                  Dock = DockStyle.Fill,
                                  TextAlign = ContentAlignment.MiddleCenter,
                                  Text = i.ToString()
                              };
                Controls.Add(lblLeft, 0, i);

                var lblTop = new Label
                             {
                                 Dock = DockStyle.Fill,
                                 TextAlign = ContentAlignment.MiddleCenter,
                                 Text = ((char)(64 + i)).ToString()
                             };
                Controls.Add(lblTop, i, 0);
            }
        }

        private void AddCellStyles()
        {
            for (var i = 0; i < 11; i++)
            {
                ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, DefaultCellWidth));
                RowStyles.Add(new RowStyle(SizeType.Absolute, DefaultCellHeight));
            }
        }


        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e)
        {
            base.OnCellPaint(e);

            if (e.Row == 0 || e.Column == 0)
                return;


            var rect = e.CellBounds;

            using (var pen = new Pen(DefaultCellBorderColor, 0))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                using (var brush = new SolidBrush(DefaultCellBackgroundColor))
                {
                    if (e.Row == (RowCount - 1))
                        rect.Height -= 1;

                    if (e.Column == (ColumnCount - 1))
                        rect.Width -= 1;

                    e.Graphics.FillRectangle(brush, rect);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            }

        }
    }
}