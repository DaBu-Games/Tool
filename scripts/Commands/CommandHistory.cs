
using System.Collections.Generic;
using Godot;

public class CommandHistory
{
    private Stack<ICommand> _undoStack = new Stack<ICommand>();
    private Stack<ICommand> _redoStack = new Stack<ICommand>();

    public void Execute(ICommand command)
    {
        _undoStack.Push(command);
        _redoStack.Clear();
    }

    public void Undo()
    {
        if(_undoStack.Count == 0) 
            return;
        
        ICommand command = _undoStack.Pop();
        command.Undo();
        _redoStack.Push(command);
    }

    public void Redo()
    {
        if(_redoStack.Count == 0)
            return;
        
        ICommand command = _redoStack.Pop();
        command.Execute();
        _undoStack.Push(command);
    }
}