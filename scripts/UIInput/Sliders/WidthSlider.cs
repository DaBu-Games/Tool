using Godot;

public partial class WidthSlider : DefaultVSlider
{
    protected override double GetValue() => Settings.SelectedWidth;

    protected override void SetValue(double value) => Settings.SetWidth((int)value);
}