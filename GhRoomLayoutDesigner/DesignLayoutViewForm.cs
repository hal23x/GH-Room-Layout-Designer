using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GhRoomLayoutDesigner
{
	public delegate void DocumentFormCloseEventHandler(DesignLayoutViewForm form);
	public delegate void DocumentFormTitleChangedEventHandler(DesignLayoutViewForm form, string newTitle);
	public delegate void DocumentFormStatusUpdateEventHandler(DesignLayoutViewForm form, string status);

	public partial class DesignLayoutViewForm : Form
	{
		public DocumentFormCloseEventHandler DocumentFormClose;
		public DocumentFormTitleChangedEventHandler DocumentFormTitleChanged;
		public DocumentFormStatusUpdateEventHandler DocumentFormStatusUpdate;

		public string DesignFilePath {
			get { return DesignLayoutViewControl.DesignData?.FileName; }
			set { if (DesignLayoutViewControl.DesignData != null) DesignLayoutViewControl.DesignData.FileName = value; }
		}

		private readonly DesignLayoutView DesignLayoutViewControl = new DesignLayoutView();

		public DesignLayoutViewForm()
		{
			InitializeComponent();

			DesignLayoutViewControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			DesignLayoutViewControl.Location = new Point(0, DesignLayoutViewTS.ClientSize.Height);
			DesignLayoutViewControl.Size = new Size(ClientSize.Width - PropertiesPnl.Width, ClientSize.Height - DesignLayoutViewTS.ClientSize.Height);
			DesignLayoutViewControl.DesignObjectSelectionEvent += new DesignObjectSelectionEventHandler(DesignObjectSelectionHandler);
			DesignLayoutViewControl.DesignObjectTitleChangedEvent += new DesignObjectTitleChangedEventHandler(DesignObjectTitleChangedHandler);
			DesignLayoutViewControl.DesignObjectPropertyChangedEvent += new DesignObjectPropertyChangedEventHandler(DesignObjectPropertyChangedHandler);
			DesignLayoutViewControl.LayoutViewScaleChangedEvent += new LayoutViewScaleChangedEventHandler(UpdateScaleValue);
			Controls.Add(DesignLayoutViewControl);
		}


		#region Public Methods
		/// <summary>
		/// Creates a new design file with the specified filename and default base object.
		/// </summary>
		/// <param name="filename">The filename to use for the design object.</param>
		public void CreateNewDesignFile(string filename)
		{
			DesignLayoutViewControl.DesignData = DesignFile.CreateNewDesignFile(filename);
			DocumentFormStatusUpdate?.Invoke(this, "Created new design file");
		}


		/// <summary>
		/// Opens a design file and loads its contents into the DesignLayoutView.
		/// </summary>
		/// <param name="filepath">The path to the design file to open.</param>
		/// <returns>True if the file was successfully opened; otherwise, false.</returns>
		public bool OpenDesignFile(string filepath)
		{
			if (string.IsNullOrEmpty(filepath))
			{
				DocumentFormStatusUpdate?.Invoke(this, "No file selected");
				return false;
			}

			DesignLayoutViewControl.DesignData = DesignFile.LoadDesignFile(filepath);
			DocumentFormStatusUpdate?.Invoke(this, $"Opened design file \"{filepath}\"");
			Text = Path.GetFileName(filepath);

			return true;
		}


		/// <summary>
		/// Reloads the current design file from disk when a valid file path is available.
		/// </summary>
		/// <remarks>Updates the design data and form title after a successful reload. Raises a status update message
		/// for both successful reload and missing file scenarios.</remarks>
		public void ReloadDesignFile()
		{
			string filepath = DesignLayoutViewControl.DesignData?.FileName;

			if (!string.IsNullOrEmpty(filepath) && File.Exists(filepath))
			{
				DesignLayoutViewControl.DesignData = DesignFile.LoadDesignFile(filepath);
				DocumentFormStatusUpdate?.Invoke(this, $"Reloaded design file \"{filepath}\"");
				Text = Path.GetFileName(filepath);
				DesignLayoutViewControl.ResetViewParameters();
			}
			else
			{
				DocumentFormStatusUpdate?.Invoke(this, "No file selected to reload");
			}
		}


		/// <summary>
		/// Saves the current design file for the loaded design data.
		/// </summary>
		/// <remarks>Raises a status update message indicating success, failure, or missing design data. On success,
		/// updates the form title to the saved file name.</remarks>
		/// <returns><see langword="true"/> when the design file is saved successfully; otherwise, <see langword="false"/>.</returns>
		public bool SaveDesignFile()
		{
			bool retVal = false;

			if (DesignLayoutViewControl.DesignData == null)
			{
				DocumentFormStatusUpdate?.Invoke(this, "No design data to save");
			}
			else
			{
				string filename = DesignLayoutViewControl.DesignData.FileName;

				if (DesignLayoutViewControl.DesignData.SaveDesignFile())
				{
					DocumentFormStatusUpdate?.Invoke(this, $"Saved design file \"{filename}\"");
					Text = Path.GetFileName(DesignLayoutViewControl.DesignData.FileName);

					retVal = true;
				}
				else
				{
					DocumentFormStatusUpdate?.Invoke(this, $"Failed to save design file \"{filename}\"");
				}
			}

			return retVal;
		}


		public bool SaveDesignFile(string filepath)
		{
			bool retVal = false;

			if (string.IsNullOrEmpty(filepath))
			{
				DocumentFormStatusUpdate?.Invoke(this, "No file selected");
				return false;
			}
			else if (DesignLayoutViewControl.DesignData == null)
			{
				DocumentFormStatusUpdate?.Invoke(this, "No design data to save");
			}
			else if (DesignLayoutViewControl.DesignData.SaveDesignFile(filepath))
			{
				DocumentFormStatusUpdate?.Invoke(this, $"Saved design file \"{filepath}\"");
				retVal = true;
			}
			else
			{
				DocumentFormStatusUpdate?.Invoke(this, $"Failed to save design file \"{filepath}\"");
			}

			return retVal;
		}
		#endregion


		#region Private Methods
		/// <summary>
		/// Handles the event when a property of a design object changes, updating the property grid to reflect the new values.
		/// </summary>
		/// <param name="designObject">The design object whose property has changed.</param>
		public void DesignObjectPropertyChangedHandler(DesignObject designObject)
		{
			SetSelectedItem(designObject);
		}


		/// <summary>
		/// Handles the selection of a design object in the DesignLayoutView and updates the property grid accordingly.
		/// </summary>
		/// <param name="designObject">The design object that was selected.</param>
		private void DesignObjectSelectionHandler(DesignObject designObject)
		{
			SetSelectedItem(designObject);
			DocumentFormStatusUpdate?.Invoke(this, designObject != null ? $"Selected: {designObject.Name}" : "No object selected");
		}


		private void DesignObjectTitleChangedHandler(DesignObject designObject, string newTitle)
		{
			string formTitle = Path.GetFileName(DesignLayoutViewControl.DesignData?.FileName ?? string.Empty);
			DocumentFormTitleChanged?.Invoke(this, formTitle);
			Text = formTitle + (DesignLayoutViewControl.DesignData?.Modified == true ? " [Modified]" : string.Empty);
		}


		/// <summary>
		/// Handles the click event for the zoom in button on the toolbar, invoking the ZoomIn method of the DesignLayoutView.
		/// </summary>
		/// <param name="sender">A reference to the button that was clicked.</param>
		/// <param name="e">Event data associated with the click event.</param>
		private void TSZoomInBtn_Click(object sender, EventArgs e)
		{
			DesignLayoutViewControl.ZoomIn();
		}


		/// <summary>
		/// Handles the click event for the zoom fit button on the toolbar, invoking the ZoomFit method of the DesignLayoutView.
		/// </summary>
		/// <param name="sender">A reference to the button that was clicked.</param>
		/// <param name="e">Event data associated with the click event.</param>
		private void TSZoomFitBtn_Click(object sender, EventArgs e)
		{
			DesignLayoutViewControl.ZoomFit();
		}


		/// <summary>
		/// Handles the click event for the zoom out button on the toolbar, invoking the ZoomOut method of the DesignLayoutView.
		/// </summary>
		/// <param name="sender">A reference to the button that was clicked.</param>
		/// <param name="e">Event data associated with the click event.</param>
		private void TSZoomOutBtn_Click(object sender, EventArgs e)
		{
			DesignLayoutViewControl.ZoomOut();
		}


		/// <summary>
		/// Handles the PropertyValueChanged event of the DesignObjectPropertyGrid, refreshing the DesignLayoutView to reflect any changes made to the properties of the selected design object.
		/// </summary>
		/// <param name="s">The source of the event.</param>
		/// <param name="e">The event data.</param>
		private void DesignObjectPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (DesignObjectPropertyGrid.SelectedObject != null)
			{
				//DesignLayoutViewControl.DesignData?.PrepareDesignObjects();
				((DesignObject)DesignObjectPropertyGrid.SelectedObject).CalculateCenteredPoints();
				DesignLayoutViewControl.Refresh();
			}
		}


		/// <summary>
		/// Updates the selected item text and property grid selection.
		/// </summary>
		/// <remarks>When null, clears the selected item text and removes the selected object from the property
		/// grid.</remarks>
		/// <param name="designObject">The design object to select, or null to clear the current selection.</param>
		private void SetSelectedItem(DesignObject designObject)
		{
			if (designObject != null)
			{
				SelectedItemCbx.Text = designObject.Name;
				DesignObjectPropertyGrid.SelectedObject = designObject;
			}
			else
			{
				SelectedItemCbx.Text = string.Empty;
				DesignObjectPropertyGrid.SelectedObject = null;
			}
		}


		//private void UpdateSelectedItemCbxItems()
		//{
		//	SelectedItemCbx.Items.Clear();
		//	if (DesignLayoutViewControl.DesignData != null)
		//	{
		//		foreach (var designObject in DesignLayoutViewControl.DesignData.DesignObjects)
		//		{
		//			SelectedItemCbx.Items.Add(designObject.Name);
		//		}
		//	}
		//}


		/// <summary>
		/// Updates the scale value displayed in the toolbar label to reflect the current zoom level of the DesignLayoutView.
		/// </summary>
		/// <param name="scale">The current zoom level of the DesignLayoutView.</param>
		private void UpdateScaleValue(float scale)
		{
			TSScaleLbl.Text = $"Zoom: {scale * 100:F0}%";
		}
		#endregion


		#region Event Handlers
		private void TSPropertiesPanelBtn_Click(object sender, EventArgs e)
		{
			if (DesignObjectPropertyGrid.Visible)
			{
				DesignObjectPropertyGrid.Visible = false;
				PropertiesPnl.Width = 0;
				TSPropertiesPanelBtn.Text = "Show Properties";
				DesignLayoutViewControl.Size = new Size(ClientSize.Width, ClientSize.Height - DesignLayoutViewTS.ClientSize.Height);
			}
			else
			{
				DesignObjectPropertyGrid.Visible = true;
				PropertiesPnl.Width = 300;
				TSPropertiesPanelBtn.Text = "Hide Properties";
				DesignLayoutViewControl.Size = new Size(ClientSize.Width - PropertiesPnl.Width, ClientSize.Height - DesignLayoutViewTS.ClientSize.Height);
			}
		}


		private void TSAddNewFurnitureMenuItem_Click(object sender, EventArgs e)
		{
			DesignLayoutViewControl.AddNewFurnitureObject();
		}

		private void TSRemoveSelectedFurnitureMenuItem_Click(object sender, EventArgs e)
		{
			DesignLayoutViewControl.RemoveSelectedFurnitureObject();
		}

		private void TSAddFixtureMenuItem_Click(object sender, EventArgs e)
		{
			DesignLayoutViewControl.AddNewFixtureObject();
		}

		private void TSRemoveFixtureMenuItem_Click(object sender, EventArgs e)
		{
			DesignLayoutViewControl.RemoveSelectedFixtureObject();
		}


		/// <summary>
		/// Handles the form's closing event by checking for unsaved design changes, prompting to save, and canceling closure
		/// when needed.
		/// </summary>
		/// <remarks>If unsaved changes exist, a confirmation prompt is shown. Choosing to save attempts to persist
		/// the design and cancels closing if saving fails. Choosing Cancel also cancels closing.</remarks>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">Provides data for the form closing event, including a value that determines whether the close operation is
		/// canceled.</param>
		private void DesignLayoutViewForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DesignLayoutViewControl.DesignData?.Modified == true)
			{
				DialogResult result = MessageBox.Show($"The design file \"{Text}\" has unsaved changes. Do you want to save before closing?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
				if (result == DialogResult.Yes)
				{
					if (!SaveDesignFile())
					{
						e.Cancel = true; // Cancel closing if save failed
					}
				}
				else if (result == DialogResult.Cancel)
				{
					e.Cancel = true; // Cancel closing
				}
			}
		}
		#endregion
	}
}
