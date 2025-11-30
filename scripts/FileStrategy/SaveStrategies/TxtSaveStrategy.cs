using Godot;

public class TxtSaveStrategy : IFileSaveStrategy
{
    public string FileType => ".txt";
    public void SaveFile(string filePath, DrawingData data)
    {
        FileAccess file = FileAccess.Open(filePath, FileAccess.ModeFlags.Write);
        file.StoreString(data.ToString());
        file.Close(); 
    }
}
