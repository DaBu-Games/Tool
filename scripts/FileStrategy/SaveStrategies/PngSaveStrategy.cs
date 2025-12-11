using Godot;

public class PngSaveStrategy : IFileSaveStrategy
{
    public string FileType => ".png";
    public void SaveFile(string filePath, DrawingData data)
    {
        data.GetCurrentDrawingLayer().Image.SavePng(filePath);
    }
}