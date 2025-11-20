using Godot;

namespace Version1
{
    public class TxtSaveStrategy : IFileSaveStrategy
    {
        public string FileType => ".txt";
        public void SaveFile(string filePath, DrawingData data)
        {
            FileAccess file = FileAccess.Open(filePath, FileAccess.ModeFlags.Write);
            file.StoreString(data.GetData());
            file.Close();
        }
    }
}