using System;
using System.Drawing;


namespace GhRoomLayoutDesigner
{
	/// <summary>
	/// Represents the method that handles a design object selection event.
	/// </summary>
	/// <param name="designObject">The selected design object.</param>
	public delegate void DesignObjectSelectedEventHandler(DesignObject designObject);


	/// <summary>
	/// Provides 2D rendering, projection, scaling, scrolling, hit testing, and selection behavior for design objects in a
	/// design layout view.
	/// </summary>
	/// <remarks>Maintains drawing state, including scale, base scale, scroll offsets, and graphics context, and
	/// coordinates selection updates with the parent control.</remarks>
	public class FigureDrawing2D
	{
		#region Public Events
		/// <summary>
		/// Occurs when a design object is selected.
		/// </summary>
		public DesignObjectSelectedEventHandler DesignObjectSelected;
		#endregion


		#region Public Variables
		/// <summary>
		/// Gets or sets the scale factor, which is used as a multiplier for the object's coordinates to determine its size on the screen. A scale factor of 1.0 means no scaling, while values greater than 1.0 will enlarge the object and values less than 1.0 will shrink it.
		/// </summary>
		public float Scale { get; set; } = 1.0f;
		/// <summary>
		/// Gets the base scale factor, or the scale factor that represents the original size of the object zoomed to fit the window. This value is set internally and is initialized to 1.0. It can be used to reset the scale factor back to its original value to fit the design to the document window size.
		/// </summary>
		/// <remarks>Set internally; initialized to 1.0.</remarks>
		public float BaseScale { get; private set; } = 1.0f;
		/// <summary>
		/// The horizontal scroll offset, or distance that the object is to the right of the visible drawing area, in pixels.  If negative, then the object's left edge will be to the left of the visible drawing area.
		/// </summary>
		public int ScrollOffsetX { get; set; } = 0;
		/// <summary>
		/// The vertical scroll offset, or distance that the object is below the visible drawing area, in pixels.  If negative, then the object's top edge will be above the visible drawing area.
		/// </summary>
		public int ScrollOffsetY { get; set; } = 0;
		/// <summary>
		/// Gets or sets the graphics context used for drawing.
		/// </summary>
		public Graphics GraphicsContext { get; set; } = null;
		/// <summary>
		/// Gets the parent control that contains this figure drawing.
		/// </summary>
		public DesignLayoutView ParentControl { get; private set; } = null;
		/// <summary>
		/// Gets or sets the pen used for drawing lines.
		/// </summary>
		public Pen LinePen { get; set; } = new Pen(Color.Black);
		/// <summary>
		/// Gets or sets the pen used for drawing selected lines.
		/// </summary>
		public Pen SelectedLinePen { get; set; } = new Pen(Color.Red);
		/// <summary>
		/// Gets or sets the font used for drawing object names.
		/// </summary>
		public Font ObjectNameFont { get; set; } = new Font("Segoe UI", 11.0F);
		/// <summary>
		/// Gets or sets the brush used for drawing object names.
		/// </summary>
		public SolidBrush ObjectNameBrush { get; set; } = new SolidBrush(Color.Black);
		/// <summary>
		/// Gets or sets the string format used for drawing object names.
		/// </summary>
		public StringFormat ObjectNameStringFormat { get; set; } = new StringFormat(StringFormat.GenericTypographic);
		#endregion


		#region Public Static Variables
		/// <summary>
		/// Gets the default margin width, in pixels.
		/// </summary>
		public static int DefaultMarginWidth { get; } = 20;
		/// <summary>
		/// Gets the default margin height, in pixels.
		/// </summary>
		public static int DefaultMarginHeight { get; } = 20;
		#endregion


		#region Public Constant Values
		/// <summary>
		/// Defines the increment value for rotating objects, set to 5 degrees. This value is used to determine the angle by which an object should be rotated when a rotation operation is performed.
		/// </summary>
		public static readonly double RotateIncrement = 5.0F;
		#endregion


		#region Private Variables
		//private PointF[] ObjectDrawPoints = null;
		#endregion


		#region Private Constant Values
		private static readonly char[] PointDelimiters = new char[] { ';' };
		private static readonly char[] ScalarDelimiters = new char[] { ',' };
		#endregion


