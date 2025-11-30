using System.Collections.Generic;
using Godot; 

public class Stroke
{
    private List<Vector2> _points = new List<Vector2>();
    private float _width;
    private Color _color;

    public int MinCellX { get; private set; }
    public int MaxCellX { get; private set; }
    public int MinCellY { get; private set; }
    public int MaxCellY { get; private set; }

    public Stroke(float width, Color color)
    {
        _width = width;
        _color = color;
        
        MinCellX = int.MaxValue;
        MaxCellX = int.MinValue;
        MinCellY = int.MaxValue;
        MaxCellY = int.MinValue;
    }

    public Stroke(List<Vector2> points, float width, Color color)
    {
        _points = points;
        _width = width;
        _color = color;
        
        MinCellX = int.MaxValue;
        MaxCellX = int.MinValue;
        MinCellY = int.MaxValue;
        MaxCellY = int.MinValue;
    }
    
    public List<Vector2> GetPoints => _points;
    public Vector2 GetPoint(int pointIndex) => _points[pointIndex];
    public float GetWidth => _width;
    public Color GetColor => _color;

    public void AddPoint(Vector2 point)
    {
        _points.Add(point);
    }

    public void SetBounds(int cellSize)
    {
        foreach (var point in _points)
        {
            int gx = (int)(point.X / cellSize);
            int gy = (int)(point.Y / cellSize);
        
            if (gx < MinCellX) MinCellX = gx;
            if (gx > MaxCellX) MaxCellX = gx;
            if (gy < MinCellY) MinCellY = gy;
            if (gy > MaxCellY) MaxCellY = gy;
        }
    }
}