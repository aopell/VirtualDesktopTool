namespace WebSearchDeskBand
{
    partial class DeskBandUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.desktopLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // desktopLabel
            // 
            this.desktopLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.desktopLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desktopLabel.ForeColor = System.Drawing.Color.White;
            this.desktopLabel.Location = new System.Drawing.Point(0, 0);
            this.desktopLabel.Name = "desktopLabel";
            this.desktopLabel.Size = new System.Drawing.Size(128, 26);
            this.desktopLabel.TabIndex = 1;
            this.desktopLabel.Text = "Desktop 1";
            this.desktopLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.desktopLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.desktopLabel_MouseClick);
            // 
            // DeskBandUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.desktopLabel);
            this.Name = "DeskBandUI";
            this.Size = new System.Drawing.Size(128, 26);
            this.Load += new System.EventHandler(this.DeskBandUI_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label desktopLabel;
    }
}
