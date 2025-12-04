using Godot;

public partial class SetGenerateButton: Button
{
    [Export]private StateMachine _stateMachine;
    [Export]private PackedScene _selectionBox;

    private void SetGenerateState()
    {
        _stateMachine.SetState(new GeneratorState(_selectionBox));
    }
}