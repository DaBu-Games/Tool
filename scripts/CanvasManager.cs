using Godot;
using System;
using System.Collections.Generic;

public partial class CanvasManager : Control
{
    [Export] private float _currentWidth; 
    [Export] private Color _currentColor;
    private Stroke _activeStroke;
    
    [Export] public InputManager InputManager { get; private set; }
    public CommandHistory CommandHistory { get; private set; } = new CommandHistory();
    public DrawingData DrawingData { get; private set; } = new DrawingData();
    
    public float GetCurrentWidth() => _currentWidth;
    public Color GetCurrentColor() => _currentColor;
    
    public override void _Ready()
    {
        MouseFilter = MouseFilterEnum.Stop;
        DrawingData.AddDrawingLayer();
        DrawingData.OnChanged += QueueRedraw;
        
        InputManager.OnUndo += CommandHistory.Undo;
        InputManager.OnRedo += CommandHistory.Redo;
    }

    #region Input
    
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
        foreach (var stroke in DrawingData.GetStrokes())
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
        DrawingData = data;
        QueueRedraw();
    }

    public void SetActiveStroke(Stroke stroke)
    {
        _activeStroke = stroke;
    }
    
    #endregion
}
