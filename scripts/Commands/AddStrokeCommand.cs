
using System;

public class AddStrokeCommand : ICommand
{
    private DrawingData _drawingData;
    private Stroke _stroke;
    private Action _redrawAction;
    public AddStrokeCommand(DrawingData drawingData, Stroke stroke)
    {
        _drawingData = drawingData;
        _stroke = stroke;
    }
    public void Execute() => _drawingData.AddStroke(_stroke);

    public void Undo() => _drawingData.RemoveStroke(_stroke);
}