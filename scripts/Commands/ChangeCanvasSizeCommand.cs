using System.Collections.Generic;
using Godot;

public class ChangeCanvasSizeCommand(CanvasManager canvasManager, Vector2I oldSize, Vector2I newSize) : ICommand
{
    private CanvasManager _canvasManager = canvasManager;
    private Vector2I _oldSize = oldSize;
    private Vector2I _newSize = newSize;
    private List<PixelChange> _deletedPixels;
    
    public void Execute()
    {
        _canvasManager.ChangeSize(_newSize);
        _deletedPixels = _canvasManager.DrawingData.GetCurrentDrawingLayer().ResizeCanvas(_newSize);
    }

    public void Undo()
    {
        _canvasManager.ChangeSize(_oldSize);
        _canvasManager.DrawingData.GetCurrentDrawingLayer().ResizeCanvas(_oldSize);
        
        GD.Print(_deletedPixels.Count);

        if (_deletedPixels.Count > 0)
        {
            foreach (PixelChange pixel in _deletedPixels)
            {
                _canvasManager.DrawingData.GetCurrentDrawingLayer().DrawAtPoint((Vector2I)pixel.Pos, pixel.OldColor);
            }
        }
    }
}