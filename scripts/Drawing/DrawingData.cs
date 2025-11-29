using System.Collections.Generic;
using Godot;

public class DrawingData
{
    private List<DrawingLayer> _drawingLayers = new List<DrawingLayer>();
    
    public List<Stroke> GetStrokes(int layerIndex) => _drawingLayers[layerIndex].GetStrokes();

    public void SetDrawingData(List<DrawingLayer> drawingLayers)
    {
        _drawingLayers = drawingLayers;
    }

    public void AddDrawingLayer()
    {
        _drawingLayers.Add(new DrawingLayer());
    }

    public void AddStroke(Stroke stroke, int index)
    {
        _drawingLayers[index].AddStroke(stroke);
    }
}