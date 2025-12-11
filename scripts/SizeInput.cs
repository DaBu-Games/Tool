using Godot;

public partial class SizeInput : Control
{
    [Export] private CanvasManager _canvasManager;
    [Export] private LineEdit _sizeInputX;
    [Export] private LineEdit _sizeInputY;

    public override void _Ready()
    {
        _canvasManager.ItemRectChanged += ChangeInputValue;

        _sizeInputX.TextSubmitted += ChangeSizeX;
        _sizeInputY.TextSubmitted += ChangeSizeY;
        
        ChangeInputValue();
    }

    private void ChangeInputValue()
    {
        _sizeInputX.Text = _canvasManager.Size.X.ToString();
        _sizeInputY.Text = _canvasManager.Size.Y.ToString();
    }

    private void ChangeSizeX(string value)
    {
        ChangeCanvas(new Vector2I((int)float.Parse(value), (int)_canvasManager.Size.Y));
    }
    
    private void ChangeSizeY(string value)
    {
        ChangeCanvas(new Vector2I(((int)_canvasManager.Size.X),(int)float.Parse(value)));
    }

    private void ChangeCanvas(Vector2I size)
    {
        ICommand icommand = new ChangeCanvasSizeCommand(_canvasManager, (Vector2I)_canvasManager.Size, size);
        _canvasManager.CommandHistory.AddCommand(icommand);
        icommand.Execute();
        
        _canvasManager.DrawingData.InvokeChange();
    }
}