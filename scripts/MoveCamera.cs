using Godot;

public partial class MoveCamera : Camera2D
{
    [Export] private InputManager _inputManager;
    private Vector2 _lastPos;
    private bool _canMove = false;

    public override void _Ready()
    {
        _inputManager.OnClickLeftMouseDown += SetStartPosition;
        _inputManager.OnClickLeftMouseUp += ResetMove;
        _inputManager.OnMouseMove += Move;
    }

    public void SetStartPosition(Vector2 position)
    {
        _lastPos = position;
        _canMove = true;
    }

    public void ResetMove(Vector2 position)
    {
        _canMove = false;
    }

    public void Move(Vector2 position)
    {
        if(!_canMove)
            return;

        Vector2 diff = _lastPos - position;
        Position += diff;

        _lastPos = position;
    }
}