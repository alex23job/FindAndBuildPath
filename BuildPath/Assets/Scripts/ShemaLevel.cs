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
                new int[26] { 0x3800000, 0x3800000, 0x3800000, 0x3c00000, 0x3e00000, 0x3f80000, 0x0fc0000, 0x07f0000, 0x03f8000, 0x00fc000, 0x007e000, 0x001f800, 0x000fe00, 
                                0x007f80, 0x0003fc0, 0x0001fe0, 0x0000ff0, 0x00007f8, 0x00001fc, 0x000007e, 0x3e, 0x1e, 0xe, 0, 0, 0 }));
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
