using Godot;

public partial class SelectionBox : Control
{
    public override void _Draw()
    {
        Rect2 rect = new Rect2(Vector2.Zero, Size);
        DrawRect(rect, new Color(ProjectSettings.Instance.SelectedColor, 0.5f), false, width: ProjectSettings.Instance.SelectedWidth * 2, antialiased: false);
    }
}