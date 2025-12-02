using System.Collections.Generic;
using Godot;

public class DrawState : IState
{
    private StateMachine _sm;
    private CanvasManager _cm;
    private DrawingData _drawingData;
    private InputManager _im;
    
    private bool _isDrawing;
    private Vector2 _lastPos;
    private float _stepDistance;
    
    private List<PixelChange> _changes;
    private HashSet<Vector2> _visitedPixels;

    protected virtual Color CurrentColor => ProjectSettings.Instance.BurshColor;
    protected virtual int CurrenWidth => ProjectSettings.Instance.BrushWidth;
    
    public void OnEnter(StateMachine stateMachine)
    {
        _sm = stateMachine;
        _cm = _sm.CanvasManager;
        _im = _cm.InputManager;
        _drawingData = _cm.DrawingData;
        _stepDistance = ProjectSettings.Instance.BrushWidth * 0.5f;

        _im.OnMouseDownCanvas += StartStroke;
        _im.OnMouseMoveCanvas += ContinueStroke;
        _im.OnMouseUpCanvas += EndStroke;
    }

    public void OnExit()
    {
        _im.OnMouseDownCanvas -= StartStroke;
        _im.OnMouseMoveCanvas -= ContinueStroke;
        _im.OnMouseUpCanvas -= EndStroke;
    }

    private void StartStroke(Vector2 pos)
    {
        _isDrawing = true;
        _changes = new List<PixelChange>();
        _visitedPixels = new HashSet<Vector2>();
        
        AddPixel(pos);
        _lastPos = pos;
    }

    private void ContinueStroke(Vector2 pos)
    {
        if(!_isDrawing)
            return;
        
        float distance = _lastPos.DistanceTo(pos);
        int steps = Mathf.Max(1, (int)(distance / _stepDistance));

        for (int i = 0; i <= steps; i++)
        {
            float t = i / (float)steps;
            Vector2 p = _lastPos.Lerp(pos, t);
            AddPixel(p);
        }
        
        _lastPos = pos;
        _drawingData.InvokeChange();
    }

    private void EndStroke(Vector2 pos)
    {
        AddPixel(pos);
        _drawingData.InvokeChange();
        
        ICommand command = new AddPixelsCommand(_drawingData, _changes);
        _cm.CommandHistory.Execute(command);
        
        _changes.Clear();
        _visitedPixels.Clear();
        
        _isDrawing = false;
    }
    
    private void AddPixel(Vector2 pos)
    {
        var changes = _drawingData.GetCurrentDrawingLayer()
            .DrawAtPoint(pos, CurrenWidth, CurrentColor);

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