		#region Constructors
		public FigureDrawing2D(DesignLayoutView control)
		{
			ParentControl = control;
			ResetParameters();
		}
		#endregion


		#region Public Methods
		/// <summary>
		/// Sets up control painting by setting the graphics context and validating object scaling.
		/// </summary>
		/// <param name="gfx">The graphics context to use for painting.</param>
		public void BeginPaint(Graphics gfx)
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  BeginPaint called, graphics context: " + (gfx == null ? "null" : "valid"));
			// --GWH DEBUG
			GraphicsContext = gfx;
			ValidateScaling();
		}


		/// <summary>
		/// Calculates the midpoint of the object.
		/// </summary>
		/// <param name="designObject">The design object for which to calculate the midpoint.</param>
		/// <returns>The midpoint of the object.</returns>
		public PointF CalculateMidpoint(DesignObject designObject)
		{
			SizeF sizeObj = designObject.CalculateSize();
			return new PointF(designObject.X + (sizeObj.Width / 2), designObject.Y + (sizeObj.Height / 2));
		}


		/// <summary>
		/// Creates a <see cref="Graphics"/> object for <c>ParentControl</c> and stores it in <c>GraphicsContext</c>.
		/// </summary>
		/// <remarks>Dispose the returned <see cref="Graphics"/> object when it is no longer needed.</remarks>
		/// <returns>The <see cref="Graphics"/> instance created for <c>ParentControl</c>.</returns>
		public Graphics CreateGraphicsContext()
		{
			GraphicsContext = ParentControl.CreateGraphics();
			return GraphicsContext;
		}


		/// <summary>
		/// Projects the design object to display coordinates, draws its polygon outline, and renders its name centered on the
		/// object when available.
		/// </summary>
		/// <remarks>Uses the selected-line pen when the object is selected; otherwise uses the default line pen. Name
		/// placement is centered at the calculated midpoint and adjusted by the current scale and scroll offsets.</remarks>
		/// <param name="designObject">Design object to project and draw, including display points, selection state, and optional name.</param>
		public void DrawObject(DesignObject designObject)
		{
			ProjectObject(designObject);

			if (designObject.DisplayPoints != null && designObject.DisplayPoints.Length > 0)
			{
				Pen drawPen = designObject.IsSelected ? SelectedLinePen : LinePen;
				GraphicsContext.DrawPolygon(drawPen, designObject.DisplayPoints);
				if (!string.IsNullOrEmpty(designObject.Name))
				{
					SizeF lineRect = GraphicsContext.MeasureString(designObject.Name, ObjectNameFont);
					PointF midpoint = CalculateMidpoint(designObject);
					float lineX = (midpoint.X * Scale) - (lineRect.Width / 2) + ScrollOffsetX;
					float lineY = (midpoint.Y * Scale) - (lineRect.Height / 2) + ScrollOffsetY;
					GraphicsContext.DrawString(designObject.Name, ObjectNameFont, ObjectNameBrush, new PointF(lineX, lineY), ObjectNameStringFormat);
				}
			}
		}


