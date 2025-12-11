using Godot;

namespace Version1
{
    public class PngLoadStrategy : IFileLoadStrategy
    {
        public string FileType => ".png";
        public int Version => 1;
        public DrawingData LoadFile(string filePath)
        {
            if (FileAccess.FileExists(filePath))
            {
                Image image = Image.LoadFromFile(filePath);
            
                return new DrawingData(image); 
            }
            else
            {
                GD.Print("file doesn't exist");
                return null;
            }
        }
    }
}