using Godot;

public class DrawingData
{
    private string text;

    public string GetData() => text;

    public void SetData(string data)
    {
        text = data;
        GD.Print(text);
    }
}