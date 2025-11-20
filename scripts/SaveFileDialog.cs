using Godot;
using System;

public partial class SaveFileDialog : FileDialog
{
    [Export] private ToolManager _toolManager;
    [Export] private FileStrategyManager _fileStrategyManager;

    private void ShowWindow()
    {
        this.Show();
    }

    private void Save(string filePath)
    {
        DrawingData data = _toolManager.GetDrawingData();
        _fileStrategyManager.SaveFile(filePath, data);
    }
}
