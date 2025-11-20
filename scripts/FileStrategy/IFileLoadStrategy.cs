using System;

public interface IFileLoadStrategy
{
    string FileType {get;}
    int Version {get;}
    DrawingData LoadFile(string filePath);
}
