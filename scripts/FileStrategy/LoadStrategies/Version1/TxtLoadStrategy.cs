using Godot;

namespace Version1
{
    public class TxtLoadStrategy : IFileLoadStrategy
    {
        public string FileType => ".txt";
        public int Version => 1;
    
        public DrawingData LoadFile(string filePath)
        {
            if (FileAccess.FileExists(filePath))
            {
                FileAccess file = FileAccess.Open(filePath, FileAccess.ModeFlags.Read);
                DrawingData drawingData = new DrawingData();
                drawingData.SetData(file.GetAsText());
                file.Close();
            
                return drawingData; 
            }
            else
            {
                GD.Print("file doesn't exist");
                return null;
            }
        }
    }
}