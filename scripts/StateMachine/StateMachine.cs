using Godot;

public partial class StateMachine : Node
{
    [Export] public CanvasManager CanvasManager { get; private set; }
    
    private IState _currentState;

    public override void _Ready()
    {
        SetState(new DrawState());
    }

    public void SetState(IState state)
    {
        _currentState?.OnExit();
        _currentState = state;
        _currentState?.OnEnter(this);
    }
}