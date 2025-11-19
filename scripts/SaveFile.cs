using Godot;
using System;

public partial class SaveFile : FileDialog
{

    private void ShowWindow()
    {
        this.Show();
    }

    private void Save(string path)
    {
        if (path.EndsWith(".svg"))
        {
            
        }
    }
}
