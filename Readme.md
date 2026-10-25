
# GH Room Layout Designer

A Windows Forms based room layout designer.  This is a re-write to the GWHCAD program, taking advantage of multiple document interface (MDI), storing design files as JSON, and allowing full creation and object property updates from the UI.

## Repository Structure

The repository directory and file structure is as follows.


## Conversion from GHX2DCAD

Corresponding Objects between projects:

| GhRoomLayoutDesigner | GHX2DCAD              | Notes/Comment |
|----------------------|-----------------------|---------------|
| DesignFile           | DesignFile            | New version is JSON versus the old style layout |
| DesignLayoutView     | GHX2DControl          | Drawing area for the designed objects |
| DesignLayoutViewForm | None                  | This is a container form for the MDI interface, containing the DesignLayoutView and object parameters controls |
| DesignObject         | GHX2DObject           | New version has annotations for using with JSON framework |
| DrawingParameters    | GHXGraphicsParameters | Parameters for drawing on the DesignLayoutView/GHX2DControl control |
| Form1                | Form1                 | No notes |


## TODO

The following item need to be done.

 * Title change setting updates
    * Add a modified indicator to the DesignFile
    * Change "modified" title change indicator to use a method that looks at the DesignFile's modified indicator
    * Add modified trigger event when a paramter is changed or the object is moved
    * Add clear modified event when design file is saved

## Future Feature Additions

The following needs to be added as future features, not necessary for first release.

 * Undo/redo states
 * Custom properties panel to make changes easier/more intuitive

