using System.Collections.Generic;

public class ShemaFigure
{
    public static List<ShemaFigure> Orders = new List<ShemaFigure>();
    public static int MaxShemaCounts = 15;
    public static ShemaFigure GetShemaOrder(int id)
    {
        if (Orders.Count == 0)
        {
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));    //  0
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }));    //  1
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0 }));    //  2
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0 }));    //  3
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0 }));    //  4
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0 }));    //  5
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0 }));    //  6
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0 }));    //  7
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0 }));    //  8
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0 }));    //  9
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0 }));    //  10
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 }));    //  11
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 0, 1, 0, 0, 0, 0 }));    //  12
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 0, 0 }));    //  13
            Orders.Add(new ShemaFigure(new int[16] { 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 }));    //  14
        }
        if ((id >= 0) && (id < Orders.Count))
        {
            return Orders[id];
        }
        return null;
    }
    private int[] shema = new int[16];
    private int angle = 0;

    public int Angle { get => angle; }

    public ShemaFigure() { }
    public ShemaFigure(int[] shemaArr)
    {
        for (int i = 0; i < shemaArr.Length; i++)
        {
            shema[i] = shemaArr[i];
        }
    }

    public int[] Rotate90()
    {
        angle += 90;
        angle %= 360;
        int[] tmp = new int[16];
        int i, x, y;
        for (i = 0; i < 16; i++)
        {
            x = i % 4; y = i / 4;
            tmp[i] = shema[4 * (3 - x) + y];
        }
        for (i = 0; i < 16; i++) shema[i] = tmp[i];
        return tmp;
    }

    public int[] GetShema()
    {
        int[] tmp = new int[16];
        for (int i = 0; i < 16; i++) tmp[i] = shema[i];
        return tmp;
    }
}

