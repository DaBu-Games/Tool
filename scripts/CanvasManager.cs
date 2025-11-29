using Godot;
using System;
using System.Collections.Generic;

public partial class CanvasManager : Control
{
    [Export] private float _currentWidth; 
    [Export] private Color _currentColor;
    private DrawingData _drawingData = new DrawingData();
    private int _layerIndex = 0;
    private Stroke _activeStroke;
    
    [Export] public InputManager InputManager { get; private set; }
    
    public float GetCurrentWidth() => _currentWidth;
    public Color GetCurrentColor() => _currentColor;
    
    public DrawingData GetDrawingData() => _drawingData;
    

    #region Input

    public override void _Ready()
    {
        MouseFilter = MouseFilterEnum.Stop;
        
        _drawingData.AddDrawingLayer();
    }
    
    public override void _GuiInput(InputEvent @event)
    {
        if (InputManager == null)
            return;
        
        if (@event is InputEventMouseButton mb && mb.ButtonIndex == MouseButton.Left)
        {
            if (mb.Pressed)
                InputManager.NotifyCanvasMouseDown(mb.Position);
            else
                InputManager.NotifyCanvasMouseUp(mb.Position);
        }

        if (@event is InputEventMouseMotion mm)
        {
            InputManager.NotifyCanvasMouseMove(mm.Position);
        }
    }

    #endregion
    
    #region Drawing

    public override void _Draw()
    {
        foreach (var stroke in _drawingData.GetStrokes(_layerIndex))
        {
            DrawStroke(stroke);
        }
        
        if (_activeStroke != null)
            DrawStroke(_activeStroke);
    }
    
    private void DrawStroke(Stroke stroke)
    {
        List<Vector2> points = stroke.GetPoints;
        for (int i = 1; i < points.Count; i++)
        {
            DrawLine(points[i - 1], points[i], stroke.GetColor, stroke.GetWidth);
        }
    }

    public void SetDrawingData(DrawingData data)
    {
        _drawingData = data;
        QueueRedraw();
    }

    public void SetLayerIndex(int layerIndex)
    {
        _layerIndex = layerIndex;
    }

    public void SetActiveStroke(Stroke stroke)
    {
        _activeStroke = stroke;
        QueueRedraw();
    }

    public void AddStroke(Stroke stroke)
    {
        _drawingData.AddStroke(stroke, _layerIndex);
    }
    #endregion
}
