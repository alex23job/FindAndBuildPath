using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFigControl : MonoBehaviour
{
    [SerializeField] private GameObject prefabTailBody;
    [SerializeField] private float speed;

    private ShemaFigure _shema = null;
    private int _candyID = -1;
    private int _figureID = -1;
    private List<GameObject> tails = new List<GameObject>();
    private Vector3 _target = Vector3.zero;
    private bool _isMove = false;

    public int FigureID {  get { return _figureID; } }

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

    public void SetShema(int id, int[] arr, Vector3 pos)
    {
        _figureID = id;
        _shema = new ShemaFigure(arr);
        _target = pos;
        Vector3 posTail = Vector3.zero;
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
            }
        }
        transform.rotation = Quaternion.Euler(new Vector3(30f, 0, 0));
        _isMove = true;
    }
}
