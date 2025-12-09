using Godot;

public partial class UIManager : Node
{
    [Export] private Control _colorPicker;
    [Export] private Control _generateUi;
    [Export] private Control _spikeSlider;

    public void ShowDrawUI()
    {
        _generateUi.Hide();
        _spikeSlider.Hide();
        _colorPicker.Show();
    }

    public void ShowEraseUI()
    {
        _generateUi.Hide();
        _spikeSlider.Hide();
        _colorPicker.Hide();
    }

    public void ShowGenerateUI()
    {
        _generateUi.Show();
        _spikeSlider.Show();
        _colorPicker.Show();
    }
}