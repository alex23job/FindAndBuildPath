using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFigControl : MonoBehaviour
{
    [SerializeField] private GameObject prefabTailBody;
    [SerializeField] private float speed;

    private SpawnFigControl _spawnFigControl = null;
    private ShemaFigure _shema = null;
    private int _numShema = -1;
    private int _candyID = -1;
    private int _figureID = -1;
    private List<GameObject> tails = new List<GameObject>();
    private Vector3 _target = Vector3.zero;
    private bool _isMove = false;
    private List<GameObject> candys = new List<GameObject>();

    public int FigureID {  get { return _figureID; } }
    public int NumberShema { get { return _numShema; } }

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
    }

    public int[] GetShema()
    {
        return _shema.GetShema();
    }

    public void SetShema(int id, int[] arr, Vector3 pos, SpawnFigControl scc, int numShema)
    {
        _numShema = numShema;
        _spawnFigControl = scc;
        _figureID = id;
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
                posTail.y = 0.75f - 0.5f * (i / 4);
                GameObject tail = Instantiate(prefabTailBody);
                tail.transform.parent = transform;
                tail.transform.localPosition = posTail;
                TailFigure tailFigure = tail.GetComponentInChildren<TailFigure>();
                if (tailFigure != null)
                {
                    tailFigure.SetCollectFigControl(i, figControl);
                }
                tails.Add(tail);
            }
        }
        transform.rotation = Quaternion.Euler(new Vector3(30f, 0, 0));
        _isMove = true;
    }

    public bool CheckCandy(Vector3 pos)
    {
        bool res = false;
        if (_spawnFigControl != null)
        {
            res = _spawnFigControl.CheckCandy(_candyID, pos, _figureID);
        }
        return res;
    }

    public void SetCandyID(int id)
    {
        _candyID = id;
    }

    public void AddCandy(GameObject candy)
    {
        candys.Add(candy);
    }

    public bool CheckFull()
    {
        return tails.Count == candys.Count;
        /*foreach (GameObject tail in tails)
        {
            TailFigure tailFigure = tail.GetComponentInChildren<TailFigure>();
            if (tailFigure != null && tailFigure.IsCandy == false) return false;
        }
        return true;*/
    }

    public void RemoveChild()
    {
        int i;
        for (i = tails.Count - 1; i >= 0; i--)
        {
            tails[i].GetComponentInChildren<TailFigure>().SetRemove();
            Destroy(tails[i], 0.5f);
        }
        tails.Clear();
        for (i = candys.Count - 1; i >= 0; i--)
        {
            candys[i].GetComponent<CandyControl>().SetRemove();
            Destroy(candys[i], 0.5f);
        }
        candys.Clear();
    }
}
