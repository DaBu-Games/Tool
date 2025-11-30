
using Godot;

public class DrawState : IState
{
    private StateMachine _sm;
    private CanvasManager _cm;
    private InputManager _im;
    private Stroke _currentStroke;
    
    public void OnEnter(StateMachine stateMachine)
    {
        _sm = stateMachine;
        _cm = _sm.CanvasManager;
        _im = _cm.InputManager;

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
        _currentStroke = new Stroke(_cm.GetCurrentWidth(), _cm.GetCurrentColor());
        _currentStroke.AddPoint(pos);
        _cm.SetActiveStroke(_currentStroke);
    }

    private void ContinueStroke(Vector2 pos)
    {
        if(_currentStroke == null)
            return;
        
        _currentStroke.AddPoint(pos);
        _cm.SetActiveStroke(_currentStroke);
    }

    private void EndStroke(Vector2 pos)
    {
        if (_currentStroke == null)
            return;

        _currentStroke.AddPoint(pos);

        _cm.AddStroke(_currentStroke);
        _cm.SetActiveStroke(null);

        _currentStroke = null;
    }
}