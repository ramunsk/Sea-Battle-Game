using System.Drawing;
using System.Windows.Forms;

namespace SeatBattle.CSharp.GameBoard
{
    public partial class PlayerBoardOld : TableLayoutPanel
    {
        private const int DefaultCellHeight = 30;
        private const int DefaultCellWidth = 30;
        private readonly Color DefaultCellBackgroundColor;
        private readonly Color DefaultCellBorderColor;
        private readonly Label[,] _cells;


        public PlayerBoardOld()
        {
            DefaultCellBackgroundColor = Color.LightBlue;
            DefaultCellBorderColor = Color.CornflowerBlue;

            ColumnCount = 11;
            RowCount = 11;

            _cells = new Label[10,10];

            AddCellStyles();
            AddBoardHeaders();
            AddBoardElements();

            Width = DefaultCellWidth * 11;
            Height = DefaultCellHeight * 11;

        }

        private void AddBoardElements()
        {
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var label = new Label
                                    {
                                        Dock = DockStyle.Fill, 
                                        AutoSize = false,
                                        Margin = new Padding(0)
                                    };
                    label.Click += label_Click;

                    Controls.Add(label, i + 1, j + 1);
                    _cells[i, j] = label;
                }
            }

        }

        void label_Click(object sender, System.EventArgs e)
        {
            ((Label)sender).BackColor = Color.Red;
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