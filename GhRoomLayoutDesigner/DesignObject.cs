using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GhRoomLayoutDesigner
{
	public enum DesignObjectShape
	{
		Rectangle,
		Perimeter
	}


	public class DesignObject
	{
		#region Public Configuration Fields
		/// <summary>
		/// Name of the object, used for identification and display purposes.
		/// </summary>
		[JsonPropertyName("name"), Description("Name of the object, used for identification and display purposes.")]
		public string Name { get; set; }
		/// <summary>
		/// X-coordinate of the object's position in the design layout.
		/// </summary>
		[JsonPropertyName("x"), Description("The x-coordinate (left-right direction) of the object's position, in inches."), Category("Location")]
		public float X { get; set; }
		/// <summary>
		/// Y-coordinate of the object's position in the design layout.
		/// </summary>
		[JsonPropertyName("y"), Description("The y-coordinate (top-bottom direction) of the object's position, in inches."), Category("Location")]
		public float Y { get; set; }
		/// <summary>
		/// Orientation angle of the object, referenced from the center of the object, in degrees.
		/// </summary>
		[JsonPropertyName("orientation"), Description("The orientation angle of the object, referenced from the center of the object, in degrees."), Category("Orientation")]
		public double Orientation { get; set; }
		/// <summary>
		/// Shape of the object, which can be used to determine how to render or interact with the object in the design layout.
		/// </summary>
		[JsonPropertyName("shape"), Description("The shape of the object, which can be used to determine how to render or interact with the object in the design layout."), Category("Appearance")]
		public string Shape { get; set; } = "Rectangle";
		/// <summary>
		/// Indicates whether the object has a static (fixed) position.
		/// </summary>
		[JsonPropertyName("isFixed"), Browsable(false)]
		public bool IsFixed { get; set; }
		/// <summary>
		/// The perimeter points array that define the shape of the object.  If only one point is specified, the object is assumed to be a rectangle where the X value is the width and the Y value is the height.
		/// </summary>
		[JsonPropertyName("perimeterPoints"), Description("The perimeter points array that define the shape of the object.  If only one point is specified, the object is assumed to be a rectangle where the X value is the width and the Y value is the height."), Category("Appearance")]
		public PointF[] PerimeterPoints { get; set; }
		#endregion


		#region Public Data Storage Fields
		/// <summary>
		/// The centered points array that define the shape of the object, adjusted so that the center of the object is at origin (0,0).  This is calculated based on the perimeter points and is used for rendering and transformations.  Note that if the perimeter points define a rectangle (ie: use only one set of coordinates indicating width and height), this array will contain the coordinates of all four corners of the rectangle.
		/// </summary>
		[Browsable(false)]
		public PointF[] CenteredPoints { get; set; } = null;
		/// <summary>
		/// The display points array that define the shape of the object for rendering on the screen, adjusted based on the object's position, orientation, and scaling factor.
		/// </summary>
		[Browsable(false)]
		public PointF[] DisplayPoints { get; set; } = null;
		#endregion


		#region Public State Fields
		[Browsable(false)]
		public bool IsSelected { get; private set; } = false;
		#endregion


		#region Public Static Variables
		public static string LastErrorValue = string.Empty;
		#endregion


		#region Private Fields
		#endregion


		#region Private Constant Values
		#endregion


		#region Constructors
		/// <summary>
		/// Default constructor for the design object.
		/// </summary>
		public DesignObject()
		{
			// do nothing
		}


		/// <summary>
		/// A constructor for the design object that initializes the name.
		/// </summary>
		/// <param name="name"></param>
		public DesignObject(string name)
		{
			Name = name;
		}
		#endregion


		#region Public Methods
		/// <summary>
		/// Calculates the centered points of the design object based on its perimeter points, adjusting them to be centered around the origin (0,0).
		/// </summary>
		/// <param name="designObject">The design object for which to calculate the centered points.</param>
		public void CalculateCenteredPoints()
		{
			if (PerimeterPoints == null || PerimeterPoints.Length == 0)
			{
				CenteredPoints = null;
				return;
			}

			SizeF sizeObj = CalculateSize();
			float centerX = sizeObj.Width / -2.0F;
			float centerY = sizeObj.Height / -2.0F;
			int cpCnt = PerimeterPoints.Length > 1 ? PerimeterPoints.Length : 4;

			if (CenteredPoints == null || CenteredPoints.Length != cpCnt)
			{
				CenteredPoints = new PointF[cpCnt];
			}

			if (PerimeterPoints.Length == 1)
			{
				CenteredPoints[0] = new PointF(centerX, centerY);
				CenteredPoints[1] = new PointF(centerX, PerimeterPoints[0].Y + centerY);
				CenteredPoints[2] = new PointF(PerimeterPoints[0].X + centerX, PerimeterPoints[0].Y + centerY);
				CenteredPoints[3] = new PointF(PerimeterPoints[0].X + centerX, centerY);
			}
			else if (PerimeterPoints.Length > 1)
			{
				for (int ptNdx = 0; ptNdx < PerimeterPoints.Length; ptNdx++)
				{
					CenteredPoints[ptNdx] = new PointF(PerimeterPoints[ptNdx].X + centerX, PerimeterPoints[ptNdx].Y + centerY);
				}
			}
		}


		/// <summary>
		/// Calculates the actual size of the object (that is, the total width and height in inches or cm, and not the screen size in pixels).
		/// </summary>
		/// <returns>The size (width and height) of the object, or (-1, -1) if there are no perimeter points defined.</returns>
		public SizeF CalculateSize()
		{
			SizeF objectSize = new SizeF(-1, -1);
			int pointsLen = PerimeterPoints?.Length ?? 0;

			if (pointsLen == 1)
			{
				objectSize.Width = PerimeterPoints[0].X;
				objectSize.Height = PerimeterPoints[0].Y;
			}
			else if (pointsLen > 1)
			{
				float minx = PerimeterPoints[0].X;
				float miny = PerimeterPoints[0].Y;
				float maxx = minx;
				float maxy = miny;

				foreach (var point in PerimeterPoints)
				{
					if (minx > point.X) minx = point.X;
					if (maxx < point.X) maxx = point.X;
					if (miny > point.Y) miny = point.Y;
					if (maxy < point.Y) maxy = point.Y;
				}

				objectSize.Width = maxx - minx;
				objectSize.Height = maxy - miny;
			}

			return objectSize;
		}


		/// <summary>
		/// Determines whether a given point is inside the design object based on its display points.
		/// </summary>
		/// <param name="point">The point to test.</param>
		/// <returns>True if the point is inside the design object; otherwise, false.</returns>
		public bool IsPointInside(PointF point)
		{
			if (DisplayPoints == null || DisplayPoints.Length < 3)
			{
				return false;
			}
			using (var path = new System.Drawing.Drawing2D.GraphicsPath())
			{
				path.AddPolygon(DisplayPoints);
				return path.IsVisible(point);
			}
		}


		/// <summary>
		/// Moves the object the specified amount, scaling the move by the current drawing scaling.
		/// </summary>
		/// <param name="deltaX">The distance to move the object in the x-direction.</param>
		/// <param name="deltaY">The distance to move the object in the y-direction.</param>
		public void MoveObject(float deltaX, float deltaY)
		{
			X += deltaX;
			Y += deltaY;
		}


		/// <summary>
		/// Rotates the object by the specified angle in degrees, adjusting the orientation to remain within the range of 0 to 360 degrees.
		/// </summary>
		/// <param name="deltaAngle">The angle in degrees to rotate the object.</param>
		public void RotateObject(double deltaAngle)
		{
			Orientation += deltaAngle;

			if (Orientation >= 360.0)
			{
				Orientation -= 360.0;
			}
			else if (Orientation < 0.0)
			{
				Orientation += 360.0;
			}
		}


		/// <summary>
		/// Sets the selection state of the object.
		/// </summary>
		/// <param name="isSelected">True to select the object; false to deselect it.</param>
		public void Selected(bool isSelected)
		{
			IsSelected = isSelected;
		}
		#endregion


		#region Private Methods

		#endregion
	}
}
