using System.Collections.Generic;
using Godot;

public class GeneratorState : IState
{
    private StateMachine _sm;
    private CanvasManager _cm;
    private InputManager _im;
    private SelectionBox _selectionBox;

    private bool _isSelecting = false;
    private const float MinSelectionSize = 2.5f;
    
    private Vector2 _selectionBoxStart;
    private Vector2 _canvasSize;

    private Color _selectedColor;
    private List<PixelChange> _changes;
    private HashSet<Vector2> _visitedPixels;

    public void OnEnter(StateMachine stateMachine)
    {
        _sm = stateMachine;
        _cm = _sm.CanvasManager;
        _im = _cm.InputManager;
        
        _selectionBox = new SelectionBox();
        _selectionBox.MouseFilter = Control.MouseFilterEnum.Ignore; 
        _selectionBox.Hide();
        _cm.AddChild(_selectionBox);

        _im.OnMouseDownCanvas += StartSelection;
        _im.OnMouseMoveCanvas += ContinueSelection;
        _im.OnMouseUpCanvas += EndSelection;
        
        _cm.UiManager.ShowGenerateUI();
    }

    public void OnExit()
    {
        _im.OnMouseDownCanvas -= StartSelection;
        _im.OnMouseMoveCanvas -= ContinueSelection;
        _im.OnMouseUpCanvas -= EndSelection;
        
        _selectionBox.QueueFree();
    }

    private void StartSelection(Vector2 rawPos)
    {
        _canvasSize = _cm.DrawingData.GetCurrentDrawingLayer().ImageSize;
        
        Vector2 pos = new Vector2(
            Mathf.Clamp(rawPos.X, 0, _canvasSize.X),
            Mathf.Clamp(rawPos.Y, 0, _canvasSize.Y)
        );
        
        _selectionBoxStart = pos;
        _selectionBox.Position = pos;
        _selectionBox.Size = Vector2.Zero;
        _selectionBox.Show();
        _isSelecting = true;
        
        _changes = new List<PixelChange>();
        _visitedPixels = new HashSet<Vector2>();
        _selectedColor = ProjectSettings.Instance.SelectedColor;
    }

    private void ContinueSelection(Vector2 rawPos)
    {
        if (_selectionBox == null || !_isSelecting) return;
        
        ClampSelection(rawPos);
    }

    private void EndSelection(Vector2 rawPos)
    {
        if (_selectionBox == null || !_isSelecting) return;

        ClampSelection(rawPos);
        
        _isSelecting = false;
        _selectionBox.Hide();

        if (_selectionBox.Size is { X: <= MinSelectionSize, Y: <= MinSelectionSize })
            return;
        
        GenerateShape(_selectionBox.Position, _selectionBox.Size);
        
        _cm.DrawingData.InvokeChange();
        
        ICommand command = new AddPixelsCommand(_cm.DrawingData, _changes);
        _cm.CommandHistory.AddCommand(command);
        
        _changes.Clear();
        _visitedPixels.Clear();
    }

    private void ClampSelection(Vector2 rawPos)
    {
        Vector2 pos = new Vector2(
            Mathf.Clamp(rawPos.X, 0, _canvasSize.X),
            Mathf.Clamp(rawPos.Y, 0, _canvasSize.Y)
        );

        Vector2 start = _selectionBoxStart;
        
        float x = Mathf.Min(start.X, pos.X);
        float y = Mathf.Min(start.Y, pos.Y);
        float w = Mathf.Abs(pos.X - start.X);
        float h = Mathf.Abs(pos.Y - start.Y);

        _selectionBox.Position = new Vector2(x, y);
        _selectionBox.Size = new Vector2(w, h);

        _selectionBox.QueueRedraw();
    }
    
    private void GenerateShape(Vector2 start, Vector2 size)
    {
        var layer = _cm.DrawingData.GetCurrentDrawingLayer();
        Rect2 rect = new Rect2(start, size).Abs();
        
        List<Vector2> poly = GenerateRandomPolygon(rect);

        for (int i = 0; i < poly.Count; i++)
        {
            Vector2 a = poly[i];
            Vector2 b = poly[(i + 1) % poly.Count];

            AddLinePixels(layer, a, b);
        }
    }
    
    private List<Vector2> GenerateRandomPolygon(Rect2 rect)
    {
        int pointCount = ProjectSettings.Instance.SelectedPointCount;
        float irregularity = ProjectSettings.Instance.SelectedIrregularity / 10f;
        float spikiness = ProjectSettings.Instance.SelectedSpikiness / 10f;
        
        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();

        List<Vector2> points = new();

        Vector2 center = rect.Position + rect.Size / 2f;
        float avgRadius = Mathf.Min(rect.Size.X, rect.Size.Y) * 0.4f;

        float angleStep = (Mathf.Pi * 2) / pointCount;

        float angle = 0;

        for (int i = 0; i < pointCount; i++)
        {
            float randomAngle = angle + rng.RandfRange(-angleStep * irregularity, angleStep * irregularity);
            float radius = avgRadius + rng.RandfRange(-avgRadius * spikiness, avgRadius * spikiness);

            Vector2 point = center + new Vector2(
                Mathf.Cos(randomAngle) * radius,
                Mathf.Sin(randomAngle) * radius
            );
            
            AddPixel(point);

            points.Add(point);
            angle += angleStep;
        }

        return points;
    }
    
    private void AddLinePixels(DrawingLayer layer, Vector2 p1, Vector2 p2)
    {
        int x0 = (int)p1.X;
        int y0 = (int)p1.Y;
        int x1 = (int)p2.X;
        int y1 = (int)p2.Y;

        int dx = Mathf.Abs(x1 - x0);
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {

            AddPixel(new Vector2(x0, y0));

            if (x0 == x1 && y0 == y1)
                break;

            int e2 = 2 * err;

            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }

            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }
    }
    
    private void AddPixel(Vector2 pos)
    {
        var changes = _cm.DrawingData.GetCurrentDrawingLayer()
            .DrawWithRadius(pos, ProjectSettings.Instance.SelectedWidth, _selectedColor);

        foreach (var c in changes)
        {
            Vector2 key = new Vector2(c.Pos.X, c.Pos.Y);

            if (_visitedPixels.Add(key))
            {
                _changes.Add(c);
            }
        }
    }
}