using System.Collections.Generic;

public class ShemaLevel
{
    private static List<ShemaLevel> levels = new List<ShemaLevel>();
    public static int CountLevels {  get { return levels.Count; } }
    public static ShemaLevel GetShemaLevel(int level)
    {
        if (levels.Count == 0)
        {

        }
        if (level >= 0 && level < levels.Count)
        {  
            return levels[level]; 
        }
        return null;
    }

    private int[] _doorGrid = null;
    private int[] _groundTails = null;
    private int numberLevel = 0;

    public int NumberLevel { get { return numberLevel; } }

    public ShemaLevel() { }

    public ShemaLevel(int num, int[] doors, int[] tails)
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
