using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditTail : MonoBehaviour
{
    private int _tailSize = 0;
    private int _tailType = 0;
    private Vector2 position = Vector2.zero;

    public int TailSize { get { return _tailSize; } }
    public int TailType { get { return _tailType; } }
    public Vector2 Position { get { return position; } }

    public void SetSize(int sz)
    {
        _tailSize = sz;
    }

    public void SetType(int tp)
    {
        _tailType = tp;
    }

    public void SetPosition(Vector2 pos)
    {
        position = pos;
    }
}
