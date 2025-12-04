using Godot;

public class GeneratorState(PackedScene selectionBox) : IState
{
    private StateMachine _sm;
    private CanvasManager _cm;
    private DrawingData _drawingData;
    private InputManager _im;
    private PackedScene _selectionBox = selectionBox;

    public void OnEnter(StateMachine stateMachine)
    {
        _sm = stateMachine;
        _cm = _sm.CanvasManager;
        _im = _cm.InputManager;
        _drawingData = _cm.DrawingData;

        _im.OnMouseDownCanvas += StartSelection;
        _im.OnMouseMoveCanvas += ContinueSelection;
        _im.OnMouseUpCanvas += EndSelection;
    }

    public void OnExit()
    {
        _im.OnMouseDownCanvas -= StartSelection;
        _im.OnMouseMoveCanvas -= ContinueSelection;
        _im.OnMouseUpCanvas -= EndSelection;
    }

    private void StartSelection(Vector2 pos)
    {
        
    }

    private void ContinueSelection(Vector2 pos)
    {
        
    }

    private void EndSelection(Vector2 pos)
    {
        
    }
}