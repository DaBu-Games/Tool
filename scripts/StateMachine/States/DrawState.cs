
using Godot;

public class DrawState : IState
{
    private StateMachine _sm;
    private CanvasManager _cm;
    private DrawingData _drawingData;
    private InputManager _im;
    
    private bool _isDrawing;
    
    public void OnEnter(StateMachine stateMachine)
    {
        _sm = stateMachine;
        _cm = _sm.CanvasManager;
        _im = _cm.InputManager;
        _drawingData = _cm.DrawingData;

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
        _drawingData.DrawAtPoint(pos);
    }

    private void ContinueStroke(Vector2 pos)
    {
        if(!_isDrawing)
            return;
        
        _drawingData.DrawAtPoint(pos);
    }

    private void EndStroke(Vector2 pos)
    {
        _drawingData.DrawAtPoint(pos);
        _isDrawing = false;
    }
}