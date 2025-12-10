using Godot;

public class ChangeDrawingDataCommand : ICommand
{
    private CanvasManager _cm;
    private DrawingData _oldData;
    private DrawingData _newData;

    public ChangeDrawingDataCommand(CanvasManager cm, DrawingData newData)
    {
        _cm = cm;
        _oldData = _cm.DrawingData;
        _newData = newData;
    }
    public void Execute()
    {
        _cm.SetDrawingData(_newData);
    }

    public void Undo()
    {
       _cm.SetDrawingData(_oldData);
    }
}