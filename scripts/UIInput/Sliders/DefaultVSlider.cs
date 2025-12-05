using Godot;

public abstract partial class DefaultVSlider : VSlider
{
    [Export] private Label _valueLabel;
    protected ProjectSettings Settings => ProjectSettings.Instance;
    
    protected abstract double GetValue();
    protected abstract void SetValue(double value);

    public override void _Ready()
    {
        Value = GetValue();
        _ValueChanged(Value);
    }

    public override void _ValueChanged(double newValue)
    {
        _valueLabel.Text = newValue.ToString();
        SetValue(newValue);
    }
}