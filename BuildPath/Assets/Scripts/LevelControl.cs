using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private UI_control _ui_control;
    [SerializeField] private KolobokMovement _kolobokMovement;
    [SerializeField] private SpawnCandyControl _spawnCandyControl;
    [SerializeField] private SpawnFigControl _spawnFigControl;
    [SerializeField] private LevelEnviroment _levelEnviroment;
    [SerializeField] private Vector3 _basePointCollectFigure;
    [SerializeField] private GameObject _doorFigPrefab;

    private int _maxSpawnPoints = 6;
    private int _levelSpawnPoints = 6;

    private List<GameObject> doorFigures = new List<GameObject>();

    private ShemaLevel _shemaLevel = null;
    private bool _isDoorFull = false;

    // Start is called before the first frame update
    void Start()
    {
        _shemaLevel = ShemaLevel.GetShemaLevel(0);  //  исправить на загрузку по данным из GM
        CreateLevelEnviroment();
        Invoke("CreateCollectFigures", 1f);
        //Invoke("MoveKolobok", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateLevelEnviroment()
    {
        _levelEnviroment.SetShemaLevel(_shemaLevel);
        _levelEnviroment.SetKolobok(_kolobokMovement.transform);
    }

    private void CreateCollectFigures()
    {
        _spawnFigControl.CreateSpawnPoints(6, _basePointCollectFigure);
        for (int i = 0; i < _levelSpawnPoints; i++)
        {
            _spawnFigControl.CreateFigure(i);
        }
    }

    public GameObject GetCurrentCandy()
    {
        return _spawnCandyControl.CurrentCandy;
    }

    public void SpawnNextCandy()
    {
        _spawnCandyControl.GenerateNextCandy();
    }

    public void GenerateDoorFigure(int[] arr, Vector3 pos, int numShema)
    {
        GameObject doorFig = Instantiate(_doorFigPrefab, pos, Quaternion.identity);
        DoorFigControl dfc = doorFig.GetComponent<DoorFigControl>();
        if (dfc != null)
        {
            Vector3 target = pos;
            target.x += 0.1f; target.z = 0;target.y = 0.7f;
            dfc.SetShema(numShema, arr, target, gameObject.GetComponent<LevelControl>());
        }
        doorFigures.Add(doorFig);
        Invoke("MoveNextDoorFigure", 1.5f);
    }

    private void MoveNextDoorFigure()
    {
        for (int i = 0; i < doorFigures.Count; i++)
        {
            //DoorFigControl dfc = doorFigures[doorFigures.Count - 1].GetComponent<DoorFigControl>();
            DoorFigControl dfc = doorFigures[i].GetComponent<DoorFigControl>();
            if (dfc != null && dfc.IsNew)
            {
                dfc.ViewAndMove();
            }
        }
    }

    public bool TestPacking(GameObject door)
    {
        if (_levelEnviroment != null)
        {
            if (_levelEnviroment.CheckPacking(door))
            {
                doorFigures.Remove(door);
                if (_levelEnviroment.DoorGridFull())
                {   //  дорожка построена
                    _isDoorFull = true;
                    MoveKolobok();
                    Invoke("ViewEndPanel", 5f);
                }
                return true;
            }
        }
        return false;
    }

    public void RemoveDoorFigure(GameObject door)
    {
        doorFigures.Remove(door);
    }

    private void MoveKolobok()
    {
        List<Vector3> path = new List<Vector3>();
        path.Add(new Vector3(5.75f, 1f, 1f));
        path.Add(new Vector3(-5.25f, 1f, -7f));
        path.Add(new Vector3(-5.25f, 1f, -10f));
        _kolobokMovement.SetPath(path);
    }

    private void ViewEndPanel()
    {
        _ui_control.ViewEndPanel();
    }
}
