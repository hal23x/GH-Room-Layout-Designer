using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Drawing;

namespace GhRoomLayoutDesigner
{
	public class DesignFile
	{
		#region Public Variables
		[JsonPropertyName("filenote")]
		public string Filenote { get; set; } = string.Empty;
		[JsonPropertyName("baseObject")]
		public DesignObject BaseObject { get; set; }
		[JsonPropertyName("baseFixtures")]
		public List<DesignObject> BaseFixtures { get; set; } = new List<DesignObject>();
		[JsonPropertyName("furniture")]
		public List<DesignObject> Furniture { get; set; } = new List<DesignObject>();
		public string FileName { get; set; } = string.Empty;
		public bool Modified { get; set; } = false;
		#endregion


		#region Public Static Variables
		public static string LastErrorValue = string.Empty;
		#endregion


		#region Public Static Methods
		/// <summary>
		/// Creates a new design file consisting only of the base object with no fixtures or furniture
		/// </summary>
		/// <param name="filename">The name to use for the filename.</param>
		/// <returns>The newly created design file object.</returns>
		public static DesignFile CreateNewDesignFile(string filename)
		{
			DesignFile designFile = new DesignFile
			{
				FileName = filename,
				BaseObject = new DesignObject
				{
					Name = "Base Object",
					X = 0,
					Y = 0,
					Orientation = 0,
					IsFixed = true,
					PerimeterPoints = new PointF[] { new PointF(120, 120) } // Default size of 120x120 inches
				}
			};
			designFile.PrepareDesignObjects();

			return designFile;
		}


		/// <summary>
		/// Loads a DesignFile from a JSON file.
		/// </summary>
		/// <param name="filename">The path to the JSON file to load.</param>
		/// <returns>The loaded DesignFile, or null if an error occurred.</returns>
		public static DesignFile LoadDesignFile(string filename)
		{
			DesignFile designFile = null;

			try
			{
				string fileText = File.ReadAllText(filename);
				designFile = JsonSerializer.Deserialize<DesignFile>(fileText);
				designFile.PrepareDesignObjects();
				designFile.Modified = false;
				designFile.FileName = filename;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				LastErrorValue = ex.ToString();
				designFile = null;
			}

			return designFile;
		}
		#endregion


		#region Public Instance Methods
		/// <summary>
		/// Saves the current DesignFile to a JSON file.
		/// </summary>
		/// <returns>True if the file was successfully saved; otherwise, false.</returns>
		public bool SaveDesignFile()
		{
			bool isModified = Modified;

			try
			{
				Modified = false;
				string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
				File.WriteAllText(FileName, jsonString);
				return true;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				LastErrorValue = ex.ToString();
				Modified = isModified;
				return false;
			}
		}


		/// <summary>
		/// Saves the current DesignFile to a JSON file using the given file name.
		/// </summary>
		/// <param name="fileName">The file name (including path) to use for the design file.</param>
		/// <returns>True if the file was successfully saved; otherwise, false.</returns>
		public bool SaveDesignFile(string fileName)
		{
			FileName = fileName;
			return SaveDesignFile();
		}
		#endregion


		#region Public Methods
		/// <summary>
		/// Returns the selected furniture object.
		/// </summary>
		/// <returns>The selected furniture object, or null if no object is selected.</returns>
		public DesignObject GetSelectedFixtureObject()
		{
			return BaseFixtures?.FirstOrDefault(f => f.IsSelected) ?? null;
		}


		/// <summary>
		/// Returns the selected furniture object.
		/// </summary>
		/// <returns>The selected furniture object, or null if no object is selected.</returns>
		public DesignObject GetSelectedFurnitureObject()
		{
			return Furniture?.FirstOrDefault(f => f.IsSelected) ?? null;
		}


		/// <summary>
		/// Adds a new furniture design object.
		/// </summary>
		/// <returns>The newly created design object.</returns>
		public DesignObject AddNewFixtureObject()
		{
			PointF[] perimeterPoints = new PointF[]
			{
				new PointF(10, 3)
			};
			int itemNumber = BaseFixtures.Count + 1;
			string itemName = "New Fixture " + itemNumber;
			foreach (DesignObject existingFixture in BaseFixtures)
			{
				if (existingFixture.Name == itemName)
				{
					itemNumber++;
					itemName = "New Fixture " + itemNumber;
				}
			}
			SizeF roomSize = BaseObject?.CalculateSize() ?? new SizeF(120, 120);
			DesignObject newFixture = new DesignObject(itemName)
			{
				PerimeterPoints = perimeterPoints,
				X = 0,
				Y = roomSize.Height / 2,
				Orientation = 0,
				IsFixed = true
			};
			BaseFixtures.Add(newFixture);
			newFixture.CalculateCenteredPoints();
			return newFixture;
		}


		/// <summary>
		/// Adds a new furniture design object.
		/// </summary>
		/// <returns>The newly created design object.</returns>
		public DesignObject AddNewFurnitureObject()
		{
			PointF[] perimeterPoints = new PointF[]
			{
				new PointF(100, 50)
			};
			int itemNumber = Furniture.Count + 1;
			string itemName = "New Furniture " + itemNumber;
			foreach (DesignObject existingFurniture in Furniture)
			{
				if (existingFurniture.Name == itemName)
				{
					itemNumber++;
					itemName = "New Furniture " + itemNumber;
				}
			}
			SizeF roomSize = BaseObject?.CalculateSize() ?? new SizeF(120, 120);
			DesignObject newFurniture = new DesignObject(itemName)
			{
				PerimeterPoints = perimeterPoints,
				X = (roomSize.Width - perimeterPoints[0].X) / 2,
				Y = (roomSize.Height - perimeterPoints[0].Y) / 2,
				Orientation = 0
			};
			Furniture.Add(newFurniture);
			newFurniture.CalculateCenteredPoints();
			return newFurniture;
		}


		/// <summary>
		/// Removes the currently selected fixture design object.
		/// </summary>
		public void RemoveSelectedFixtureObject()
		{
			DesignObject selectedObject = GetSelectedFixtureObject();

			if (selectedObject != null)
			{
				BaseFixtures.Remove(selectedObject);
			}
		}


		/// <summary>
		/// Removes the currently selected furniture design object.
		/// </summary>
		public void RemoveSelectedFurnitureObject()
		{
			DesignObject selectedObject = GetSelectedFurnitureObject();

			if (selectedObject != null)
			{
				Furniture.Remove(selectedObject);
			}
		}


		/// <summary>
		/// Prepares all design objects for transformations.
		/// </summary>
		public void PrepareDesignObjects()
		{
			if (BaseObject != null)
			{
				BaseObject.CalculateCenteredPoints();
			}

			if (BaseFixtures != null)
			{
				foreach (DesignObject fixture in BaseFixtures)
				{
					fixture.CalculateCenteredPoints();
				}
			}

			if (Furniture != null)
			{
				foreach (DesignObject furniture in Furniture)
				{
					furniture.CalculateCenteredPoints();
				}
			}
		}
		#endregion
	}
}
