using System.Collections.Generic;
using Godot;

public class DrawState : IState
{
    private StateMachine _sm;
    protected CanvasManager _cm;
    private InputManager _im;
    
    private bool _isDrawing;
    private Vector2 _lastPos;
    private float _stepDistance;
    
    private List<PixelChange> _changes;
    private HashSet<Vector2> _visitedPixels;

    protected virtual Color CurrentColor => ProjectSettings.Instance.SelectedColor;
    private int CurrentWidth => ProjectSettings.Instance.SelectedWidth;
    
    public void OnEnter(StateMachine stateMachine)
    {
        _sm = stateMachine;
        _cm = _sm.CanvasManager;
        _im = _cm.InputManager;

        _im.OnMouseDownCanvas += StartStroke;
        _im.OnMouseMoveCanvas += ContinueStroke;
        _im.OnMouseUpCanvas += EndStroke;
        
        ShowUi();
    }

    public void OnExit()
    {
        _im.OnMouseDownCanvas -= StartStroke;
        _im.OnMouseMoveCanvas -= ContinueStroke;
        _im.OnMouseUpCanvas -= EndStroke;
    }

    protected virtual void ShowUi()
    {
        _cm.UiManager.ShowDrawUI();
    }

    private void StartStroke(Vector2 pos)
    {
        _isDrawing = true;
        _changes = new List<PixelChange>();
        _visitedPixels = new HashSet<Vector2>();
        _stepDistance = CurrentWidth * 0.5f;
        
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
        _cm.DrawingData.InvokeChange();
    }

    private void EndStroke(Vector2 pos)
    {
        AddPixel(pos);
        _cm.DrawingData.InvokeChange();

        if (_changes.Count > 0)
        {
            ICommand command = new AddPixelsCommand(_cm.DrawingData, _changes);
            _cm.CommandHistory.AddCommand(command);
        }
        
        _changes.Clear();
        _visitedPixels.Clear();
        
        _isDrawing = false;
    }
    
    private void AddPixel(Vector2 pos)
    {
        var changes = _cm.DrawingData.GetCurrentDrawingLayer()
            .DrawWithRadius(pos, CurrentWidth, CurrentColor);

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