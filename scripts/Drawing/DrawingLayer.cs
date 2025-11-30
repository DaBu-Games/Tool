using System;
using System.Collections.Generic;
using Godot;

public class DrawingLayer
{
    private List<Stroke> _strokes = new List<Stroke>();

    private int _cellSize = 50;
    private int _cols, _rows;
    private List<(Stroke stroke, int pointIndex)>[,] _grid;

    public DrawingLayer(Vector2 canvasSize)
    {
        _cols = (int)Math.Ceiling(canvasSize.X / (float)_cellSize);
        _rows = (int)Math.Ceiling(canvasSize.Y / (float)_cellSize);
        
        _grid = new List<(Stroke stroke, int pointIndex)>[_cols, _rows];

        for (int x = 0; x < _cols; x++)
        {
            for (int y = 0; y < _rows; y++)
            {
                _grid[x, y] = new List<(Stroke stroke, int pointIndex)>();
            }
        }
    }
    
    public List<Stroke> GetStrokes() => _strokes;

    public void AddStroke(Stroke stroke)
    {
        stroke.SetBounds(_cellSize);
        _strokes.Add(stroke);
        
        for (int i = 0; i < stroke.GetPoints.Count; i++)
        {
            AddPointToGrid(stroke, i, stroke.GetPoint(i));
        }
    }

    public void RemoveStroke(Stroke stroke)
    {
        _strokes.Remove(stroke);
        RemoveStrokeFromGrid(stroke);
    }

    private void AddPointToGrid(Stroke stroke, int pointIndex, Vector2 point)
    {
        int gx = (int)(point.X / _cellSize);
        int gy = (int)(point.Y / _cellSize);

        if (gx < 0 || gy < 0 || gx >= _cols || gy >= _rows) 
            return;
        
        _grid[gx, gy].Add((stroke, pointIndex));
    }
    
    private void RemoveStrokeFromGrid(Stroke stroke)
    {
        for (int gx = stroke.MinCellX; gx <= stroke.MaxCellX; gx++)
        {
            for (int gy = stroke.MinCellY; gy <= stroke.MaxCellY; gy++)
            {
                if (gx < 0 || gy < 0 || gx >= _cols || gy >= _rows) 
                    continue;

                _grid[gx, gy].RemoveAll(p => p.stroke == stroke);
            }
        }
    }
}