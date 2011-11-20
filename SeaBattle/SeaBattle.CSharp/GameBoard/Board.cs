using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var cell = new BoardCell
                                   {
                                       Size = CellSize,
                                       Location = new Point(CellSize.Height * (i + 1), CellSize.Width * (j + 1)),
                                       State = BoardCellState.Normal,
                                       IsValidForNewShip = true
                                   };
                    _cells[i, j] = cell;
                    cell.MouseDown += OnCellMouseDown;
                    cell.DragEnter += OnCellDragEnter;
                    cell.DragLeave += OnCellDragLeave;
                    cell.DragDrop += OnCellDragDrop;

                    Controls.Add(cell);
                }
            }
        }

        private void OnCellMouseDown(object sender, MouseEventArgs e)
        {
            var cell = (BoardCell)sender;
            if (cell.State != BoardCellState.Ship)
                return;

            Debug.WriteLine("DragDrop started");
            cell.DoDragDrop(cell, DragDropEffects.Copy | DragDropEffects.Move);
            
        }

        void OnCellDragEnter(object sender, DragEventArgs e)
        {
            var cell = (BoardCell)sender;
            if (e.Data.GetDataPresent(typeof(BoardCell)))
            {
                e.Effect = DragDropEffects.Move;
                cell.PreviousState = cell.State;
                if (cell.IsValidForNewShip)
                {
                    Debug.WriteLine("ShipDrag");
                    cell.State = BoardCellState.ShipDrag;
                    cell.Invalidate();
                }
                else
                {
                    Debug.WriteLine("ShipDragInvalid");
                    cell.State = BoardCellState.ShipDragInvalid;
                    cell.Invalidate();
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        void OnCellDragLeave(object sender, EventArgs e)
        {
            var cell = (BoardCell)sender;
            cell.State = cell.PreviousState;
            cell.Invalidate();
        }

        void OnCellDragDrop(object sender, DragEventArgs e)
        {
            var cell = (BoardCell)sender;
            if (e.Data.GetDataPresent(typeof(BoardCell)))
            {
                if (cell.IsValidForNewShip)
                {
                    cell.State = BoardCellState.Ship;
                    cell.PreviousState = BoardCellState.Normal;
                    
                    // TODO: Do drop logics here
                }
                else
                {
                    cell.State = cell.PreviousState;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            cell.Invalidate();
        }


        public Size CellSize { get { return new Size(25, 25); } }

        public void AddShip(Ship ship)
        {

            var x = ship.Location.X - 1;
            var y = ship.Location.Y - 1;

            if (x < 0 || x > 9 || y < 0 || y > 9)
                throw new Exception("Bad ship location");

            for (var i = 0; i < ship.Length; i++)
            {
                var dx = ship.Orientation == ShipOrientation.Horizontal ? x + i : x;
                var dy = ship.Orientation == ShipOrientation.Horizontal ? y : y + i;
                _cells[dx, dy].State = BoardCellState.Ship;

                for (var j = 0; j < 3; j++)
                {
                    for (var k = 0; k < 3; k++)
                    {
                        if (j >= 0 && j <= 9 && k >= 0 && k <= 9)
                            _cells[j, k].IsValidForNewShip = false;
                    }
                }
            }
        }

    }

    public enum BoardCellState
    {
        Normal,
        MissedShot,
        Ship,
        ShotShip,
        ShipDrag,
        ShipDragInvalid
    }

    public class BoardCell : Label
    {
        private static readonly Color DefaultBorderColor = Color.CornflowerBlue;
        private static readonly Color DefaultBackgroundColor = Color.LightBlue;

        private static readonly Color DragOverBorderColor = Color.Orange;
        private static readonly Color DragOverInvalidBorderColor = Color.Red;

        private static readonly Color ShipColor = Color.Orange;

        private const char ShipHitChar = (char)0x72;
        private const char MissedHitChar = (char)0x3D;

        public BoardCell()
        {
            base.AutoSize = false;
            base.TextAlign = ContentAlignment.MiddleCenter;
            base.BackColor = Color.LightBlue;
            base.Font = new Font("Webdings", 10);
            base.AllowDrop = true;
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

        public BoardCellState PreviousState { get; set; }

        public bool IsValidForNewShip { get; set; }

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

            var borderColor = GetBorderColor();
            Debug.WriteLine(borderColor);

            using (var pen = new Pen(borderColor))
            {
                pen.Alignment = PenAlignment.Inset;
                pen.DashStyle = DashStyle.Solid;

                var rect = ClientRectangle;
                rect.Height -= 1;
                rect.Width -= 1;

                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        private Color GetBorderColor()
        {
            if (State == BoardCellState.ShipDrag)
                return DragOverBorderColor;

            if (State == BoardCellState.ShipDragInvalid)
                return DragOverInvalidBorderColor;

            return DefaultBorderColor;
        }

    }
}