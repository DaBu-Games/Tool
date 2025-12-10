using System;
using System.Collections.Generic;
using Godot;

public class DrawingLayer
{
    private Image _image;
    private ImageTexture _texture;
    private Vector2 _imageSize;

    public DrawingLayer(Vector2 canvasSize)
    {
        _imageSize  = canvasSize;
        int width = (int)canvasSize.X;
        int height = (int)canvasSize.Y;

        _image = Image.CreateEmpty(width, height, false, Image.Format.Rgba8);
        _image.Fill(ProjectSettings.Instance.BackgroundColor);

        _texture = ImageTexture.CreateFromImage(_image);
    }

    public DrawingLayer(Image image)
    {
        if (image.GetFormat() != Image.Format.Rgba8)
            image.Convert(Image.Format.Rgba8);
        
        image.Decompress();
        
        _image = image;
        _imageSize = new Vector2(image.GetWidth(), image.GetHeight());
        _texture = ImageTexture.CreateFromImage(_image);
    }

    public Vector2 ImageSize => _imageSize;
    public Image Image => _image; 
    
    public ImageTexture GetTexture()
    {
        _texture.Update(_image);
        return _texture;
    }

    public List<PixelChange> DrawWithRadius(Vector2 pos, int width, Color color)
    {
        List<PixelChange> changes = new List<PixelChange>();
        
        float rSquared = width * width;
        
        for (int i = -width; i <= width; i++)
        {
            for (int j = -width; j <= width; j++)
            {
                if (i * i + j * j > rSquared)
                    continue;
                
                int px = (int)Mathf.Clamp(pos.X + i, 0, _image.GetWidth() - 1);
                int py = (int)Mathf.Clamp(pos.Y + j, 0, _image.GetHeight() - 1);

                PixelChange pixel = DrawAtPoint(new Vector2I(px, py), color);

                if (pixel.OldColor != pixel.NewColor)
                {
                    changes.Add(pixel);
                }
            }
        }
        
        return changes;
    }

    public PixelChange DrawAtPoint(Vector2I pos, Color newColor)
    {
        Color oldColor = _image.GetPixel(pos.X, pos.Y);
        
        _image.SetPixel(pos.X, pos.Y, newColor);
        
        return new PixelChange(pos, oldColor, newColor);
    }

    public void ChangePixels(List<PixelChange> changes, bool newColor)
    {
        foreach (var pixel in changes)
        {
            _image.SetPixel((int)pixel.Pos.X, (int)pixel.Pos.Y, newColor ? pixel.NewColor : pixel.OldColor);
        }
    }
}