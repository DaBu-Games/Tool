using System.Collections.Generic;
using Godot;

public class DrawingLayer
{
    private List<Stroke> _strokes = new List<Stroke>();
    
    public List<Stroke> GetStrokes() => _strokes;

    public void AddStroke(Stroke stroke)
    {
        _strokes.Add(stroke);
    }
}