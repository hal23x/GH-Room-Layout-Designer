using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace GhRoomLayoutDesigner
{
	/// <summary>
	/// Represents the main MDI container form for creating, opening, selecting, and saving design layout documents.
	/// </summary>
	/// <remarks>Coordinates DesignLayoutViewForm child windows, keeps the design selection list synchronized with
	/// open documents, and updates status text from active document events.</remarks>
	public partial class Form1 : Form
	{
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="Form1"/> class.
		/// </summary>
		public Form1()
		{
			InitializeComponent();
		}
		#endregion


		#region Non-Event Private Methods
		/// <summary>
		/// Creates and displays a design layout view document window, opening an existing design file or creating a new one
		/// when no path is provided.
		/// </summary>
		/// <remarks>Sets the document window title from the file name, subscribes to document events, shows the
		/// window as an MDI child, and refreshes the design file selection list.</remarks>
		/// <param name="filepath">The full or relative path of the design file to open. If null or empty, a new design file name is generated and a
		/// new design file is created.</param>
		private void AddNewDesignLayoutViewForm(string filepath)
		{
			bool isNewFile = string.IsNullOrEmpty(filepath);
			if (isNewFile)
			{
				filepath = GetNextDesignLayoutViewFormName();
			}
			string titleText = System.IO.Path.GetFileName(filepath);
			DesignLayoutViewForm dlv = new DesignLayoutViewForm {
				MdiParent = this,
				Text = titleText
			};

			if (isNewFile)
			{
				dlv.CreateNewDesignFile(filepath);
			}
			else
			{
				dlv.OpenDesignFile(filepath);
			}

			dlv.DocumentFormClose += new DocumentFormCloseEventHandler(DocumentFormClose);
			dlv.DocumentFormTitleChanged += new DocumentFormTitleChangedEventHandler(DocumentFormTitleChanged);
			dlv.DocumentFormStatusUpdate += new DocumentFormStatusUpdateEventHandler(DocumentFormStatusUpdate);
			dlv.Show();

			UpdateDesignFileComboBox();
		}


		/// <summary>
		/// Handles closing a design layout view form and refreshes the design file selection list.
		/// </summary>
		/// <param name="form">The design layout view form being closed.</param>
		private void DocumentFormClose(DesignLayoutViewForm form)
		{
			UpdateDesignFileComboBox();
		}


		/// <summary>
		/// Handles a document form title change and refreshes the design file combo box.
		/// </summary>
		/// <param name="form">Document form with the updated title.</param>
		/// <param name="newTitle">New title of the document form.</param>
		private void DocumentFormTitleChanged(DesignLayoutViewForm form, string newTitle)
		{
			UpdateDesignFileComboBox();
		}


		/// <summary>
		/// Updates the status label text for the document form.
		/// </summary>
		/// <param name="form">Form associated with the status update context.</param>
		/// <param name="status">Status text to display.</param>
		private void DocumentFormStatusUpdate(DesignLayoutViewForm form, string status)
		{
			TSStatusLbl.Text = status;
		}


		/// <summary>
		/// Returns the currently selected design's file path.
		/// </summary>
		/// <returns>The file path of the current design, or null if no design is selected.</returns>
		private string GetCurrentDesignFilePath()
		{
			return ActiveMdiChild != null ? ((DesignLayoutViewForm)ActiveMdiChild).DesignFilePath : null;
		}


		/// <summary>
		/// Generates the next available design layout view form title using the current number of open
		/// <c>DesignLayoutViewForm</c> MDI child windows and adjusts it if a matching title already exists.
		/// </summary>
		/// <returns>A form title in the format <c>"Design Layout View {n}"</c> that is not currently used by an open
		/// <c>DesignLayoutViewForm</c>.</returns>
		private string GetNextDesignLayoutViewFormName()
		{
			int count = MdiChildren.OfType<DesignLayoutViewForm>().Count();
			string formname = $"Design Layout View {count + 1}";
			foreach (var child in MdiChildren.OfType<DesignLayoutViewForm>())
			{
				if (child.Text == formname)
				{
					count++;
					formname = $"Design Layout View {count + 1}";
				}
			}
			return formname;
		}


		/// <summary>
		/// Updates the main form's selected design file combo box based on all open design files, and sets the text to the currently selected design.
		/// </summary>
		private void UpdateDesignFileComboBox()
		{
			TSDesignFileCbx.BeginUpdate();
			TSDesignFileCbx.Items.Clear();
			string selectedDesignText = string.Empty;
			foreach (var child in MdiChildren.OfType<DesignLayoutViewForm>())
			{
				TSDesignFileCbx.Items.Add(child.Text);
				if (child == ActiveMdiChild)
				{
					selectedDesignText = child.Text;
				}
			}
			TSDesignFileCbx.Text = selectedDesignText;
			TSDesignFileCbx.EndUpdate();
		}
		#endregion


		#region Event Handlers
		/// <summary>
		/// Handles the New File button click by opening a new design layout view.
		/// </summary>
		/// <param name="sender">The source of the click event.</param>
		/// <param name="e">Event data associated with the click event.</param>
		private void TSNewFileBtn_Click(object sender, EventArgs e)
		{
			AddNewDesignLayoutViewForm(string.Empty);
		}


		/// <summary>
		/// Opens a file selection dialog for design layout files and loads the selected file into a new design layout view.
		/// </summary>
		/// <remarks>Initializes the dialog to the current design file location when available.</remarks>
		/// <param name="sender">The object that raised the click event.</param>
		/// <param name="e">The event data.</param>
		private void TSOpenFileBtn_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog
			{
				Filter = "Design Layout Files (*.json)|*.json|All Files (*.*)|*.*",
				Title = "Open Design Layout File"
			};
			string filepath = GetCurrentDesignFilePath();

			if (!string.IsNullOrEmpty(filepath))
			{
				ofd.InitialDirectory = System.IO.Path.GetDirectoryName(filepath);
				ofd.FileName = System.IO.Path.GetFileName(filepath);
			}

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				AddNewDesignLayoutViewForm(ofd.FileName);
			}
		}


		/// <summary>
		/// Handles the "Save As" button click event. Opens a SaveFileDialog to allow the user to specify a new file name and location for the design layout file. If the user selects a file and confirms, the active DesignLayoutViewForm saves the design layout to the specified file path.
		/// </summary>
		/// <param name="sender">A reference to the source button for the event.</param>
		/// <param name="e">An EventArgs that contains the event data.</param>
		private void TSSaveAsBtn_Click(object sender, EventArgs e)
		{
			DesignLayoutViewForm activeForm = ActiveMdiChild as DesignLayoutViewForm;

			if (activeForm != null)
			{
				SaveFileDialog sfd = new SaveFileDialog
				{
					Filter = "Design Layout Files (*.json)|*.json|All Files (*.*)|*.*",
					Title = "Save Design Layout File As"
				};
				string filepath = activeForm.DesignFilePath;

				if (!string.IsNullOrEmpty(filepath))
				{
					sfd.InitialDirectory = System.IO.Path.GetDirectoryName(filepath);
					sfd.FileName = System.IO.Path.GetFileName(filepath);
				}

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					activeForm.SaveDesignFile(sfd.FileName);
				}
			}
		}


		/// <summary>
		/// Handles the "Save" button click event. If the active DesignLayoutViewForm has a valid file path, it saves the design layout to that file. If the file path is empty or null or if the base path doesn't exist, it calls the "Save As" functionality to prompt the user for a new file name and location.
		/// </summary>
		/// <param name="sender">A reference to the source button for the event.</param>
		/// <param name="e">An EventArgs that contains the event data.</param>
		private void TSSaveBtn_Click(object sender, EventArgs e)
		{
			DesignLayoutViewForm activeForm = ActiveMdiChild as DesignLayoutViewForm;

			if (activeForm != null)
			{
				if (string.IsNullOrEmpty(activeForm.DesignFilePath) || !Directory.Exists(Path.GetDirectoryName(activeForm.DesignFilePath)))
				{
					TSSaveAsBtn_Click(sender, e);
				}
				else
				{
					activeForm.SaveDesignFile();
				}
			}
		}


		/// <summary>
		/// Activates the open design layout child form whose title matches the currently selected design file in the combo
		/// box.
		/// </summary>
		/// <param name="sender">The control that raised the SelectedIndexChanged event.</param>
		/// <param name="e">Event data for the SelectedIndexChanged event.</param>
		private void TSDesignFileCbx_SelectedIndexChanged(object sender, EventArgs e)
		{
			Form selectedChild = MdiChildren.OfType<DesignLayoutViewForm>().FirstOrDefault(f => f.Text == TSDesignFileCbx.Text);
			if (selectedChild != null)
			{
				selectedChild.Activate();
			}
		}
		#endregion
	}
}
