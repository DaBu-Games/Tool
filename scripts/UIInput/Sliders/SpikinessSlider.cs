using Godot;

public partial class SpikinessSlider : DefaultVSlider
{
    protected override double GetValue() => Settings.SelectedSpikiness;

    protected override void SetValue(double value) => Settings.SetSpikiness((int)value);
}
