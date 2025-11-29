using System;
using Godot;

public partial class InputManager : Node
{
    public event Action<Vector2> OnMouseDownCanvas;
    public event Action<Vector2> OnMouseUpCanvas;
    public event Action<Vector2> OnMouseMoveCanvas;

    public event Action OnUndo;
    public event Action OnRedo;

    public override void _Input(InputEvent e)
    {
        if (Input.IsKeyPressed(Key.Ctrl) && Input.IsKeyPressed(Key.Z))
        {
            if (Input.IsKeyPressed(Key.Shift))
            {
                OnRedo?.Invoke();
            }
            else
            {
                OnUndo?.Invoke();
            }
        }
    }
    
    public void NotifyCanvasMouseDown(Vector2 pos) => OnMouseDownCanvas?.Invoke(pos);
    public void NotifyCanvasMouseUp(Vector2 pos) => OnMouseUpCanvas?.Invoke(pos);
    public void NotifyCanvasMouseMove(Vector2 pos) => OnMouseMoveCanvas?.Invoke(pos);
}