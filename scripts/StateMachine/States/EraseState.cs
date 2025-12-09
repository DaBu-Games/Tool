using Godot;

public class EraseState : DrawState
{
    protected override Color CurrentColor => ProjectSettings.Instance.BackgroundColor;

    protected override void ShowUi()
    {
        _cm.UiManager.ShowEraseUI();
    }
}