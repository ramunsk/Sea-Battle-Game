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
            this.boardCell1 = new SeatBattle.CSharp.GameBoard.BoardCell();
            this.SuspendLayout();
            // 
            // boardCell1
            // 
            this.boardCell1.BackColor = System.Drawing.Color.LightBlue;
            this.boardCell1.Font = new System.Drawing.Font("Webdings", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.boardCell1.Location = new System.Drawing.Point(70, 38);
            this.boardCell1.Name = "boardCell1";
            this.boardCell1.Size = new System.Drawing.Size(25, 25);
            this.boardCell1.TabIndex = 0;
            this.boardCell1.Text = "boardCell1";
            this.boardCell1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.boardCell1);
            this.Name = "tmp";
            this.Text = "tmp";
            this.ResumeLayout(false);

        }

        #endregion

        private GameBoard.BoardCell boardCell1;


    }
}