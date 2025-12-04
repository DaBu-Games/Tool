using Godot;
using System;
using System.Collections.Generic;

public partial class CanvasManager : TextureRect
{
    [Export] public InputManager InputManager { get; private set; }
    public CommandHistory CommandHistory { get; private set; } = new CommandHistory();
    public DrawingData DrawingData { get; private set; } = new DrawingData();
    
    public override void _Ready()
    {
        MouseFilter = MouseFilterEnum.Pass;
        DrawingData.OnChanged += UpdateTexture;
        DrawingData.AddDrawingLayer(this.Size);
        
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
        Texture = DrawingData.GetCurrentDrawingLayer().GetTexture();
    }

    public void SetDrawingData(DrawingData data)
    {
        DrawingData = data;
        UpdateTexture();
    }
}
