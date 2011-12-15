using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SeatBattle.CSharp
{
    [DebuggerDisplay("({X},{Y}) {_state}")]
    public class BoardCell : Label
    {
        private static readonly Color DefaultBorderColor = Color.FromArgb(214,214,214);
        private static readonly Color DefaultBackgroundColor = Color.FromArgb(222,222,222);
        private static readonly Color DragOverBackgroundColor = Color.FromArgb(255,174,0);
        private static readonly Color DragOverInvalidBackgroundColor = Color.FromArgb(222,0,0);
        private static readonly Color ShipColor = Color.FromArgb(65,133,243);
        private static readonly Color ShipDrownedColor = Color.FromArgb(222, 0, 0);

        private const char ShipHitChar = (char)0x72;
        private const char MissedHitChar = (char)0x3D;



        private BoardCellState _state;

        public BoardCell(int x, int y)
        {
            X = x;
            Y = y;
            base.AutoSize = false;
            base.TextAlign = ContentAlignment.MiddleCenter;
            base.Font = new Font("Webdings", 10);
            base.AllowDrop = true;
        }

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
                case BoardCellState.ShipDrag:
                    BackColor = DragOverBackgroundColor;
                    Text = string.Empty;
                    break;
                case BoardCellState.ShipDragInvalid:
                    BackColor = DragOverInvalidBackgroundColor;
                    Text = string.Empty;
                    break;
                case BoardCellState.ShowDrowned:
                    BackColor = ShipDrownedColor;
                    Text = ShipHitChar.ToString();
                    break;
            }
            Invalidate();
            ResumeLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var pen = new Pen(DefaultBorderColor))
            {
                pen.Alignment = PenAlignment.Inset;
                pen.DashStyle = DashStyle.Solid;

                var rect = ClientRectangle;
                rect.Height -= 1;
                rect.Width -= 1;

                e.Graphics.DrawRectangle(pen, rect);
            }
        }

        public int X { get; private set; }
        public int Y { get; private set; }
    }
}