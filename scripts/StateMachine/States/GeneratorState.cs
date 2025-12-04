using Godot;

public class GeneratorState : IState
{
    private StateMachine _sm;
    private CanvasManager _cm;
    private DrawingData _drawingData;
    private InputManager _im;
    private SelectionBox _selectionBox;

    private bool _isSelecting = false;
    private const float MinSelectionSize = 2.5f;

    public void OnEnter(StateMachine stateMachine)
    {
        _sm = stateMachine;
        _cm = _sm.CanvasManager;
        _im = _cm.InputManager;
        _drawingData = _cm.DrawingData;
        
        _selectionBox = new SelectionBox();
        _selectionBox.MouseFilter = Control.MouseFilterEnum.Ignore; 
        _selectionBox.Hide();
        _cm.AddChild(_selectionBox);

        _im.OnMouseDownCanvas += StartSelection;
        _im.OnMouseMoveCanvas += ContinueSelection;
        _im.OnMouseUpCanvas += EndSelection;
    }

    public void OnExit()
    {
        _im.OnMouseDownCanvas -= StartSelection;
        _im.OnMouseMoveCanvas -= ContinueSelection;
        _im.OnMouseUpCanvas -= EndSelection;
        
        _selectionBox.QueueFree();
    }

    private void StartSelection(Vector2 pos)
    {
        _selectionBox.Position = pos;
        _selectionBox.Size = Vector2.Zero;
        _selectionBox.Show();
        _isSelecting = true;
    }

    private void ContinueSelection(Vector2 pos)
    {
        if (_selectionBox == null || !_isSelecting) return;

        Vector2 start = _selectionBox.Position;
        _selectionBox.Size = pos - start;

        _selectionBox.QueueRedraw();
    }

    private void EndSelection(Vector2 pos)
    {
        if (_selectionBox == null || !_isSelecting) return;
        
        Vector2 start = _selectionBox.Position;
        _selectionBox.Size = pos - start;
        _isSelecting = false;
        
        if(_selectionBox.Size is { X: <= MinSelectionSize, Y: <= MinSelectionSize } )
            _selectionBox.Hide();
    }
}