using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFigControl : MonoBehaviour
{
    [SerializeField] private GameObject _prefabCollectFigure;
    [SerializeField] private float _shiftX = 4f;
    [SerializeField] private float _shiftY = 4f;

    private List<Vector3> _spawnPoints = new List<Vector3>();
    private List<GameObject> _figures = new List<GameObject>();

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
        fig.GetComponent<CollectFigControl>().SetShema(num, ShemaFigure.GetShemaOrder(numShema).GetShema(), _spawnPoints[num]);
        _figures[num] = fig;
    }
}
