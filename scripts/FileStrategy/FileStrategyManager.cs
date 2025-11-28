using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class FileStrategyManager : Node
{
    // diffrent file types the user can save
    private Dictionary<string, IFileSaveStrategy> _saveStrategies = new ();
    
    // diffrent file types and tool version the user can load in 
    private Dictionary<string, List<IFileLoadStrategy>> _loadStrategies = new ();

    public override void _Ready()
    {
        //save
            RegisterSaveStrategy(new TxtSaveStrategy());
            
        // version 1   
            //load
            RegisterLoadStrategy(new Version1.TxtLoadStrategy());
    }

    public void RegisterSaveStrategy(IFileSaveStrategy strategy)
    {
        _saveStrategies[strategy.FileType] = strategy;
    }

    public void RegisterLoadStrategy(IFileLoadStrategy strategy)
    {
        if (!_loadStrategies.ContainsKey(strategy.FileType))
            _loadStrategies[strategy.FileType] = new List<IFileLoadStrategy>();

        _loadStrategies[strategy.FileType].Add(strategy);
    }

    public void SaveFile(string filePath, DrawingData data)
    {
        string fileType = System.IO.Path.GetExtension(filePath).ToLower();

        if (!_saveStrategies.TryGetValue(fileType, out IFileSaveStrategy strategy))
        {
            GD.PrintErr("No save strategy for: " + fileType);
            return;
        }
        
        strategy.SaveFile(filePath, data);
    }

    public DrawingData LoadFile(string filePath)
    {
        string fileType = System.IO.Path.GetExtension(filePath).ToLower();
        
        if (!_loadStrategies.TryGetValue(fileType, out List<IFileLoadStrategy> strategies))
        {
            GD.PrintErr("No load strategies for: " + fileType);
            return null;
        }
        
        int version = GetVersion(filePath);
        
        IFileLoadStrategy strategy = strategies.FirstOrDefault(s => s.Version == version);

        if (strategy == null)
        {
            GD.PrintErr("No load strategy for: " + fileType + " version: " + version);
            return null;
        }
        
        return strategy.LoadFile(filePath);
    }

    private int GetVersion(string filePath)
    {
        // get the version of the current file 
        return 1;
    }
}