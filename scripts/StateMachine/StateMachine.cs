using Godot;

public partial class StateMachine : Node
{
    public CanvasManager CanvasManager { get; private set; }
    
    public InputManager InputManager { get; private set; }
    
    private IState _currentState;

    public void SetState(IState state)
    {
        _currentState?.OnExit();
        _currentState = state;
        _currentState?.OnEnter();
    }
}