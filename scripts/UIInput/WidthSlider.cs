using Godot;

public partial class WidthSlider : VSlider
{
    [Export] private Label _valueLabel;

    public override void _Ready()
    {
        Value = ProjectSettings.Instance.SelectedWidth;
        _ValueChanged(Value);
    }

    public override void _ValueChanged(double newValue)
    {
        _valueLabel.Text = newValue.ToString();
        ProjectSettings.Instance.SetWidth((int)newValue);
    }
}