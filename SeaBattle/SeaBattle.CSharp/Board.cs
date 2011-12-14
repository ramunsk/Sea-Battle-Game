using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SeatBattle.CSharp
{
    public class Board : Control
    {
        private const int CellSize = 25;

        private static readonly Rect BoardRegion = new Rect(0, 0, 10, 10);

        private readonly BoardCell[,] _cells;
        private readonly List<Ship> _ships;
        private DraggableShip _draggedShip;
        private readonly Random _rnd;
        private readonly bool _drawShips;

        public Board():this(true){}

        public Board(bool drawShips)
        {
            _drawShips = drawShips;
            _cells = new BoardCell[10, 10];
            _ships = new List<Ship>();
            _rnd = new Random(DateTime.Now.Millisecond);
            Mode = BoardMode.Design;
            Margin = Padding.Empty;

            CreateBoard();
        }

        /// <summary>
        ///     Creates board header cell ar a given position
        /// </summary>
        /// <param name="x">X coordnate of cell</param>
        /// <param name="y">Y coordinate of cell</param>
        /// <param name="text">Text of cell</param>
        /// <returns></returns>
        private static Label CreateHeaderCell(int x, int y, string text)
        {
            var cell = new Label
                           {
                               AutoSize = false,
                               BackColor = Color.Transparent,
                               TextAlign = ContentAlignment.MiddleCenter,
                               Text = text,
                               Location = new Point(x, y),
                               Width = CellSize,
                               Height = CellSize
                           };
            return cell;
        }

        /// <summary>
        ///     Creates board headers
        /// </summary>
        private void CreateHeaders()
        {
            for (var i = 0; i < BoardRegion.Width; i++)
            {
                var offset = CellSize * i + CellSize;
                var columnHeader = CreateHeaderCell(offset, 0, (i + 1).ToString());
                var rowHeader = CreateHeaderCell(0, offset, (i + 1).ToString());

                Controls.Add(columnHeader);
                Controls.Add(rowHeader);
            }

        }

        /// <summary>
        ///     Creates a board layout
        /// </summary>
        private void CreateBoard()
        {
            SuspendLayout();

            var boardSize = new Size(CellSize * BoardRegion.Width + CellSize, CellSize * BoardRegion.Height + CellSize);
            base.MinimumSize = boardSize;
            base.MaximumSize = boardSize;

            CreateHeaders();

            var points = BoardRegion.GetPoints();

            foreach(var point in points)
            {
                var cell = new BoardCell(point.X, point.Y)
                               {
                                   Top = point.X * CellSize + CellSize,
                                   Left = point.Y * CellSize + CellSize,
                                   Width = CellSize,
                                   Height = CellSize,
                                   State = BoardCellState.Normal
                               };
                _cells[point.X, point.Y] = cell;
                cell.MouseDown += OnCellMouseDown;
                cell.DragEnter += OnCellDragEnter;
                cell.DragLeave += OnCellDragLeave;
                cell.DragDrop += OnCellDragDrop;
                cell.QueryContinueDrag += OnCellQueryContinueDrag;
                cell.Click += OnCellClick;
                Controls.Add(cell);
            }

            ResumeLayout();
        }

        private void OnCellClick(object sender, EventArgs e)
        {
            if (Mode != BoardMode.Game)
                return;

            var handler = OnClick;
            if (handler == null)
                return;

            var cell = (BoardCell)sender;
            var eventArgs = new BoardCellClickEventErgs(cell.X, cell.Y);
            handler(this, eventArgs);
        }

        /// <summary>
        ///     Get a ship a a given location
        /// </summary>
        /// <param name="x">X coordinate to check ship at</param>
        /// <param name="y">y coordinate to check ship at</param>
        /// <returns><see cref="Ship"/></returns>
        private Ship GetShipAt(int x, int y)
        {
            return _ships.FirstOrDefault(ship => ship.IsLocatedAt(x, y));
        }

        /// <summary>
        ///     Handles <see cref="BoardCell"/>'s MouseDown event and initiates ship drag'n'drop    
        /// </summary>
        private void OnCellMouseDown(object sender, MouseEventArgs e)
        {
            if (Mode == BoardMode.Game || !_drawShips)
                return;

            var cell = (BoardCell)sender;
            var ship = GetShipAt(cell.X, cell.Y);

            if (ship == null)
            {
                return;
            }
            _draggedShip = DraggableShip.From(ship);
            cell.DoDragDrop(ship, DragDropEffects.Copy | DragDropEffects.Move);
        }

        /// <summary>
        ///     Gives feedback for ship rotation while dragging it
        /// </summary>
        private void OnCellQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            // check Ctrl key state
            var shouldRotate = ((e.KeyState & 8) == 8);
            var isRotated = _draggedShip.IsOrientationModified;

            if ((shouldRotate && isRotated) || (!shouldRotate && !isRotated))
                return;

            var rect = _draggedShip.GetShipRegion();
            RedrawRegion(rect);

            _draggedShip.Rotate();
            _draggedShip.IsOrientationModified = !isRotated;

            var state = CanPlaceShip(_draggedShip, _draggedShip.X, _draggedShip.Y) ? BoardCellState.ShipDrag : BoardCellState.ShipDragInvalid;
            DrawShip(_draggedShip, state);
        }

        /// <summary>
        ///     Handles <see cref="BoardCell"/>'s DragEnter event
        /// </summary>
        private void OnCellDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Ship)))
            {
                var cell = (BoardCell)sender;
                _draggedShip.MoveTo(cell.X, cell.Y);

                var canPlaceShip = CanPlaceShip(_draggedShip, cell.X, cell.Y);
                var state = canPlaceShip ? BoardCellState.ShipDrag : BoardCellState.ShipDragInvalid;

                DrawShip(_draggedShip, state);

                e.Effect = canPlaceShip ? DragDropEffects.Move : DragDropEffects.None;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        ///     Handles <see cref="BoardCell"/>'s DragLeave event
        /// </summary>
        private void OnCellDragLeave(object sender, EventArgs e)
        {
            var rect = _draggedShip.GetShipRegion();
            RedrawRegion(rect);

        }

        /// <summary>
        ///     Handles <see cref="BoardCell"/>'s DragDrop event
        /// </summary>
        private void OnCellDragDrop(object sender, DragEventArgs e)
        {
            var cell = (BoardCell)sender;
            if (e.Data.GetDataPresent(typeof(Ship)))
            {
                if (!CanPlaceShip(_draggedShip, cell.X, cell.Y))
                    return;

                var ship = _draggedShip.Source;
                _ships.Remove(ship);

                var rect = ship.GetShipRegion();
                RedrawRegion(rect);

                ship.Orientation = _draggedShip.Orientation;

                AddShip(ship, cell.X, cell.Y);
                _draggedShip = null;

            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        ///     Adds a ship on a board
        /// </summary>
        /// <param name="ship">Ship to add</param>
        /// <param name="x">X coordinate of where to add ship</param>
        /// <param name="y">Y coordinate of where to add ship</param>
        public void AddShip(Ship ship, int x, int y)
        {
            ship.MoveTo(x, y);

            _ships.Add(ship);
            DrawShip(ship, BoardCellState.Ship);
        }

        /// <summary>
        ///     Returns true if given shio van be placed at a given location
        /// </summary>
        /// <param name="ship">Ship to check</param>
        /// <param name="x">X coordinate to check</param>
        /// <param name="y">Y coordinate to check</param>
        private bool CanPlaceShip(Ship ship, int x, int y)
        {
            var shipRegion = ship.GetShipRegion();

            shipRegion.MoveTo(x, y);

            if (!BoardRegion.Contains(shipRegion))
                return false;

            shipRegion.Inflate(1, 1);

            foreach (var s in _ships)
            {
                if (ship is DraggableShip && s == ((DraggableShip)ship).Source)
                {
                    continue;
                }

                if (s.GetShipRegion().IntersectsWith(shipRegion))
                {
                    return false;
                }
            }


            return true;
        }

        /// <summary>
        ///     Redraws a given region on a board
        /// </summary>
        /// <param name="region">A region to redraw</param>
        private void RedrawRegion(Rect region)
        {
            SuspendLayout();

            var points = region.GetPoints();
            foreach (var point in points)
            {
                if (!BoardRegion.Contains(point))
                {
                    continue;
                }

                var ship = GetShipAt(point.X, point.Y);
                _cells[point.X, point.Y].State = ship == null ? BoardCellState.Normal : BoardCellState.Ship;
            }

            ResumeLayout();
        }


        public void ShowShips()
        {
            foreach(var ship in _ships)
            {
                var shipPoints = ship.GetShipRegion().GetPoints();

                foreach(var point in shipPoints)
                {
                    var cell = _cells[point.X, point.Y];
                    if (cell.State != BoardCellState.Normal)
                        continue;

                    cell.State = BoardCellState.Ship;                        
                }
            }
        }

        /// <summary>
        ///     Draws a ship on a board
        /// </summary>
        /// <param name="ship">Ship to draw</param>
        /// <param name="state">Ship state to draw</param>
        private void DrawShip(Ship ship, BoardCellState state)
        {
            DrawShip(ship, state, false);
        }

        /// <summary>
        ///     Draws a ship on a board
        /// </summary>
        /// <param name="ship">Ship to draw</param>
        /// <param name="state">Ship state to draw</param>
        /// <param name="force">True to force ship drawing</param>
        private void DrawShip(Ship ship, BoardCellState state, bool force)
        {
            if (!_drawShips && !force)
                return;

            var points = ship.GetShipRegion().GetPoints();

            foreach (var point in points)
            {
                if (BoardRegion.Contains(point))
                {
                    _cells[point.X, point.Y].State = state;
                }
            }
        }

        /// <summary>
        ///     Removes all ships from board
        /// </summary>
        public void ClearBoard()
        {
            SuspendLayout();

            _ships.Clear();

            var points = BoardRegion.GetPoints();
            foreach (var point in points)
            {
                _cells[point.X, point.Y].State = BoardCellState.Normal;
            }

            ResumeLayout();
        }

        /// <summary>
        ///     Adds random ships on a board
        /// </summary>
        public void AddRandomShips()
        {
            SuspendLayout();

            ClearBoard();

            var ships = GetNewShips();

            foreach (var ship in ships)
            {
                var shipAdded = false;

                while (!shipAdded)
                {
                    var x = _rnd.Next(10);
                    var y = _rnd.Next(10);

                    if (!CanPlaceShip(ship, x, y))
                        continue;
                    
                    AddShip(ship, x, y);
                    shipAdded = true;
                }
            }

            ResumeLayout();
        }

        /// <summary>
        ///     Returns a list of ships with random locations. 
        ///     This is usualy used to begin new game     
        /// </summary>
        private IList<Ship> GetNewShips()
        {
            var ships = new List<Ship>
                        {
                            new Ship(4){Orientation = (ShipOrientation)_rnd.Next(2)},
                            new Ship(3){Orientation = (ShipOrientation)_rnd.Next(2)},
                            new Ship(3){Orientation = (ShipOrientation)_rnd.Next(2)},
                            new Ship(2){Orientation = (ShipOrientation)_rnd.Next(2)},
                            new Ship(2){Orientation = (ShipOrientation)_rnd.Next(2)},
                            new Ship(2){Orientation = (ShipOrientation)_rnd.Next(2)},
                            new Ship(1){Orientation = (ShipOrientation)_rnd.Next(2)},
                            new Ship(1){Orientation = (ShipOrientation)_rnd.Next(2)},
                            new Ship(1){Orientation = (ShipOrientation)_rnd.Next(2)},
                            new Ship(1){Orientation = (ShipOrientation)_rnd.Next(2)}
                        };

            return ships;
        }

        public BoardMode Mode { get; set; }


        public ShotResult OpenentShotAt(int x, int y)
        {
            var ship = GetShipAt(x, y);

            if (ship == null)
            {
                _cells[x, y].State = BoardCellState.MissedShot;
                return ShotResult.Missed;
            }
            _cells[x, y].State = BoardCellState.ShotShip;

            ship.HitCount++;

            if (ship.IsDrowned)
                DrawShip(ship, BoardCellState.ShowDrowned, true);

            return ship.IsDrowned ? ShotResult.ShipDrowned : ShotResult.ShipHit;
        }

        public new event EventHandler<BoardCellClickEventErgs> OnClick;

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            Font = Parent.Font;
            Debug.WriteLine(Font.Name);
        }

    }
}
