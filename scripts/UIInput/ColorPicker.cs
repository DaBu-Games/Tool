using Godot;

public partial class ColorPicker : ColorPickerButton
{
    [Export] private Label _valueLabel;
    [Export] BurshIcon _burshIcon;

    public override void _Ready()
    {
        Color = ProjectSettings.Instance.SelectedColor;
        
        ChangeValue(Color);
        ColorChanged += ChangeValue;
        ColorChanged += _burshIcon.SetColor;
    }

    private void ChangeValue(Color color)
    {
        _valueLabel.Text = "#" + color.ToHtml();
        ProjectSettings.Instance.SetColor(color);
    }
    
}