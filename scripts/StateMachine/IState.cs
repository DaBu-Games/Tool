
public interface IState
{
    void OnEnter(StateMachine stateMachine);
    void OnExit();
}