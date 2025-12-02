using System;
using Godot;

public partial class InputManager : Node
{
    public event Action<Vector2> OnMouseDownCanvas;
    public event Action<Vector2> OnMouseUpCanvas;
    public event Action<Vector2> OnMouseMoveCanvas;
    
    public event Action OnMouseScrollUp;
    public event Action OnMouseScrollDown;
    public event Action<Vector2> OnClickLeftMouseDown;
    public event Action<Vector2> OnClickLeftMouseUp;
    public event Action<Vector2> OnMouseMove;
    public event Action OnUndo;
    public event Action OnRedo;

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey e && e.Pressed)
        {
            if (e.CtrlPressed && e.Keycode == Key.Z)
            {
                if (e.ShiftPressed)
                {
                    OnRedo?.Invoke();
                }
                else
                {
                    OnUndo?.Invoke();
                }
            }
        }
        
        if (@event is InputEventMouseButton mb && mb.ButtonIndex == MouseButton.Right)
        {
            if (mb.Pressed)
            {
                OnClickLeftMouseDown?.Invoke(mb.Position);
            }
            else
            {
                OnClickLeftMouseUp?.Invoke(mb.Position);
            }
        }

        if (@event is InputEventMouseButton mw)
        {
            if (mw.ButtonIndex == MouseButton.WheelUp)
            {
                OnMouseScrollUp?.Invoke();
            }
            else if (mw.ButtonIndex == MouseButton.WheelDown)
            {
                OnMouseScrollDown?.Invoke();
            }
        }

        if (@event is InputEventMouseMotion mm)
        {
            OnMouseMove?.Invoke(mm.Position);
        }
    }

    public void NotifyCanvasMouseDown(Vector2 pos) => OnMouseDownCanvas?.Invoke(pos);
    public void NotifyCanvasMouseUp(Vector2 pos) => OnMouseUpCanvas?.Invoke(pos);
    public void NotifyCanvasMouseMove(Vector2 pos) => OnMouseMoveCanvas?.Invoke(pos);
}