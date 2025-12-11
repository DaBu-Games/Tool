using Godot;

public class JpgSaveStrategy : IFileSaveStrategy
{
    public string FileType => ".jpg";
    public void SaveFile(string filePath, DrawingData data)
    {
        data.GetCurrentDrawingLayer().Image.SaveJpg(filePath);
    }
}