namespace GhRoomLayoutDesigner
{
	partial class DesignLayoutView
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
			this.HorizontalScrollBar = new System.Windows.Forms.HScrollBar();
			this.VerticalScrollBar = new System.Windows.Forms.VScrollBar();
			this.SuspendLayout();
			// 
			// HorizontalScrollBar
			// 
			this.HorizontalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.HorizontalScrollBar.Location = new System.Drawing.Point(0, 346);
			this.HorizontalScrollBar.Name = "HorizontalScrollBar";
			this.HorizontalScrollBar.Size = new System.Drawing.Size(518, 21);
			this.HorizontalScrollBar.TabIndex = 0;
			this.HorizontalScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HorizontalScrollBar_Scroll);
			// 
			// VerticalScrollBar
			// 
			this.VerticalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.VerticalScrollBar.Location = new System.Drawing.Point(518, 0);
			this.VerticalScrollBar.Name = "VerticalScrollBar";
			this.VerticalScrollBar.Size = new System.Drawing.Size(21, 346);
			this.VerticalScrollBar.TabIndex = 1;
			this.VerticalScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.VerticalScrollBar_Scroll);
			// 
			// DesignLayoutView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.VerticalScrollBar);
			this.Controls.Add(this.HorizontalScrollBar);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "DesignLayoutView";
			this.Size = new System.Drawing.Size(539, 367);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.DesignLayoutView_Paint);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DesignLayoutView_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DesignLayoutView_KeyUp);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DesignLayoutView_MouseDown);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DesignLayoutView_MouseMove);
			this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DesignLayoutView_MouseUp);
			this.Resize += new System.EventHandler(this.DesignLayoutView_Resize);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.HScrollBar HorizontalScrollBar;
		private System.Windows.Forms.VScrollBar VerticalScrollBar;
	}
}
