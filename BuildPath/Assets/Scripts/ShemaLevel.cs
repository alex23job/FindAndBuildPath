using System.Collections.Generic;

public class ShemaLevel
{
    private static List<ShemaLevel> _levels = new List<ShemaLevel>();
    public static int CountLevels {  get { return _levels.Count; } }
    public static ShemaLevel GetShemaLevel(int level)
    {
        if (_levels.Count == 0)
        {
            _levels.Add(new ShemaLevel(0, new int[13] { 0x0015500, 0x0006400, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[26] { 0, 0, 0, 0x3c00000, 0x3e00000, 0x3f80000, 0x1fc0000, 0x07f0000, 0x03f8000, 0x01fc000, 0x00fe000, 0x007ff00, 0x003ff80, 
                                0x001ffc0, 0x000ffe0, 0x000fff0, 0x0007fff, 0x0003fff, 0x000fff, 0x0003ff, 0, 0, 0, 0, 0, 0 }));
        }
        if (level >= 0 && level < _levels.Count)
        {  
            return _levels[level]; 
        }
        return null;
    }

    private int[] _doorGrid = null;
    private int[] _groundTails = null;
    private int[] _forests = null;
    private int numberLevel = 0;

    public int NumberLevel { get { return numberLevel; } }

    public ShemaLevel() { }

    public ShemaLevel(int num, int[] tails, int[] doors)
    {
        numberLevel = num;
        _doorGrid = doors;
        _groundTails = tails;
    }

    public int[] GetDoorGrids()
    {
        return _doorGrid;
    }

    public int[] GetGroundTails()
    {
        return _groundTails; 
    }
}
