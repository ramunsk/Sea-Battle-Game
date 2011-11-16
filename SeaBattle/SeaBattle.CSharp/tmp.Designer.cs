namespace SeatBattle.CSharp
{
    partial class tmp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.board1 = new SeatBattle.CSharp.GameBoard.Board();
            this.SuspendLayout();
            // 
            // board1
            // 
            this.board1.Location = new System.Drawing.Point(12, 12);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(383, 293);
            this.board1.TabIndex = 0;
            this.board1.Text = "board1";
            // 
            // tmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 390);
            this.Controls.Add(this.board1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "tmp";
            this.Text = "tmp";
            this.ResumeLayout(false);

        }

        #endregion

        private GameBoard.Board board1;



    }
}