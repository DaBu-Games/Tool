using Godot;

public partial class PointCountSlider : DefaultVSlider
{
    protected override double GetValue() => Settings.SelectedPointCount;

    protected override void SetValue(double value) => Settings.SetPointCount((int)value);
}