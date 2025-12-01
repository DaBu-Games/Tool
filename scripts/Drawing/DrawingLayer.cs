using System;
using System.Collections.Generic;
using Godot;

public class DrawingLayer
{
    private Image _image;
    private ImageTexture _texture;
    private Color _backgroundColor = Colors.White;

    public DrawingLayer(Vector2 canvasSize)
    {
        int width = (int)canvasSize.X;
        int height = (int)canvasSize.Y;
        
        _image = Image.CreateEmpty(width, height, false, Image.Format.Rgba8);
        _image.Fill(_backgroundColor);
        
        _texture = ImageTexture.CreateFromImage(_image);
    }
    
    public ImageTexture GetTexture() => _texture;
    
    public void DrawAtPoint(Vector2 pos, int width, Color color)
    {
        for (int i = -width; i <= width; i++)
        {
            for (int j = -width; j <= width; j++)
            {
                int px = (int)Mathf.Clamp(pos.X + i, 0, _image.GetWidth() - 1);
                int py = (int)Mathf.Clamp(pos.Y + j, 0, _image.GetHeight() - 1);
                
                _image.SetPixel(px, py, color);
            }
        }
        
        _texture.Update(_image);
    }

    public void ErasePoint()
    {
        
    }
    
}