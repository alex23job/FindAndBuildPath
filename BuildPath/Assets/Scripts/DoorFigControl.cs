using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFigControl : MonoBehaviour
{
    [SerializeField] private GameObject prefabTailBody;
    [SerializeField] private float speed = 5f;

    private LevelControl _levelControl;
    private ShemaFigure _shema = null;
    private int _figureID = -1;
    private List<GameObject> tails = new List<GameObject>();
    private Vector3 _target = Vector3.zero;
    private bool _isMove = false;
    private bool _isNew = true;
    private bool _isMoving = false;
    private bool _isTurn = true;

    private Vector3 _startPos;
    private Vector3 _deltaPos;

    private bool isPacking = false;

    public int FigureID { get { return _figureID; } }
    public bool IsNew { get { return _isNew; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMove)
        {
            Vector3 delta = _target - transform.position;
            if (delta.magnitude > 0.2f)
            {
                delta.Normalize();
                transform.position += delta * speed * Time.deltaTime;
            }
            else
            {
                transform.position = _target;
                _isMove = false;
            }
        }
        if (_isMoving)
        {
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 figPos = transform.position;
            //figPos.x += mp.x - deltaPos.x; figPos.z += 1.35f * (mp.z - deltaPos.z);
            figPos.x += mp.x - _deltaPos.x; figPos.z += 4 * (mp.z - _deltaPos.z);
            transform.position = figPos;
            _deltaPos = mp;
        }

    }
    public void SetShema(int id, int[] arr, Vector3 pos, LevelControl lc)
    {
        _levelControl = lc;
        _figureID = id;
        if ((id == 0) || (id == 3)) _isTurn = false;
        _shema = new ShemaFigure(arr);
        _target = pos;
        Vector3 posTail = Vector3.zero;
        CollectFigControl figControl = gameObject.GetComponent<CollectFigControl>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != 0)
            {
                //posTail.x = -1.5f + i % 4;
                //posTail.y = 1.5f - i / 4;
                posTail.x = -0.75f + 0.5f * (i % 4);
                posTail.z = 0.75f - 0.5f * (i / 4);
                GameObject tail = Instantiate(prefabTailBody);
                tail.transform.parent = transform;
                tail.transform.localPosition = posTail;
                tail.transform.localRotation = Quaternion.Euler(new Vector3(0, 90f, 0));
                TailFigure tailFigure = tail.GetComponentInChildren<TailFigure>();
                if (tailFigure != null)
                {
                    tailFigure.SetCollectFigControl(i, figControl);
                }
                tails.Add(tail);
            }
        }
        //transform.rotation = Quaternion.Euler(new Vector3(30f, 0, 0));
        gameObject.SetActive(false);
    }

    public void ViewAndMove()
    {
        gameObject.SetActive(true);
        _isMove = true;
        _isNew = false;
    }

    private void OnMouseDown()
    {
        //if (isPacking) return;
        if (Input.GetMouseButtonDown(0))
        {
            _startPos = transform.position;
            _isMoving = true;
            Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _deltaPos = mp;
        }
    }
    private void OnMouseUp()
    {
        //if (isPacking) return;
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 delta = _startPos - transform.position;
            if (delta.magnitude < 0.1f && _isTurn)
            {
                _shema.Rotate90();
                transform.Rotate(0, 90, 0, Space.World);
                transform.position = _startPos;
                //return;
                /*Vector3 rot = transform.rotation.eulerAngles;
                rot.z += 90f;rot.z = Mathf.RoundToInt(rot.z) % 360;
                transform.rotation = Quaternion.Euler(rot);*/
            }
            //print($"OnMouseUp   isMovement={isMovement}");
            if (_isMoving)
            {
                if (_levelControl != null && _levelControl.TestPacking(gameObject))
                {
                    isPacking = true; 
                    _isMove = false;
                }
                else
                {
                    print($"OnMouseUp   isPacking={isPacking}");
                    if (isPacking == false) transform.position = _startPos;
                }
                _isMoving = false;
                //_levelControl = null;
            }
        }
    }

}

