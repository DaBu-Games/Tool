using System;
using System.Collections.Generic;
using Godot;

public class AddPixelsCommand(DrawingData drawingData, List<PixelChange> changes) : ICommand
{
    private DrawingData _drawingData = drawingData;
    private List<PixelChange> _changes = new(changes);

    public void Execute() => _drawingData.GetCurrentDrawingLayer().ChangePixels(_changes, true);
    public void Undo() => _drawingData.GetCurrentDrawingLayer().ChangePixels(_changes, false);
}