using Godot;
using System;

public partial class LoadFileDialog : FileDialog
{
    [Export] private ToolManager _toolManager;
    [Export] private FileStrategyManager _fileStrategyManager;

    private void ShowWindow()
    {
        Show();
    }

    private void Load(string path)
    {
        DrawingData data = _fileStrategyManager.LoadFile(path);
        
        if(data != null)
            _toolManager.SetDrawingData(data);
    }
}
