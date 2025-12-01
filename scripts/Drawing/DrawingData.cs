using System;
using System.Collections.Generic;
using Godot;

public class DrawingData
{
    private List<DrawingLayer> _drawingLayers = new List<DrawingLayer>();
    private int _layerIndex = 0;
    private int _currentDrawWidth = 5; 
    private int _currentEraseRadius = 2;
    private Color _currentColor = Colors.Aqua;
    public event Action OnChanged;
    
    public DrawingLayer GetCurrentDrawingLayer() => _drawingLayers[_layerIndex];

    public void SetDrawingData(List<DrawingLayer> drawingLayers)
    {
        _drawingLayers = drawingLayers;
    }

    public void AddDrawingLayer(Vector2 canvasSize)
    {
        _drawingLayers.Add(new DrawingLayer(canvasSize));
        OnChanged?.Invoke();
    }

    public void DrawAtPoint(Vector2 pos)
    {
        GetCurrentDrawingLayer().DrawAtPoint(pos, _currentDrawWidth, _currentColor);
        OnChanged?.Invoke();
    }

    public void RemoveStroke()
    {
        
        OnChanged?.Invoke();
    }
}