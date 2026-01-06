using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailFigure : MonoBehaviour
{
    private CollectFigControl _figControl = null;
    private int _tailID = -1;
    private bool _isCandy = false;

    public int TailID { get { return _tailID; } }
    public bool IsCandy {  get { return _isCandy; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCollectFigControl(int id, CollectFigControl collect)
    {
        _figControl = collect;
        _tailID = id;
    }

    private void OnMouseUp()
    {
        if (_isCandy) return;
        if (_figControl != null)
        {
            _isCandy = _figControl.CheckCandy(transform.position);
        }
    }
}
