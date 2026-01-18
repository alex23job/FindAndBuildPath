using System;
using System.Collections.Generic;
using System.Text;

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
                                0x007f80, 0x0003fc0, 0x0001fe0, 0x0000ff0, 0x00007f8, 0x00001fc, 0x000007e, 0x3e, 0x1e, 0xe, 0, 0, 0 },
                new int[26] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}));
        }
        if (level >= 0 && level < _levels.Count)
        {  
            return _levels[level]; 
        }
        return null;
    }

    private string _ids_level = "";

    private int[] _doorGrid = null;
    private int[] _groundTails = null;
    private int[] _forests = null;
    private int numberLevel = 0;

    public int NumberLevel { get { return numberLevel; } }

    public string IDS_LEVEL {  get { return _ids_level; } }

    public ShemaLevel()
    {
        numberLevel = -1;
        _ids_level = "Level";
        _groundTails = new int[13];
        _doorGrid = new int[26];
        _forests = new int[26];
    }

    public ShemaLevel(string csv, char sep = '=')
    {
        string[] ar = csv.Split(sep);
        if (ar.Length >= 5)
        {
            _ids_level = ar[0];
            if (int.TryParse(ar[1], out int num)) { numberLevel = num; }
            int i, zn;
            string s;
            List<int> tmp = new List<int>();
            for (i = 0; i < 13; i++)
            {
                s = ar[2].Substring(8 * i, 8);
                zn = Convert.ToInt32(s, 16);
                tmp.Add(zn);
            }
            _groundTails = tmp.ToArray();
            tmp.Clear();
            for (i = 0; i < 26; i++)
            {
                s = ar[3].Substring(8 * i, 8);
                zn = Convert.ToInt32(s, 16);
                tmp.Add(zn);
            }
            _doorGrid = tmp.ToArray();
            tmp.Clear();
            for (i = 0; i < 26; i++)
            {
                s = ar[4].Substring(8 * i, 8);
                zn = Convert.ToInt32(s, 16);
                tmp.Add(zn);
            }
            _forests = tmp.ToArray();
        }
    }

    public ShemaLevel(int num, int[] tails, int[] doors, int[] forest)
    {
        _ids_level = "Level";
        numberLevel = num;
        _doorGrid = doors;
        _groundTails = tails;
        _forests = forest;
    }

    public int[] GetDoorGrids()
    {
        return _doorGrid;
    }

    public int[] GetGroundTails()
    {
        return _groundTails; 
    }

    public string ToCsvString(char sep = '=')
    {
        StringBuilder sb = new StringBuilder($"Level{sep}{numberLevel}{sep}");
        if (numberLevel != -1)
        {
            int i;
            for (i = 0; i < 13; i++)
            {
                sb.Append($"{_doorGrid[i]:X08}");
            }
            sb.Append(sep);
            for (i = 0; i < 26; i++)
            {
                sb.Append($"{_groundTails[i]:X08}");
            }
            sb.Append(sep);
            for (i = 0; i < 26; i++)
            {
                sb.Append($"{_forests[i]:X08}");
            }
            sb.Append(sep);
        }
        return sb.ToString();
    }
}
