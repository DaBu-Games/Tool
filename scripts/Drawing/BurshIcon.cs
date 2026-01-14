using Godot;

public partial class BurshIcon : Node2D
{
    private Color _currentColor;
    private bool _canShow = true;

    public override void _Ready()
    {
        _currentColor = ProjectSettings.Instance.SelectedColor;
        ShowBursh(false);
    }
    public override void _Process(double delta)
    {
        QueueRedraw();
    }

    public void IsErasing(bool isErasing)
    {
        _currentColor = isErasing ? ProjectSettings.Instance.CanvasColor : ProjectSettings.Instance.SelectedColor;
        CanShowBursh(true);
        ShowBursh(true);
    }

    public void SetColor(Color color)
    {
        _currentColor = color;
    }

    public override void _Draw()
    {
        Vector2 mousePos = GetGlobalMousePosition();

        DrawCircle(mousePos, ProjectSettings.Instance.SelectedWidth, _currentColor);
    }

    public void ShowBursh(bool show)
    {
        if (!_canShow && show)
            return;

        Visible = show;
    }
    
    public void CanShowBursh(bool canShow) => _canShow = canShow;
}
