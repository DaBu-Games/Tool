using System;
using System.Collections.Generic;
using Godot;

public class DrawingData
{
    private List<DrawingLayer> _drawingLayers = new List<DrawingLayer>();
    private int _layerIndex = 0;
    public event Action OnChanged;
    
    public List<Stroke> GetStrokes() => _drawingLayers[_layerIndex].GetStrokes();

    public void SetDrawingData(List<DrawingLayer> drawingLayers)
    {
        _drawingLayers = drawingLayers;
    }

    public void AddDrawingLayer()
    {
        _drawingLayers.Add(new DrawingLayer());
    }

    public void AddStroke(Stroke stroke)
    {
        _drawingLayers[_layerIndex].AddStroke(stroke);
        OnChanged?.Invoke();
    }

    public void RemoveStroke(Stroke stroke)
    {
        _drawingLayers[_layerIndex].RemoveStroke(stroke);
        OnChanged?.Invoke();
    }
}