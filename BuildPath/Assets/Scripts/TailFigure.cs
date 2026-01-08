using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailFigure : MonoBehaviour
{
    private CollectFigControl _figControl = null;
    private int _tailID = -1;
    private bool _isCandy = false;
    private bool _isRemove = false;
    private Vector3 _startSize;
    private int _prc = 100;

    public int TailID { get { return _tailID; } }
    public bool IsCandy {  get { return _isCandy; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRemove)
        {
            if (_prc > 0)
            {
                _prc -= 1;
                transform.localScale = _startSize * _prc / 100f;
            }
            else
            {
                _isRemove = false;
            }
        }
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

    public void SetRemove()
    {
        _startSize = transform.localScale;
        _isRemove = true;
    }
}
