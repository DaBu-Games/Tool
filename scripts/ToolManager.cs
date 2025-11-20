using Godot;
using System;

public partial class ToolManager : Node
{
    private DrawingData drawingData = new DrawingData();
    
    public DrawingData GetDrawingData() => drawingData;
    
    public void SetDrawingData(DrawingData data)
    {
        this.drawingData = data;
    }
}