		/// <summary>
		/// Ends control painting by clearing the graphics context.
		/// </summary>
		public void EndPaint()
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  EndPaint called");
			// --GWH DEBUG
			GraphicsContext = null;
		}


		/// <summary>
		/// Finds the design object at the specified coordinates, taking into account the current scroll offsets and scaling factor.
		/// </summary>
		/// <param name="x">The x-coordinate to test.</param>
		/// <param name="y">The y-coordinate to test.</param>
		/// <returns>The design object at the specified coordinates, or null if none is found.</returns>
		public DesignObject FindObjectAtCoordinates(int x, int y)
		{
			DesignObject designObject = null;

			if (ParentControl?.DesignData?.Furniture != null)
			{
				PointF testPoint = new PointF(x, y);

				foreach (DesignObject currObj in ParentControl.DesignData.Furniture)
				{
					if (currObj.IsPointInside(testPoint))
					{
						// TODO:  Handle z-order of objects to select the top-most object.
						designObject = currObj;
						break;
					}
				}
			}

			if (designObject == null &&ParentControl?.DesignData?.BaseFixtures != null)
			{
				PointF testPoint = new PointF(x, y);

				foreach (DesignObject currObj in ParentControl.DesignData.BaseFixtures)
				{
					if (currObj.IsPointInside(testPoint))
					{
						// TODO:  Handle z-order of objects to select the top-most object.
						designObject = currObj;
						break;
					}
				}
			}

			if (designObject == null && ParentControl?.DesignData?.BaseObject != null)
			{
				PointF testPoint = new PointF(x, y);

				DesignObject currObj = ParentControl?.DesignData?.BaseObject;
				if (currObj.IsPointInside(testPoint))
				{
					designObject = currObj;
				}
			}

			return designObject;
		}


		/// <summary>
		/// Returns the currently selected object.
		/// </summary>
		/// <returns>The selected design object, or null if either no objects are present or no object is selected.</returns>
		public DesignObject GetSelectedObject()
		{
			DesignObject selectedObject = null;

			if (ParentControl?.DesignData?.Furniture != null)
			{
				foreach (DesignObject currObj in ParentControl.DesignData.Furniture)
				{
					if (currObj.IsSelected)
					{
						selectedObject = currObj;
						break;
					}
				}
			}

			return selectedObject;
		}


		/// <summary>
		/// Projects the object points based on the specified midpoint and orientation.
		/// </summary>
		/// <param name="designObject">The design object for which to project the data points.</param>
		public void ProjectObject(DesignObject designObject)
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  ProjectObject called for object " + designObject.Name);
			// --GWH DEBUG
			PointF[] pointsBase = designObject.CenteredPoints;
			double orientation = (designObject.Orientation * Math.PI) / 180.0F;
			PointF midpoint = CalculateMidpoint(designObject);
			int pointsCnt = pointsBase?.Length ?? 0;

			if (pointsCnt > 0)
			{
				int pointNdx = 0;

				if (designObject.DisplayPoints == null || designObject.DisplayPoints.Length != pointsCnt)
				{
					designObject.DisplayPoints = new PointF[pointsCnt];
				}

				foreach (PointF currPt in pointsBase)
				{
					double ptDistance = Math.Sqrt((currPt.X * currPt.X) + (currPt.Y * currPt.Y));
					double currAngle = ptDistance == 0.0F ? 0.0F : CorrectAngleQuadrant(currPt.X, currPt.Y, Math.Asin(Math.Abs(currPt.Y / ptDistance))) + orientation;

					designObject.DisplayPoints[pointNdx].X = (((float)(ptDistance * Math.Cos(currAngle)) + midpoint.X) * Scale) + ScrollOffsetX;
					designObject.DisplayPoints[pointNdx].Y = (((float)(0.0F - (ptDistance * Math.Sin(currAngle))) + midpoint.Y) * Scale) + ScrollOffsetY;

					pointNdx++;
				}
			}
		}


		/// <summary>
		/// Resets the scaling factor, graphics context, and scroll offsets to their default values.
		/// </summary>
		public void ResetParameters()
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  ResetParameters called");
			// --GWH DEBUG
			Scale = -1.0F;
			BaseScale = Scale;
			GraphicsContext = null;
			ScrollOffsetX = DefaultMarginWidth;
			ScrollOffsetY = DefaultMarginHeight;
		}


		/// <summary>
		/// Resets the scaling by scaling to the base object.
		/// </summary>
		public void ResetScaling()
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  ResetScaling called");
			// --GWH DEBUG
			ScaleToObject(ParentControl?.DesignData?.BaseObject);
		}


		/// <summary>
		/// Sets the scaling factor based on the size of the base object and the size of the parent control, ensuring that the object fits within the control's dimensions while maintaining its aspect ratio.
		/// </summary>
		/// <param name="designObject">The design object for which to set the scale factor.</param>
		public void ScaleToObject(DesignObject designObject)
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  ScaleToObject called, designObject: " + (designObject == null ? "null" : "valid"));
			// --GWH DEBUG
			Scale = 1.0F;

			if (designObject != null && designObject.PerimeterPoints != null && designObject.PerimeterPoints.Length > 0)
			{
				bool tmpGfx = false;

				if (GraphicsContext == null)
				{
					CreateGraphicsContext();
					tmpGfx = true;
				}

				ProjectObject(designObject);

				SizeF drawableSize = designObject.CalculateSize();
				SizeF ctrlDrawArea = ParentControl.GetDrawableSize();
				float scaleX = (float)(ctrlDrawArea.Width - (DefaultMarginWidth * 2)) / drawableSize.Width;
				float scaleY = (float)(ctrlDrawArea.Height - (DefaultMarginHeight * 2)) / drawableSize.Height;

				Scale = (scaleX < scaleY) ? scaleX : scaleY;

				if (tmpGfx)
				{
					GraphicsContext.Dispose();
					GraphicsContext = null;
				}
			}

			BaseScale = Scale;
			//ScalingUpdated?.Invoke();
		}


		/// <summary>
		/// Sets the specified design object as the selected object, updating the selection state of all objects in the parent control's furniture collection.
		/// </summary>
		/// <param name="designObject">The design object to set as selected.</param>
		public DesignObject SetSelectedObject(DesignObject designObject)
		{
			if (ParentControl?.DesignData?.Furniture != null)
			{
				foreach (DesignObject currObj in ParentControl.DesignData.Furniture)
				{
					currObj.Selected(currObj == designObject);
				}
			}

			if (ParentControl?.DesignData?.BaseFixtures != null)
			{
				foreach (DesignObject currObj in ParentControl.DesignData.BaseFixtures)
				{
					currObj.Selected(currObj == designObject);
				}
			}

			if (ParentControl?.DesignData?.BaseObject != null)
			{
				ParentControl?.DesignData?.BaseObject.Selected(false);
			}

			// TODO:  Have event that updates selected object in parent control, so that it can update the property grid and other UI elements.
			DesignObjectSelected?.Invoke(designObject);

			ParentControl?.Invalidate();

			return designObject;
		}


		/// <summary>
		/// Sets the selected object based on the specified coordinates, finding the design object at those coordinates and updating the selection state accordingly.
		/// </summary>
		/// <param name="x">The X-coordinate of the point to check for a design object.</param>
		/// <param name="y">The Y-coordinate of the point to check for a design object.</param>
		/// <returns>The design object at the specified coordinates, or null if no object is found.</returns>
		public DesignObject SetSelectedObject(int x, int y)
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  SetSelectedObject called with coordinates (" + x + ", " + y + ")");
			// --GWH DEBUG
			DesignObject designObject = FindObjectAtCoordinates(x, y);
			SetSelectedObject(designObject);
			return designObject;
		}


		/// <summary>
		/// Validates the scaling if not set by rescaling to the base object.
		/// </summary>
		public void ValidateScaling()
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  ValidateScaling called");
			// --GWH DEBUG
			if (Scale <= 0.0f)
			{
				ResetScaling();
			}
		}


		/// <summary>
		/// Increases the scaling factor by 10%, effectively zooming in on the design view.
		/// </summary>
		public void ZoomIn()
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  ZoomIn called");
			// --GWH DEBUG
			Scale *= 1.1f;
			//ScalingUpdated?.Invoke();
		}


		/// <summary>
		/// Decreases the scaling factor by 10%, effectively zooming out on the design view.
		/// </summary>
		public void ZoomOut()
		{
			// GWH DEBUG--
			System.Diagnostics.Debug.WriteLine(Environment.TickCount.ToString() + ":  ZoomOut called");
			// --GWH DEBUG
			Scale /= 1.1f;
			//ScalingUpdated?.Invoke();
		}
		#endregion


		#region Private Methods
		/// <summary>
		/// Corrects the angle based on the quadrant of the point (x, y).
		/// </summary>
		/// <param name="x">The X-coordinate of the point.</param>
		/// <param name="y">The Y-coordinate of the point.</param>
		/// <param name="angle">The angle to be corrected.</param>
		/// <returns>The corrected angle based on the quadrant of the point (x, y).</returns>
		private double CorrectAngleQuadrant(float x, float y, double angle)
		{
			double outangle = angle;

			if (x < 0.0F)
			{
				if (y > 0.0F)
				{
					outangle += Math.PI;
				}
				else
				{
					outangle = Math.PI - angle;
				}
			}
			else if (y > 0.0F)
			{
				outangle = (Math.PI * 2.0F) - angle;
			}

			return outangle;
		}
		#endregion
	}

	internal class DesignObjectSelectedEventArgs
	{
		private DesignObject designObject;

		public DesignObjectSelectedEventArgs(DesignObject designObject)
		{
			this.designObject = designObject;
		}

		public DesignObject DesignObject
		{
			get { return designObject; }
		}
	}
}
