using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SeatBattle.CSharp
{
    [DebuggerDisplay("({X},{Y}) {_state}")]
    public class BoardCell : Label
    {
        private static readonly Color DefaultBorderColor = Color.CornflowerBlue;
        private static readonly Color DefaultBackgroundColor = Color.LightBlue;
        private static readonly Color DragOverBorderColor = Color.Orange;
        private static readonly Color DragOverInvalidBorderColor = Color.Red;
        private static readonly Color ShipColor = Color.Orange;

        private const char ShipHitChar = (char)0x72;
        private const char MissedHitChar = (char)0x3D;



        private BoardCellState _state;
        private BoardCellState _previousState;

        public BoardCell(int x, int y)
        {
            X = x;
            Y = y;
            base.AutoSize = false;
            base.TextAlign = ContentAlignment.MiddleCenter;
            base.BackColor = Color.LightBlue;
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
                _previousState = _state;
                _state = value;
                OnCellStateChenged();
            }
        }

        public void RestorePreviousState()
        {
            State = _previousState;
        }

        //public bool IsValidForNewShip { get; set; }

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
                    BackColor = DefaultBackgroundColor;
                    Text = string.Empty;
                    break;
                case BoardCellState.ShipDragInvalid:
                    BackColor = DefaultBackgroundColor;
                    Text = string.Empty;
                    break;
            }
            Invalidate();
            ResumeLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var borderColor = GetBorderColor();

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

        public int X { get; private set; }
        public int Y { get; private set; }
    }
}