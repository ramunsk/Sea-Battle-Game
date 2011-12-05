using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SeatBattle.CSharp.GameBoard
{
    public class Board : Control
    {
        private const int BoardHeight = 10;
        private const int BoardWidth = 10;

        private readonly BoardCell[,] _cells;
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

            cell.DoDragDrop(cell, DragDropEffects.Copy | DragDropEffects.Move);
            
        }

        private void OnCellDragEnter(object sender, DragEventArgs e)
        {
            var cell = (BoardCell)sender;
            if (e.Data.GetDataPresent(typeof(BoardCell)))
            {
                e.Effect = DragDropEffects.Move;
                cell.PreviousState = cell.State;
                if (cell.IsValidForNewShip)
                {
                    cell.State = BoardCellState.ShipDrag;
                    cell.Invalidate();
                }
                else
                {
                    cell.State = BoardCellState.ShipDragInvalid;
                    cell.Invalidate();
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void OnCellDragLeave(object sender, EventArgs e)
        {
            var cell = (BoardCell)sender;
            cell.State = cell.PreviousState;
            cell.Invalidate();
        }

        private void OnCellDragDrop(object sender, DragEventArgs e)
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

        private bool CanPlaceShip(Ship ship, int x, int y)
        {
            var wouldFit = true;

            if (ship.Orientation == ShipOrientation.Horizontal)
            {
                wouldFit = (x + ship.Length) < BoardWidth;
            }
            else
            {
                wouldFit = (y + ship.Length) < BoardHeight;
            }

            return wouldFit;
        }

        private void DrawDragableShip(Ship ship, int x, int y)
        {
            var state = CanPlaceShip(ship, x, y) ? BoardCellState.ShipDrag : BoardCellState.ShipDragInvalid;

            var dx = x;
            var dy = y;

            for (var i = 0; i < ship.Length; i++)
            {
                dx = ship.Orientation == ShipOrientation.Horizontal ? dx + 1 : dx;
                dy = ship.Orientation == ShipOrientation.Vertical ? dy + 1 : dy;
            }

            if (dx >= BoardWidth || dy >= BoardHeight)
                return;
            
            var cell = _cells[dx, dy];
            cell.PreviousState = cell.State;
            cell.State = state;
        }

    

        private void ClearDragableShip(Ship ship, int x, int y)
        {

        }



    }
}