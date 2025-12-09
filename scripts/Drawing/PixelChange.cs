using Godot;

public class PixelChange
{
    private Vector2 _pos;
    private Color _oldColor;
    private Color _newColor;

    public PixelChange(Vector2 pos, Color oldColor, Color newColor)
    {
        _pos = pos;
        _oldColor = oldColor;
        _newColor = newColor;
    }
    
    public Vector2 Pos => _pos;
    public Color OldColor => _oldColor;
    public Color NewColor => _newColor;
}