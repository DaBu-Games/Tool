using Godot;


public partial class ProjectSettings : Node
{
    public static ProjectSettings Instance { get; private set; }
    
    // draw
    [Export]public int SelectedWidth { get; private set; }
    public void SetWidth(int width) => SelectedWidth = width;
    
    [Export]public Color SelectedColor { get; private set; }
    public void SetColor(Color color) => SelectedColor = color;
    
    [Export]public Color BackgroundColor { get; private set; }
    
    
    // zoom 
    [Export]public float MinZoom { get; private set; }
    [Export]public float MaxZoom { get; private set; }
    [Export]public float ZoomSpeed { get; private set; }

    public override void _EnterTree()
    {
        Instance = this;
    }
}