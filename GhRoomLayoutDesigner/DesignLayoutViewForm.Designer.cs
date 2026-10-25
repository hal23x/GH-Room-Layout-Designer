namespace GhRoomLayoutDesigner
{
	partial class DesignLayoutViewForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DesignLayoutViewForm));
			this.PropertiesPnl = new System.Windows.Forms.Panel();
			this.DesignObjectPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.DesignLayoutViewTS = new System.Windows.Forms.ToolStrip();
			this.TSZoomInBtn = new System.Windows.Forms.ToolStripButton();
			this.TSZoomFitBtn = new System.Windows.Forms.ToolStripButton();
			this.TSZoomOutBtn = new System.Windows.Forms.ToolStripButton();
			this.TSScaleLbl = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.TSRoomDdBtn = new System.Windows.Forms.ToolStripDropDownButton();
			this.TSAddFixtureMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TSRemoveFixtureMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.TSFurnitureDdBtn = new System.Windows.Forms.ToolStripDropDownButton();
			this.TSAddNewFurnitureMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TSRemoveSelectedFurnitureMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.TSPropertiesPanelBtn = new System.Windows.Forms.ToolStripButton();
			this.SelectedItemCbx = new System.Windows.Forms.ComboBox();
			this.PropertiesPnl.SuspendLayout();
			this.DesignLayoutViewTS.SuspendLayout();
			this.SuspendLayout();
			// 
			// PropertiesPnl
			// 
			this.PropertiesPnl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PropertiesPnl.BackColor = System.Drawing.SystemColors.Control;
			this.PropertiesPnl.Controls.Add(this.SelectedItemCbx);
			this.PropertiesPnl.Controls.Add(this.DesignObjectPropertyGrid);
			this.PropertiesPnl.Location = new System.Drawing.Point(500, 28);
			this.PropertiesPnl.Name = "PropertiesPnl";
			this.PropertiesPnl.Size = new System.Drawing.Size(300, 534);
			this.PropertiesPnl.TabIndex = 5;
			// 
			// DesignObjectPropertyGrid
			// 
			this.DesignObjectPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DesignObjectPropertyGrid.Location = new System.Drawing.Point(3, 37);
			this.DesignObjectPropertyGrid.Name = "DesignObjectPropertyGrid";
			this.DesignObjectPropertyGrid.Size = new System.Drawing.Size(294, 494);
			this.DesignObjectPropertyGrid.TabIndex = 0;
			this.DesignObjectPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.DesignObjectPropertyGrid_PropertyValueChanged);
			// 
			// DesignLayoutViewTS
			// 
			this.DesignLayoutViewTS.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.DesignLayoutViewTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSZoomInBtn,
            this.TSZoomFitBtn,
            this.TSZoomOutBtn,
            this.TSScaleLbl,
            this.toolStripSeparator1,
            this.TSRoomDdBtn,
            this.toolStripSeparator3,
            this.TSFurnitureDdBtn,
            this.toolStripSeparator2,
            this.TSPropertiesPanelBtn});
			this.DesignLayoutViewTS.Location = new System.Drawing.Point(0, 0);
			this.DesignLayoutViewTS.Name = "DesignLayoutViewTS";
			this.DesignLayoutViewTS.Size = new System.Drawing.Size(800, 31);
			this.DesignLayoutViewTS.TabIndex = 6;
			this.DesignLayoutViewTS.Text = "toolStrip1";
			// 
			// TSZoomInBtn
			// 
			this.TSZoomInBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TSZoomInBtn.Image = ((System.Drawing.Image)(resources.GetObject("TSZoomInBtn.Image")));
			this.TSZoomInBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSZoomInBtn.Name = "TSZoomInBtn";
			this.TSZoomInBtn.Size = new System.Drawing.Size(29, 28);
			this.TSZoomInBtn.Text = "Zoom In";
			this.TSZoomInBtn.ToolTipText = "Zoom in";
			this.TSZoomInBtn.Click += new System.EventHandler(this.TSZoomInBtn_Click);
			// 
			// TSZoomFitBtn
			// 
			this.TSZoomFitBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TSZoomFitBtn.Image = ((System.Drawing.Image)(resources.GetObject("TSZoomFitBtn.Image")));
			this.TSZoomFitBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSZoomFitBtn.Name = "TSZoomFitBtn";
			this.TSZoomFitBtn.Size = new System.Drawing.Size(29, 24);
			this.TSZoomFitBtn.Text = "Reset Zoom";
			this.TSZoomFitBtn.ToolTipText = "Reset zoom to fit to window size";
			this.TSZoomFitBtn.Click += new System.EventHandler(this.TSZoomFitBtn_Click);
			// 
			// TSZoomOutBtn
			// 
			this.TSZoomOutBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TSZoomOutBtn.Image = ((System.Drawing.Image)(resources.GetObject("TSZoomOutBtn.Image")));
			this.TSZoomOutBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSZoomOutBtn.Name = "TSZoomOutBtn";
			this.TSZoomOutBtn.Size = new System.Drawing.Size(29, 24);
			this.TSZoomOutBtn.Text = "Zoom Out";
			this.TSZoomOutBtn.ToolTipText = "Zoom out";
			this.TSZoomOutBtn.Click += new System.EventHandler(this.TSZoomOutBtn_Click);
			// 
			// TSScaleLbl
			// 
			this.TSScaleLbl.Name = "TSScaleLbl";
			this.TSScaleLbl.Size = new System.Drawing.Size(87, 24);
			this.TSScaleLbl.Text = "Scale: 100%";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
			// 
			// TSRoomDdBtn
			// 
			this.TSRoomDdBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.TSRoomDdBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSAddFixtureMenuItem,
            this.TSRemoveFixtureMenuItem});
			this.TSRoomDdBtn.Image = ((System.Drawing.Image)(resources.GetObject("TSRoomDdBtn.Image")));
			this.TSRoomDdBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSRoomDdBtn.Name = "TSRoomDdBtn";
			this.TSRoomDdBtn.Size = new System.Drawing.Size(120, 24);
			this.TSRoomDdBtn.Text = "Room Settings";
			// 
			// TSAddFixtureMenuItem
			// 
			this.TSAddFixtureMenuItem.Name = "TSAddFixtureMenuItem";
			this.TSAddFixtureMenuItem.Size = new System.Drawing.Size(194, 26);
			this.TSAddFixtureMenuItem.Text = "Add Fixture";
			this.TSAddFixtureMenuItem.Click += new System.EventHandler(this.TSAddFixtureMenuItem_Click);
			// 
			// TSRemoveFixtureMenuItem
			// 
			this.TSRemoveFixtureMenuItem.Name = "TSRemoveFixtureMenuItem";
			this.TSRemoveFixtureMenuItem.Size = new System.Drawing.Size(194, 26);
			this.TSRemoveFixtureMenuItem.Text = "Remove Fixture";
			this.TSRemoveFixtureMenuItem.Click += new System.EventHandler(this.TSRemoveFixtureMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
			// 
			// TSFurnitureDdBtn
			// 
			this.TSFurnitureDdBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.TSFurnitureDdBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSAddNewFurnitureMenuItem,
            this.TSRemoveSelectedFurnitureMenuItem});
			this.TSFurnitureDdBtn.Image = ((System.Drawing.Image)(resources.GetObject("TSFurnitureDdBtn.Image")));
			this.TSFurnitureDdBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSFurnitureDdBtn.Name = "TSFurnitureDdBtn";
			this.TSFurnitureDdBtn.Size = new System.Drawing.Size(81, 24);
			this.TSFurnitureDdBtn.Text = "Furniture";
			// 
			// TSAddNewFurnitureMenuItem
			// 
			this.TSAddNewFurnitureMenuItem.Name = "TSAddNewFurnitureMenuItem";
			this.TSAddNewFurnitureMenuItem.Size = new System.Drawing.Size(269, 26);
			this.TSAddNewFurnitureMenuItem.Text = "Add New Furniture Item";
			this.TSAddNewFurnitureMenuItem.ToolTipText = "Add New Furniture Item";
			this.TSAddNewFurnitureMenuItem.Click += new System.EventHandler(this.TSAddNewFurnitureMenuItem_Click);
			// 
			// TSRemoveSelectedFurnitureMenuItem
			// 
			this.TSRemoveSelectedFurnitureMenuItem.Name = "TSRemoveSelectedFurnitureMenuItem";
			this.TSRemoveSelectedFurnitureMenuItem.Size = new System.Drawing.Size(269, 26);
			this.TSRemoveSelectedFurnitureMenuItem.Text = "Remove Selected Furniture";
			this.TSRemoveSelectedFurnitureMenuItem.Click += new System.EventHandler(this.TSRemoveSelectedFurnitureMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
			// 
			// TSPropertiesPanelBtn
			// 
			this.TSPropertiesPanelBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.TSPropertiesPanelBtn.Image = ((System.Drawing.Image)(resources.GetObject("TSPropertiesPanelBtn.Image")));
			this.TSPropertiesPanelBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TSPropertiesPanelBtn.Name = "TSPropertiesPanelBtn";
			this.TSPropertiesPanelBtn.Size = new System.Drawing.Size(116, 24);
			this.TSPropertiesPanelBtn.Text = "Hide Properties";
			this.TSPropertiesPanelBtn.Click += new System.EventHandler(this.TSPropertiesPanelBtn_Click);
			// 
			// SelectedItemCbx
			// 
			this.SelectedItemCbx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.SelectedItemCbx.FormattingEnabled = true;
			this.SelectedItemCbx.Location = new System.Drawing.Point(3, 3);
			this.SelectedItemCbx.Name = "SelectedItemCbx";
			this.SelectedItemCbx.Size = new System.Drawing.Size(294, 28);
			this.SelectedItemCbx.TabIndex = 8;
			// 
			// DesignLayoutViewForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 562);
			this.Controls.Add(this.DesignLayoutViewTS);
			this.Controls.Add(this.PropertiesPnl);
			this.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "DesignLayoutViewForm";
			this.Text = "Design Layout View";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DesignLayoutViewForm_FormClosing);
			this.PropertiesPnl.ResumeLayout(false);
			this.DesignLayoutViewTS.ResumeLayout(false);
			this.DesignLayoutViewTS.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel PropertiesPnl;
		private System.Windows.Forms.ToolStrip DesignLayoutViewTS;
		private System.Windows.Forms.ToolStripButton TSZoomInBtn;
		private System.Windows.Forms.ToolStripButton TSZoomFitBtn;
		private System.Windows.Forms.ToolStripButton TSZoomOutBtn;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.PropertyGrid DesignObjectPropertyGrid;
		private System.Windows.Forms.ToolStripLabel TSScaleLbl;
		private System.Windows.Forms.ToolStripButton TSPropertiesPanelBtn;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripDropDownButton TSFurnitureDdBtn;
		private System.Windows.Forms.ToolStripMenuItem TSAddNewFurnitureMenuItem;
		private System.Windows.Forms.ToolStripMenuItem TSRemoveSelectedFurnitureMenuItem;
		private System.Windows.Forms.ToolStripDropDownButton TSRoomDdBtn;
		private System.Windows.Forms.ToolStripMenuItem TSAddFixtureMenuItem;
		private System.Windows.Forms.ToolStripMenuItem TSRemoveFixtureMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ComboBox SelectedItemCbx;
	}
}