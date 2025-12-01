
using System;

public class AddPixelsCommand : ICommand
{
    private DrawingData _drawingData;
    
    public AddPixelsCommand(DrawingData drawingData)
    {
        _drawingData = drawingData;
    }

    public void Execute()
    {
        
    }

    public void Undo()
    { 
       
    }
}