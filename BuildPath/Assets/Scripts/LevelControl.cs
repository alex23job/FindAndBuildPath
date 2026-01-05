using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private SpawnCandyControl _spawnCandyControl;
    [SerializeField] private SpawnFigControl _spawnFigControl;
    [SerializeField] private Vector3 _basePointCollectFigure;

    private int _maxSpawnPoints = 6;
    private int _levelSpawnPoints = 6;

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
}
