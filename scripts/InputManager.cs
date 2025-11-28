using System;
using Godot;

public partial class InputManager : Node
{
    public event Action<Vector2> OnMouseDown;
    public event Action<Vector2> OnMouseUp;
    public event Action<Vector2> OnMouseMove;

    public event Action OnUndo;
    public event Action OnRedo;

    public override void _Input(InputEvent e)
    {
        if (e is InputEventMouseButton mb)
        {
            if (mb.ButtonIndex == MouseButton.Left)
            {
                if (mb.Pressed)
                    OnMouseDown?.Invoke(mb.Position);
                else
                    OnMouseUp?.Invoke(mb.Position);
            }
        }

        if (e is InputEventMouseMotion mm)
            OnMouseMove?.Invoke(mm.Position);

        // Undo / Redo
        //if (Input.IsKeyPressed(Key.Control) && Input.IsKeyPressed(Key.Z))
            //OnUndo?.Invoke();

        //if (Input.IsKeyPressed(Key.Control) && Input.IsKeyPressed(Key.Y))
            //OnRedo?.Invoke();
    }
}