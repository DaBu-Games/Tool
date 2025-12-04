using Godot;


public partial class ProjectSettings : Node
{
    public static ProjectSettings Instance { get; private set; }
    
    // draw
    [Export]public int BrushWidth { get; private set; }
    [Export]public int EraseWidth { get; private set; }
    [Export]public Color BurshColor { get; private set; }
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