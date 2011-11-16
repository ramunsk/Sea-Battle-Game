using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SeatBattle.CSharp.GameBoard
{
    public class Board : Control
    {

        private BoardCell[,] _cells;
        private readonly Label[] _rowHeaders;
        private readonly Label[] _columnHeaders;

        public Board()
        {
            _cells = new BoardCell[10, 10];
            _rowHeaders = new Label[10];
            _columnHeaders = new Label[10];

            CreateRowHeaders();
            CreateColumnHeaders();
            CreateBoard();

            base.MinimumSize = new Size(CellSize.Width * 11, CellSize.Height * 11);
            base.MaximumSize = new Size(CellSize.Width * 11, CellSize.Height * 11);
        }




        private void CreateColumnHeaders()
        {
            for (var i = 1; i < 11; i++)
            {
                var label = new Label
                                {
                                    AutoSize = false,
                                    BackColor = BackColor,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Text = i.ToString(),
                                    Location = new Point(CellSize.Width * i, 0),
                                    Size = CellSize
                                };
                _columnHeaders[i - 1] = label;
                Controls.Add(label);

            }

        }

        private void CreateRowHeaders()
        {
            for (var i = 1; i < 11; i++)
            {
                var label = new Label
                                {
                                    AutoSize = false,
                                    BackColor = BackColor,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                    Text = i.ToString(),
                                    Location = new Point(0, CellSize.Height * i),
                                    Size = CellSize
                                };
                _rowHeaders[i - 1] = label;
                Controls.Add(label);
            }

        }

        private void CreateBoard()
        {
            var rand = new Random(DateTime.Now.Millisecond);
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var cell = new BoardCell
                                   {
                                       Size = CellSize,
                                       Location = new Point(CellSize.Height * (i + 1), CellSize.Width * (j + 1)),
                                       State = j + 1 <= 5 ? (BoardCellState)(j+1) : BoardCellState.Normal
                                   };
                    _cells[i, j] = cell;
                    Controls.Add(cell);

                }
            }

        }

        public Size CellSize { get { return new Size(25, 25); } }

    }

    public enum BoardCellState
    {
        Normal,
        MissedShot,
        Ship,
        ShotShip
    }

    public class BoardCell : Label
    {
        private readonly Color _borderColor;

        private const char ShipHitChar = (char)0x72;
        private const char MissedHitChar = (char)0x3D;

        private static readonly Color DefaultBackgroundColor = Color.LightBlue;
        private static readonly Color ShipColor = Color.Orange;


        public BoardCell()
        {
            base.AutoSize = false;
            base.TextAlign = ContentAlignment.MiddleCenter;
            base.BackColor = Color.LightBlue;
            _borderColor = Color.CornflowerBlue;
            base.TextAlign = ContentAlignment.MiddleCenter;
            base.Font = new Font("Webdings", 10);
        }


        private BoardCellState _state;
        public BoardCellState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                OnCellStateChenged();
            }
        }

        private void OnCellStateChenged()
        {
            SuspendLayout();
            switch (_state)
            {
                case BoardCellState.Normal:
                    Text = string.Empty;
                    BackColor = DefaultBackgroundColor;
                    break;
                case BoardCellState.MissedShot:
                    Text = MissedHitChar.ToString();
                    BackColor = DefaultBackgroundColor;
                    break;
                case BoardCellState.Ship:
                    Text = string.Empty;
                    BackColor = ShipColor;
                    break;
                case BoardCellState.ShotShip:
                    Text = ShipHitChar.ToString();
                    BackColor = ShipColor;
                    break;
            }
            ResumeLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (State == BoardCellState.Ship || State == BoardCellState.ShotShip)
                return;
            
            using (var pen = new Pen(_borderColor, 0))
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