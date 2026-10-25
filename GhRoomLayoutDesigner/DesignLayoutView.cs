using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GhRoomLayoutDesigner
{
	public delegate void DesignObjectSelectionEventHandler(DesignObject designObject);
	public delegate void DesignObjectTitleChangedEventHandler(DesignObject designObject, string newTitle);
	public delegate void DesignObjectPropertyChangedEventHandler(DesignObject designObject);
	public delegate void LayoutViewScaleChangedEventHandler(float newScale);


	public partial class DesignLayoutView : UserControl
	{
		public DesignObjectSelectionEventHandler DesignObjectSelectionEvent;
		public DesignObjectTitleChangedEventHandler DesignObjectTitleChangedEvent;
		public DesignObjectPropertyChangedEventHandler DesignObjectPropertyChangedEvent;
		public LayoutViewScaleChangedEventHandler LayoutViewScaleChangedEvent;

		public DesignFile DesignData { get; set; }

		private FigureDrawing2D FigureDrawing { get; set; }
		private bool ControlKeyPressed { get; set; } = false;
		private Point LastMouseCoords = Point.Empty;
		private bool Modified { get; set; } = false;
		private DesignObject CurrentSelectedObject { get; set; } = null;


		#region Constructor
		public DesignLayoutView()
		{
			FigureDrawing = new FigureDrawing2D(this);
			FigureDrawing.DesignObjectSelected += new DesignObjectSelectedEventHandler(DesignObjectSelected);

			InitializeComponent();

			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			SetStyle(ControlStyles.ResizeRedraw, true);

			MouseWheel += new MouseEventHandler(DesignLayoutView_MouseWheelEvent);
		}
		#endregion


		#region Public Methods
		//public void ScalingUpdated()
		//{
		//	// TODO:  UPDATE SCALING?!

		//	//SizeF drawableSize = DesignData.BaseObject.CalculateSize();
		//	SizeF ctrlDrawArea = GetDrawableSize();

		//	VerticalScrollBar.Maximum = 100;
		//	VerticalScrollBar.Minimum = 0;
		//	VerticalScrollBar.LargeChange = 10;
		//	VerticalScrollBar.SmallChange = 1;
		//	VerticalScrollBar.Value = FigureDrawing.ScrollOffsetY / (int)ctrlDrawArea.Height;

		//	HorizontalScrollBar.Maximum = 100;
		//	HorizontalScrollBar.Minimum = 0;
		//	HorizontalScrollBar.LargeChange = 10;
		//	HorizontalScrollBar.SmallChange = 1;
		//	HorizontalScrollBar.Value = FigureDrawing.ScrollOffsetX / (int)ctrlDrawArea.Width;
		//}


		/// <summary>
		/// Adds a new furniture design object.
		/// </summary>
		/// <returns>The newly created design object.</returns>
		public DesignObject AddNewFixtureObject()
		{
			DesignObject newFixture = DesignData?.AddNewFixtureObject();
			SelectDesignObject(newFixture);
			return newFixture;
		}


		/// <summary>
		/// Adds a new furniture design object.
		/// </summary>
		/// <returns>The newly created design object.</returns>
		public DesignObject AddNewFurnitureObject()
		{
			DesignObject newFurniture = DesignData?.AddNewFurnitureObject();
			SelectDesignObject(newFurniture);
			return newFurniture;
		}


		/// <summary>
		/// Invokes the DesignObjectSelectionEvent when a design object is selected.
		/// </summary>
		/// <param name="designObject">The design object that was selected.</param>
		public void DesignObjectSelected(DesignObject designObject)
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  DesignObjectSelected called");
			// --GWH DEBUG
			DesignObjectSelectionEvent?.Invoke(designObject);
		}


		/// <summary>
		/// Gets the drawable size of the control, accounting for scrollbars and margins.
		/// </summary>
		/// <returns>A Size representing the drawable area of the control.</returns>
		public Size GetDrawableSize()
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  GetDrawableSize called");
			// --GWH DEBUG
			return new Size(ClientSize.Width - VerticalScrollBar.Width - 1, ClientSize.Height - HorizontalScrollBar.Height - 1);
		}


		/// <summary>
		/// Removes the currently selected fixture design object.
		/// </summary>
		public void RemoveSelectedFixtureObject()
		{
			DesignData?.RemoveSelectedFixtureObject();
			SelectDesignObject(null);
		}


		/// <summary>
		/// Removes the currently selected furniture design object.
		/// </summary>
		public void RemoveSelectedFurnitureObject()
		{
			DesignData?.RemoveSelectedFurnitureObject();
			SelectDesignObject(null);
		}


		/// <summary>
		/// Sets the specified design object as selected.
		/// </summary>
		/// <param name="designObject">The design object to set as selected.</param>
		public void SelectDesignObject(DesignObject designObject)
		{
			CurrentSelectedObject = designObject;
			FigureDrawing.SetSelectedObject(designObject);
		}


		/// <summary>
		/// Sets the design data for the view and prepares the design objects.
		/// </summary>
		/// <param name="designData">The design data to set.</param>
		//public void SetDesignData(DesignFile designData)
		//{
		//	DesignData = designData;
		//	designData.PrepareDesignObjects();
		//}


		/// <summary>
		/// Resets the zoom level to fit the entire design within the view and invalidates the control to force a redraw.
		/// </summary>
		public void ZoomFit()
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  ZoomFit called");
			// --GWH DEBUG
			FigureDrawing.ResetScaling();
			Invalidate();
			LayoutViewScaleChangedEvent?.Invoke(FigureDrawing.Scale / FigureDrawing.BaseScale);
		}


		/// <summary>
		/// Zooms in on the design view by increasing the scale and invalidates the control to force a redraw.
		/// </summary>
		public void ZoomIn()
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  ZoomIn called");
			// --GWH DEBUG
			FigureDrawing.ZoomIn();

			Invalidate();
			LayoutViewScaleChangedEvent?.Invoke(FigureDrawing.Scale / FigureDrawing.BaseScale);
		}


		/// <summary>
		/// Zooms out of the design view by decreasing the scale and invalidates the control to force a redraw.
		/// </summary>
		public void ZoomOut()
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  ZoomOut called");
			// --GWH DEBUG
			FigureDrawing.ZoomOut();
			Invalidate();
			LayoutViewScaleChangedEvent?.Invoke(FigureDrawing.Scale / FigureDrawing.BaseScale);
		}
		#endregion


		#region Private Methods
		/// <summary>
		/// Resets the scrollbar limits and value based on the current window size.
		/// </summary>
		private void ResetScrollBars()
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  ResetScrollBars called");
			// --GWH DEBUG
			SizeF ctrlDrawArea = GetDrawableSize();

			VerticalScrollBar.Maximum = 100;
			VerticalScrollBar.Minimum = 0;
			VerticalScrollBar.LargeChange = FigureDrawing.Scale > 0 ? (int)(ctrlDrawArea.Height * FigureDrawing.Scale) : 1;
			VerticalScrollBar.SmallChange = (int)(VerticalScrollBar.LargeChange / 10);
			VerticalScrollBar.Value = FigureDrawing.ScrollOffsetY / (int)ctrlDrawArea.Height;

			HorizontalScrollBar.Maximum = 100;
			HorizontalScrollBar.Minimum = 0;
			HorizontalScrollBar.LargeChange = FigureDrawing.Scale > 0 ? (int)(ctrlDrawArea.Width * FigureDrawing.Scale) : 1;
			HorizontalScrollBar.SmallChange = (int)(HorizontalScrollBar.LargeChange / 10);
			HorizontalScrollBar.Value = FigureDrawing.ScrollOffsetX / (int)ctrlDrawArea.Width;
		}
		#endregion


		#region Event Handlers
		/// <summary>
		/// Handles the Paint event of the DesignLayoutView control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">A PaintEventArgs that contains the event data.</param>
		private void DesignLayoutView_Paint(object sender, PaintEventArgs e)
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  DesignLayoutView_Paint called");
			// --GWH DEBUG
			FigureDrawing.BeginPaint(e.Graphics);

			if (DesignData != null)
			{
				// GWH DEBUG--
				System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  DesignLayoutView_Paint drawing design data");
				// --GWH DEBUG
				if (DesignData.BaseObject != null)
				{
					// GWH DEBUG--
					System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  DesignLayoutView_Paint drawing base object");
					// --GWH DEBUG
					FigureDrawing.DrawObject(DesignData.BaseObject);
				}

				if (DesignData.BaseFixtures != null)
				{
					// GWH DEBUG--
					System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  DesignLayoutView_Paint drawing base fixtures (" + DesignData.BaseFixtures.Count + " total)");
					// --GWH DEBUG
					foreach (DesignObject fixture in DesignData.BaseFixtures)
					{
						FigureDrawing.DrawObject(fixture);
					}
				}

				if (DesignData.Furniture != null)
				{
					// GWH DEBUG--
					System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  DesignLayoutView_Paint drawing furniture (" + DesignData.Furniture.Count + " total)");
					// --GWH DEBUG
					foreach (DesignObject furniture in DesignData.Furniture)
					{
						FigureDrawing.DrawObject(furniture);
					}
				}
			}

			FigureDrawing.EndPaint();
		}


		/// <summary>
		/// Handles the key-down event for the design layout view and records when the Control key is pressed.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">Key event data associated with the key press.</param>
		private void DesignLayoutView_KeyDown(object sender, KeyEventArgs e)
		{
			// TODO
			if (e.KeyCode == Keys.ControlKey)
			{
				ControlKeyPressed = true;
			}
		}


		/// <summary>
		/// Handles the key release event for the design layout view and clears the Control key state when the Control key is
		/// released.
		/// </summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event data containing information about the released key.</param>
		private void DesignLayoutView_KeyUp(object sender, KeyEventArgs e)
		{
			// TODO
			if (e.KeyCode == Keys.ControlKey)
			{
				ControlKeyPressed = false;
			}
		}


		/// <summary>
		/// Handles the MouseDown event of the DesignLayoutView control. Selects the current design object at the mouse coordinats (if the mouse is within an object's bounds) and records the mouse coordinates when the mouse button was pressed.
		/// </summary>
		/// <param name="sender">A reference to the object that triggered this event.</param>
		/// <param name="e">The event arguments.</param>
		private void DesignLayoutView_MouseDown(object sender, MouseEventArgs e)
		{
			// GWH DEBUG--
			//System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  DesignLayoutView_MouseDown called");
			// --GWH DEBUG
			CurrentSelectedObject = FigureDrawing.SetSelectedObject(e.X, e.Y);
			LastMouseCoords.X = e.X;
			LastMouseCoords.Y = e.Y;
		}


		/// <summary>
		/// Handles mouse movement over the design surface, moving the selected unfixed object while the left button is
		/// pressed and triggering related UI updates.
		/// </summary>
		/// <remarks>Updates the last known mouse coordinates on every move. When dragging an unfixed selected object,
		/// applies movement scaled by the current drawing scale, raises the design-object property change notification, marks
		/// the design as modified, updates the title indicator, and requests a redraw.</remarks>
		/// <param name="sender">The object that raised the event.</param>
		/// <param name="e">The mouse event data, including pointer position and button state.</param>
		private void DesignLayoutView_MouseMove(object sender, MouseEventArgs e)
		{
			// GWH DEBUG--
			//System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  DesignLayoutView_MouseMove called");
			// --GWH DEBUG
			bool forceRedraw = false;

			if (e.Button == MouseButtons.Left)
			{
				if (CurrentSelectedObject != null && !CurrentSelectedObject.IsFixed)
				{
					float deltaX = (e.X - LastMouseCoords.X) / FigureDrawing.Scale;
					float deltaY = (e.Y - LastMouseCoords.Y) / FigureDrawing.Scale;

					CurrentSelectedObject.MoveObject(deltaX, deltaY);
					DesignObjectPropertyChangedEvent?.Invoke(CurrentSelectedObject);

					if (!Modified)
					{
						Modified = true;
						DesignObjectTitleChangedEvent?.Invoke(CurrentSelectedObject, CurrentSelectedObject.Name + " *");
					}

					forceRedraw = true;
				}
			}

			LastMouseCoords.X = e.X;
			LastMouseCoords.Y = e.Y;

			if (forceRedraw)
			{
				Invalidate();
			}
		}


		/// <summary>
		/// Records the mouse coordinates when the mouse button is released in the design layout view.
		/// </summary>
		/// <param name="sender">The object that raised the event.</param>
		/// <param name="e">The mouse event data containing the pointer position at release time.</param>
		private void DesignLayoutView_MouseUp(object sender, MouseEventArgs e)
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  DesignLayoutView_MouseUp called");
			// --GWH DEBUG
			LastMouseCoords.X = e.X;
			LastMouseCoords.Y = e.Y;
		}


		/// <summary>
		/// Handles mouse wheel input to zoom the layout when Ctrl is pressed or rotate the selected object when Ctrl is not
		/// pressed.
		/// </summary>
		/// <remarks>When zooming, the view is invalidated and the scale-changed event is raised. When rotating a
		/// selected object, the object is rotated by the configured increment, modification state is updated, related change
		/// events are raised, and the view is invalidated.</remarks>
		/// <param name="sender">The object that raised the mouse wheel event.</param>
		/// <param name="e">Mouse event data that provides the wheel delta used to determine zoom or rotation direction and amount.</param>
		private void DesignLayoutView_MouseWheelEvent(object sender, MouseEventArgs e)
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  DesignLayoutView_MouseWheelEvent called");
			// --GWH DEBUG
			if (ControlKeyPressed)
			{
				if (e.Delta > 0)
				{
					FigureDrawing.ZoomIn();
				}
				else if (e.Delta < 0)
				{
					FigureDrawing.ZoomOut();
				}
				Invalidate();
				LayoutViewScaleChangedEvent?.Invoke(FigureDrawing.Scale / FigureDrawing.BaseScale);
				// TODO:  UPDATE SCROLLBARS!
			}
			else
			{
				DesignObject selectedObject = FigureDrawing.GetSelectedObject();

				if (selectedObject != null)
				{
					selectedObject.RotateObject((e.Delta / 120) * FigureDrawing2D.RotateIncrement);

					if (!Modified)
					{
						Modified = true;
						DesignObjectTitleChangedEvent?.Invoke(selectedObject, selectedObject.Name + " *");
					}

					DesignObjectPropertyChangedEvent?.Invoke(selectedObject);
					Invalidate();
				}
			}
		}


		/// <summary>
		/// Resizes the figure then then invalidates the object to force a redraw.
		/// </summary>
		/// <param name="sender">A reference to the object that triggered this event.</param>
		/// <param name="e">The event arguments.</param>
		private void DesignLayoutView_Resize(object sender, EventArgs e)
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  DesignLayoutView_Resize called");
			// --GWH DEBUG
			//FigureDrawing.ResetScaling();
			ResetScrollBars();
			Invalidate();
		}


		/// <summary>
		/// Updates the vertical drawing offset from the current scrollbar position and invalidates the control to trigger a
		/// redraw.
		/// </summary>
		/// <remarks>Computes the offset using the drawable height and the scrollbar maximum, then applies the default
		/// top margin.</remarks>
		/// <param name="sender">The source of the scroll event.</param>
		/// <param name="e">Provides scroll event data, including the new scrollbar value.</param>
		private void VerticalScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  VerticalScrollBar_Scroll called");
			// --GWH DEBUG
			FigureDrawing.ScrollOffsetY = FigureDrawing2D.DefaultMarginWidth - (int)(GetDrawableSize().Height * ((float)e.NewValue / VerticalScrollBar.Maximum));
			Invalidate();
		}


		/// <summary>
		/// Updates the horizontal drawing offset from the scroll bar position and requests a redraw.
		/// </summary>
		/// <param name="sender">The object that raised the scroll event.</param>
		/// <param name="e">The scroll event data, including the new scroll value used to calculate the horizontal offset.</param>
		private void HorizontalScrollBar_Scroll(object sender, ScrollEventArgs e)
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  HorizontalScrollBar_Scroll called");
			// --GWH DEBUG
			FigureDrawing.ScrollOffsetX = (int)(GetDrawableSize().Width * ((float)e.NewValue / HorizontalScrollBar.Maximum)) + FigureDrawing2D.DefaultMarginHeight;
			Invalidate();
		}
		#endregion
	}
}