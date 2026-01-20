using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class ShemaLevel
{
    private static List<ShemaLevel> _levels = new List<ShemaLevel>();
    public static int CountLevels {  get { return _levels.Count; } }
    public static ShemaLevel GetShemaLevel(int level)
    {
        if (_levels.Count == 0)
        {
            _levels.Add(new ShemaLevel(0, new Vector2(2, -1), new Vector2(12, 10), new int[13] { 0x0015500, 0x0006400, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
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
    private int[] _doorFulls = null;
    private int _numberLevel = 0;
    private Vector2 _startPoint = Vector2.zero;
    private Vector2 _finishPoint = Vector2.zero;

    public int NumberLevel { get { return _numberLevel; } }

    public string IDS_LEVEL {  get { return _ids_level; } }

    public Vector2 StartPoint { get { return _startPoint; } }
    public Vector2 FinishPoint { get { return _finishPoint; } }

    public ShemaLevel()
    {
        _numberLevel = -1;
        _ids_level = "Level";
        _groundTails = new int[13];
        _doorGrid = new int[26];
        _doorFulls = new int[26];
    }

    public ShemaLevel(string csv, char sep = '=')
    {
        string[] ar = csv.Split(sep);
        if (ar.Length >= 5)
        {
            _ids_level = ar[0];
            if (int.TryParse(ar[1], out int num)) { _numberLevel = num; }
            int i, zn, x, y;
            string s;
            s = ar[2].Substring(0, 2);
            x = Convert.ToInt32(s, 16);
            s = ar[2].Substring(2, 2);
            y = Convert.ToInt32(s, 16);
            _startPoint = new Vector2(x, y);
            s = ar[3].Substring(0, 2);
            x = Convert.ToInt32(s, 16);
            s = ar[3].Substring(2, 2);
            y = Convert.ToInt32(s, 16);
            _finishPoint = new Vector2(x, y);
            //Debug.Log($"ar[2]=>{ar[2]}   start=>{_startPoint}        ar[3]=>{ar[3]}   finish=>{_finishPoint}");
            List<int> tmp = new List<int>();
            for (i = 0; i < 13; i++)
            {
                s = ar[4].Substring(8 * i, 8);
                zn = Convert.ToInt32(s, 16);
                tmp.Add(zn);
            }
            _groundTails = tmp.ToArray();
            tmp.Clear();
            for (i = 0; i < 26; i++)
            {
                s = ar[5].Substring(8 * i, 8);
                zn = Convert.ToInt32(s, 16);
                tmp.Add(zn);
            }
            _doorGrid = tmp.ToArray();
            tmp.Clear();
            for (i = 0; i < 26; i++)
            {
                s = ar[6].Substring(8 * i, 8);
                zn = Convert.ToInt32(s, 16);
                tmp.Add(zn);
            }
            _doorFulls = tmp.ToArray();
        }
    }

    public ShemaLevel(int num, Vector2 start, Vector2 finish, int[] tails, int[] doors, int[] fulls)
    {
        _ids_level = "Level";
        _numberLevel = num;
        _doorGrid = doors;
        _groundTails = tails;
        _doorFulls = fulls;
        _startPoint = start;
        _finishPoint = finish;
    }

    public int[] GetDoorGrids()
    {
        return _doorGrid;
    }

    public int[] GetGroundTails()
    {
        return _groundTails;
    }
    public int[] GetDoorFulls()
    {
        return _doorFulls;
    }

    public void ChangeTails(int x, int y, int tailType, bool isSet = true)
    {
        switch(tailType)
        {
            case 1:
                _groundTails[y / 2] ^= (1 << ((x / 2) * 2)); 
                break;
            case 2:
                _groundTails[y / 2] ^= (2 << ((x / 2) * 2));
                break;
            case 3:
                _groundTails[y / 2] ^= (3 << ((x / 2) * 2));
                break;
            case 4:
                _doorFulls[y] ^= (1 << x);
                break;
            case 5:
                _doorGrid[y] ^= (1 << x);
                break;
            case 6:                
                if (isSet == false) _startPoint = Vector2.zero;
                else _startPoint = new Vector2(x, y);
                    break;
            case 7:                
                if (isSet == false) _finishPoint = Vector2.zero;
                else _finishPoint = new Vector2(x, y);
                    break;
            default:
                Debug.Log($"Неизвестный тип части {tailType}");
                break;
        }
    }

    public void SetNumber(int num)
    {
        _numberLevel = num;
    }

    public string ToCsvString(char sep = '=')
    {
        StringBuilder sb = new StringBuilder($"Level{sep}{_numberLevel}{sep}");
        if (_numberLevel != -1)
        {
            int i, x, y;
            x = (int)(_startPoint.x);
            y = (int)(_startPoint.y);
            sb.Append($"{x:X02}{y:X02}{sep}");
            x = (int)(_finishPoint.x);
            y = (int)(_finishPoint.y);
            sb.Append($"{x:X02}{y:X02}{sep}");
            //sb.Append($"{_startPoint.x:X02}{_startPoint.y:X02}{sep}");
            //sb.Append($"{_finishPoint.x:X02}{_finishPoint.y:X02}{sep}");
            for (i = 0; i < 13; i++)
            {
                sb.Append($"{_groundTails[i]:X08}");
            }
            sb.Append(sep);
            for (i = 0; i < 26; i++)
            {
                sb.Append($"{_doorGrid[i]:X08}");
            }
            sb.Append(sep);
            for (i = 0; i < 26; i++)
            {
                sb.Append($"{_doorFulls[i]:X08}");
            }
            sb.Append(sep);
        }
        return sb.ToString();
    }
}
