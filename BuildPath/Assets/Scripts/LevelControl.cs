using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private SpawnCandyControl _spawnCandyControl;
    [SerializeField] private SpawnFigControl _spawnFigControl;
    [SerializeField] private Vector3 _basePointCollectFigure;
    [SerializeField] private GameObject _doorFigPrefab;

    private int _maxSpawnPoints = 6;
    private int _levelSpawnPoints = 6;

    private List<GameObject> doorFigures = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Invoke("CreateCollectFigures", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            target.z = 0;target.y = 0.8f;
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
}
