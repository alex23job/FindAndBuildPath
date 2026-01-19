using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditTail : MonoBehaviour
{
    private int _tailSize = 0;

    public int TailSize { get { return _tailSize; } }

    public void SetSize(int sz)
    {
        _tailSize = sz;
    }
}
