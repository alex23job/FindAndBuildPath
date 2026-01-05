using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCandyControl : MonoBehaviour
{
    [SerializeField] private GameObject[] candyPrefabs;

    private List<GameObject> _candys = new List<GameObject>();
    private GameObject _currentCandy = null;
    private int _maxCountCandys = 5;

    public GameObject CurrentCandy { get { return _currentCandy; } }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 5; i++)
        {
            Invoke("CreateCandy", i);
        }
        Invoke("GenerateNextCandy", 5f);
        //_currentCandy = _candys[0];
        //_candys.RemoveAt(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateCandy()
    {
        int num = Random.Range(0, _maxCountCandys);
        GameObject candy = Instantiate(candyPrefabs[num]);
        Vector3 target = transform.position;
        target.x += 3f;target.y += 0.3f;target.z -= 0.5f;
        candy.transform.position = target;
        for(int i = 0; i < _candys.Count; i++)
        {
            target = _candys[i].transform.position;
            target.x += 1f;
            print($"i={i}  pos=<{_candys[i].transform.position}>  tg=<{target}>");
            CandyControl candyControl = _candys[i].GetComponent<CandyControl>();
            if (candyControl != null)
            {
                candyControl.SetTarget(target);
            }
        }
        _candys.Add(candy);
    }

    public void GenerateNextCandy()
    {
        CreateCandy();
        _currentCandy = _candys[0];
        _candys.RemoveAt(0);
    }
}
