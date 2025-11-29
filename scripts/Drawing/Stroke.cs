using System.Collections.Generic;
using Godot; 

public class Stroke
{
    private List<Vector2> _points = new List<Vector2>();
    private float _width;
    private Color _color;

    public Stroke(float width, Color color)
    {
        _width = width;
        _color = color;
    }

    public Stroke(List<Vector2> points, float width, Color color)
    {
        _points = points;
        _width = width;
        _color = color;
    }
    
    public List<Vector2> GetPoints => _points;
    public float GetWidth => _width;
    public Color GetColor => _color;

    public void AddPoint(Vector2 point)
    {
        _points.Add(point);
    }
}