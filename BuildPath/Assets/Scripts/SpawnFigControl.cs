using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFigControl : MonoBehaviour
{
    [SerializeField] private LevelControl _levelControl;
    [SerializeField] private GameObject _prefabCollectFigure;
    [SerializeField] private float _shiftX = 4f;
    [SerializeField] private float _shiftY = 4f;
    [SerializeField] private Vector3 _basketPoint;

    private List<Vector3> _spawnPoints = new List<Vector3>();
    private List<GameObject> _figures = new List<GameObject>();

    private List<GameObject> _completeFigures = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSpawnPoints(int count, Vector3 baseSpawnPoint)
    {
        int div = (count + 1) / 2;
        for (int i = 0; i < count; i++)
        {
            _spawnPoints.Add(new Vector3(baseSpawnPoint.x + _shiftX * (i % div), baseSpawnPoint.y + _shiftY * (i / div), baseSpawnPoint.z));
            _figures.Add(null);
        }
    }

    public void CreateFigure(int num)
    {
        GameObject fig = Instantiate(_prefabCollectFigure, transform.position, Quaternion.identity);
        int numShema = Random.Range(0, ShemaFigure.MaxShemaCounts);
        fig.GetComponent<CollectFigControl>().SetShema(num, ShemaFigure.GetShemaOrder(numShema).GetShema(), _spawnPoints[num], gameObject.GetComponent<SpawnFigControl>(), numShema);
        _figures[num] = fig;
    }

    public bool CheckCandy(int candyID, Vector3 pos, int figureNum)
    {
        bool res = false;
        GameObject curCandy = null;
        if (_levelControl != null)
        {
            curCandy = _levelControl.GetCurrentCandy();
            CandyControl candyControl = curCandy.GetComponent<CandyControl>();
            if (candyControl != null)
            {
                CollectFigControl cfc = _figures[figureNum].GetComponent<CollectFigControl>();
                Vector3 target = pos;
                target.z -= 0.2f; target.y += 0.15f;
                if (candyID == -1)
                {   //  первая конфета для фигуры
                    cfc.SetCandyID(candyControl.CandyID);
                    candyControl.SetTarget(target, true);
                    cfc.AddCandy(curCandy);
                    res = true;
                }
                else if (candyControl.CmpCandyID(candyID))
                {   //  в фигуре тип конфет совпал с текущей конфетой - добавляем
                    candyControl.SetTarget(target, true);
                    cfc.AddCandy(curCandy);
                    res = true;
                }
                else
                {   //  тип конфет не совпал - удаляем текущую конфету ???
                    candyControl.RemoveCandy(_basketPoint);
                }
                if (cfc.CheckFull())
                {   //  фигура заполнена
                    //print($"CheckFull => true   figureNum={figureNum}  candyID={candyID}");
                    _levelControl.GenerateDoorFigure(cfc.GetShema(), _figures[figureNum].transform.position, cfc.NumberShema);
                    
                    _completeFigures.Add(_figures[figureNum]);
                    _figures[figureNum] = null;
                    
                    CreateFigure(figureNum);
                    Invoke("RemoveCompleteFigure", 1.5f);
                }
            }
            _levelControl.SpawnNextCandy();
        }

        return res;
    }

    private void RemoveCompleteFigure()
    {
        CollectFigControl cfc = _completeFigures[0].GetComponent<CollectFigControl>();
        cfc.RemoveChild();
        GameObject fig = _completeFigures[0];
        _completeFigures.RemoveAt(0);
        Destroy(fig, 0.15f);
    }
}
