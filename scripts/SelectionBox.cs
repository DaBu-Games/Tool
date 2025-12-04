using Godot;

public partial class SelectionBox : Control
{
    public override void _Draw()
    {
        var rect = new Rect2(Vector2.Zero, Size);
        DrawRect(rect, ProjectSettings.Instance.SelectedColor, false, width: 2.0f, antialiased: false);
    }
}