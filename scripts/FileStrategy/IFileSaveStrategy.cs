using System;

public interface IFileSaveStrategy
{
    string FileType {get;}
    void SaveFile(string filePath, DrawingData data);
}
