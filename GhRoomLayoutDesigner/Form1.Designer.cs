namespace GhRoomLayoutDesigner
{
	partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.TSNewFileBtn = new System.Windows.Forms.ToolStripButton();
			this.TSOpenFileBtn = new System.Windows.Forms.ToolStripButton();
			this.TSSaveBtn = new System.Windows.Forms.ToolStripButton();
			this.TSSaveAsBtn = new System.Windows.Forms.ToolStripButton();
			this.TSReloadBtn = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.TSDesignFileCbx = new System.Windows.Forms.ToolStripComboBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.TSStatusPbr = new System.Windows.Forms.ToolStripProgressBar();
			this.TSStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSNewFileBtn,
            this.TSOpenFileBtn,
            this.TSSaveBtn,
            this.TSSaveAsBtn,
            this.TSReloadBtn,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.TSDesignFileCbx});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(871, 28);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// TSNewFileBtn
			// 
			this.TSNewFileBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TSNewFileBtn.Image = ((System.Drawing.Image)(resources.GetObject("TSNewFileBtn.Image")));
			this.TSNewFileBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSNewFileBtn.Name = "TSNewFileBtn";
			this.TSNewFileBtn.Size = new System.Drawing.Size(29, 25);
			this.TSNewFileBtn.Text = "Create New File";
			this.TSNewFileBtn.ToolTipText = "Opens a document window with a new, unpopulated file";
			this.TSNewFileBtn.Click += new System.EventHandler(this.TSNewFileBtn_Click);
			// 
			// TSOpenFileBtn
			// 
			this.TSOpenFileBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TSOpenFileBtn.Image = ((System.Drawing.Image)(resources.GetObject("TSOpenFileBtn.Image")));
			this.TSOpenFileBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSOpenFileBtn.Name = "TSOpenFileBtn";
			this.TSOpenFileBtn.Size = new System.Drawing.Size(29, 28);
			this.TSOpenFileBtn.Text = "Open Existing File";
			this.TSOpenFileBtn.ToolTipText = "Opens an open file dialog to select an existing file";
			this.TSOpenFileBtn.Click += new System.EventHandler(this.TSOpenFileBtn_Click);
			// 
			// TSSaveBtn
			// 
			this.TSSaveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TSSaveBtn.Image = ((System.Drawing.Image)(resources.GetObject("TSSaveBtn.Image")));
			this.TSSaveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSSaveBtn.Name = "TSSaveBtn";
			this.TSSaveBtn.Size = new System.Drawing.Size(29, 28);
			this.TSSaveBtn.Text = "Save File";
			this.TSSaveBtn.ToolTipText = "Save the current design to a file";
			this.TSSaveBtn.Click += new System.EventHandler(this.TSSaveBtn_Click);
			// 
			// TSSaveAsBtn
			// 
			this.TSSaveAsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TSSaveAsBtn.Image = ((System.Drawing.Image)(resources.GetObject("TSSaveAsBtn.Image")));
			this.TSSaveAsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSSaveAsBtn.Name = "TSSaveAsBtn";
			this.TSSaveAsBtn.Size = new System.Drawing.Size(29, 28);
			this.TSSaveAsBtn.Text = "Save Design As...";
			this.TSSaveAsBtn.ToolTipText = "Opens a file save dialog to save the current design to a new file";
			this.TSSaveAsBtn.Click += new System.EventHandler(this.TSSaveAsBtn_Click);
			// 
			// TSReloadBtn
			// 
			this.TSReloadBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TSReloadBtn.Image = ((System.Drawing.Image)(resources.GetObject("TSReloadBtn.Image")));
			this.TSReloadBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSReloadBtn.Name = "TSReloadBtn";
			this.TSReloadBtn.Size = new System.Drawing.Size(29, 25);
			this.TSReloadBtn.Text = "Reload Design";
			this.TSReloadBtn.ToolTipText = "Reloads the current design from the design file";
			this.TSReloadBtn.Click += new System.EventHandler(this.TSReloadBtn_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(82, 28);
			this.toolStripLabel1.Text = "Design File";
			// 
			// TSDesignFileCbx
			// 
			this.TSDesignFileCbx.Name = "TSDesignFileCbx";
			this.TSDesignFileCbx.Size = new System.Drawing.Size(400, 31);
			this.TSDesignFileCbx.ToolTipText = "The current design file";
			this.TSDesignFileCbx.SelectedIndexChanged += new System.EventHandler(this.TSDesignFileCbx_SelectedIndexChanged);
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSStatusPbr,
            this.TSStatusLbl});
			this.statusStrip1.Location = new System.Drawing.Point(0, 462);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(871, 30);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// TSStatusPbr
			// 
			this.TSStatusPbr.Name = "TSStatusPbr";
			this.TSStatusPbr.Size = new System.Drawing.Size(100, 22);
			// 
			// TSStatusLbl
			// 
			this.TSStatusLbl.Name = "TSStatusLbl";
			this.TSStatusLbl.Size = new System.Drawing.Size(37, 24);
			this.TSStatusLbl.Text = "Idle.";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(871, 492);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.IsMdiContainer = true;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "Form1";
			this.Text = "Form1";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton TSOpenFileBtn;
		private System.Windows.Forms.ToolStripButton TSSaveBtn;
		private System.Windows.Forms.ToolStripButton TSSaveAsBtn;
		private System.Windows.Forms.ToolStripButton TSReloadBtn;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox TSDesignFileCbx;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripProgressBar TSStatusPbr;
		private System.Windows.Forms.ToolStripStatusLabel TSStatusLbl;
		private System.Windows.Forms.ToolStripButton TSNewFileBtn;
	}
}

