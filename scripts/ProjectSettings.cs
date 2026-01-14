using Godot;


public partial class ProjectSettings : Node
{
    public static ProjectSettings Instance { get; private set; }
    
    // generate
    [Export] public int SelectedPointCount { get; private set; }
    public void SetPointCount(int value) => SelectedPointCount = value;
    
    [Export] public int SelectedSpikiness { get; private set; }
    public void SetSpikiness(int value) => SelectedSpikiness = value;
    
    [Export] public int SelectedIrregularity  { get; private set; }
    public void SetIrregularity(int value) => SelectedIrregularity = value;
    
    // draw
    [Export]public int SelectedWidth { get; private set; }
    public void SetWidth(int width) => SelectedWidth = width;
    
    [Export]public Color SelectedColor { get; private set; }
    public void SetColor(Color color) => SelectedColor = color;
    
    [Export]public Color BackgroundColor { get; private set; }
    
    [Export] public Color CanvasColor { get; private set; }
    // zoom 
    [Export]public float MinZoom { get; private set; }
    [Export]public float MaxZoom { get; private set; }
    [Export]public float ZoomSpeed { get; private set; }

    public override void _EnterTree()
    {
        Instance = this;
    }
}