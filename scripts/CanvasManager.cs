using Godot;
using System;
using System.Collections.Generic;

public partial class CanvasManager : TextureRect
{
    [Export] public InputManager InputManager { get; private set; }
    [Export] public UIManager UiManager { get; private set; }
    public CommandHistory CommandHistory { get; private set; } = new CommandHistory();
    private DrawingData _drawingData = null;
    
    public override void _Ready()
    {
        MouseFilter = MouseFilterEnum.Pass;
        _drawingData = new DrawingData(this.Size);
        _drawingData.OnChanged += UpdateTexture;
        _drawingData.InvokeChange();
        
        InputManager.OnUndo += CommandHistory.Undo;
        InputManager.OnUndo += UpdateTexture;
        InputManager.OnRedo += CommandHistory.Redo;
        InputManager.OnRedo += UpdateTexture;
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

    public void UpdateTexture()
    {
        Texture = _drawingData.GetCurrentDrawingLayer().GetTexture();
    }
    
    public DrawingData DrawingData => _drawingData;

    public void SetDrawingData(DrawingData data)
    {
        _drawingData = data;
        Size = _drawingData.GetCurrentDrawingLayer().ImageSize;
        Position = Size / 4;
        
        _drawingData.OnChanged += UpdateTexture;
        _drawingData.InvokeChange();
    }
}
