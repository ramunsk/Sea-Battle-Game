using System;
using System.Collections.Generic;
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
        private readonly List<Ship> _ships;
        private Ship _draggedShip;


        public Board()
        {
            _cells = new BoardCell[10, 10];
            _rowHeaders = new Label[10];
            _columnHeaders = new Label[10];
            _ships = new List<Ship>();

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
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 10; y++)
                {
                    var cell = new BoardCell(x, y)
                                   {
                                       Size = CellSize,
                                       Location = new Point(CellSize.Width * (x + 1), CellSize.Height * (y + 1)),
                                       State = BoardCellState.Normal,
                                       //IsValidForNewShip = true
                                   };
                    _cells[x, y] = cell;
                    cell.MouseDown += OnCellMouseDown;
                    cell.DragEnter += OnCellDragEnter;
                    cell.DragLeave += OnCellDragLeave;
                    cell.DragDrop += OnCellDragDrop;
                    Controls.Add(cell);
                }
            }
        }

        private Ship GetShipAt(int x, int y)
        {
            foreach (var ship in _ships)
            {
                if (ship.IsLocatedAt(x, y))
                    return ship;
            }
            return null;
        }

        private void OnCellMouseDown(object sender, MouseEventArgs e)
        {
            var cell = (BoardCell)sender;
            var ship = GetShipAt(cell.X, cell.Y);

            if (ship == null)
                return;

            _draggedShip = ship;
            cell.DoDragDrop(ship, DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void OnCellDragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(typeof(Ship)))
            {
                var ship = _draggedShip;
                var cell = (BoardCell)sender;

                var canPlaceShip = CanPlaceShip(ship, cell.X, cell.Y);
                var state = canPlaceShip ? BoardCellState.ShipDrag : BoardCellState.ShipDragInvalid;

                DrawDragableShip(ship, cell.X, cell.Y, state);
                
                
                e.Effect = canPlaceShip ? DragDropEffects.Move : DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void OnCellDragLeave(object sender, EventArgs e)
        {
            var cell = (BoardCell)sender;
            ClearDragableShip(_draggedShip, cell.X, cell.Y);
        }

        private void OnCellDragDrop(object sender, DragEventArgs e)
        {
            var cell = (BoardCell)sender;
            if (e.Data.GetDataPresent(typeof(Ship)))
            {

            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            cell.Invalidate();
        }

        public Size CellSize { get { return new Size(25, 25); } }

        public void AddShip(Ship ship, int x, int y)
        {

            for (var i = 0; i < ship.Length; i++)
            {
                var dx = ship.Orientation == ShipOrientation.Horizontal ? x + i : x;
                var dy = ship.Orientation == ShipOrientation.Horizontal ? y : y + i;
                _cells[dx, dy].State = BoardCellState.Ship;

                //for (var j = 0; j < 3; j++)
                //{
                //    for (var k = 0; k < 3; k++)
                //    {
                //        if (j >= 0 && j <= 9 && k >= 0 && k <= 9)
                //            _cells[j, k].IsValidForNewShip = false;
                //    }
                //}
            }
            ship.Location = new Point(x, y);
            _ships.Add(ship);
        }

        private bool CanPlaceShip(Ship ship, int x, int y)
        {
            var wouldFit = true;

            if (ship.Orientation == ShipOrientation.Horizontal)
            {
                wouldFit = (x + ship.Length) <= BoardWidth;
            }
            else
            {
                wouldFit = (y + ship.Length) <= BoardHeight;
            }

            return wouldFit;
        }

        private void DrawDragableShip(Ship ship, int x, int y, BoardCellState state)
        {
            var dx = x;
            var dy = y;

            for (var i = 0; i < ship.Length; i++)
            {
                dx = ship.Orientation == ShipOrientation.Horizontal ? x + i : dx;
                dy = ship.Orientation == ShipOrientation.Vertical ? y + i : dy;
                //_cells[dx, dy].State = state;

                Debug.WriteLine("x={0}, y={1}", dx, dy);

                if (dx >= BoardWidth || dy >= BoardHeight)
                    return;

                var cell = _cells[dx, dy];
                cell.State = state;
            }
        }

        private void ClearDragableShip(Ship ship, int x, int y)
        {
            var dx = x;
            var dy = y;

            for (var i = 0; i < ship.Length; i++)
            {
                dx = ship.Orientation == ShipOrientation.Horizontal ? x + i : dx;
                dy = ship.Orientation == ShipOrientation.Vertical ? y + i : dy;
                //_cells[dx, dy].State = state;

                Debug.WriteLine("x={0}, y={1}", dx, dy);

                if (dx >= BoardWidth || dy >= BoardHeight)
                    return;

                var cell = _cells[dx, dy];
                cell.RestorePreviousState();
            }
        }



    }
}