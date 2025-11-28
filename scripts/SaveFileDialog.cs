using Godot;
using System;

public partial class SaveFileDialog : FileDialog
{
    [Export] private CanvasManager _canvasManager;
    [Export] private FileStrategyManager _fileStrategyManager;

    private void ShowWindow()
    {
        Show();
    }

    private void Save(string filePath)
    {
        DrawingData data = _canvasManager.GetDrawingData();
        _fileStrategyManager.SaveFile(filePath, data);
    }
}
