using Godot;

public partial class IrregularitySlider : DefaultVSlider
{
    protected override double GetValue() => Settings.SelectedIrregularity;

    protected override void SetValue(double value) => Settings.SetIrregularity((int)value);
}