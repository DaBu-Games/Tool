using System;
using System.Collections.Generic;
using Godot;

public class DrawingData
{
    private List<DrawingLayer> _drawingLayers = new List<DrawingLayer>();
    private int _layerIndex = 0;
    public event Action OnChanged;
    
    public DrawingLayer GetCurrentDrawingLayer() => _drawingLayers[_layerIndex];
    
    public void InvokeChange() => OnChanged?.Invoke();

    public DrawingData(Vector2 canvasSize)
    {
        _drawingLayers.Add(new DrawingLayer(canvasSize));
    }
    
    public DrawingData(Image image)
    {
        _drawingLayers.Add(new DrawingLayer(image));
    }
}