using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SeatBattle.CSharp.GameBoard
{
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