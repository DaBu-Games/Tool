using Godot;

public class EraseState : DrawState
{
    protected override Color CurrentColor => ProjectSettings.Instance.BackgroundColor;
    protected override int CurrenWidth => ProjectSettings.Instance.EraseWidth;
}