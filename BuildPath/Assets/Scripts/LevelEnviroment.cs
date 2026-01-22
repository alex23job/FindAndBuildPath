using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnviroment : MonoBehaviour
{
    [SerializeField] private GameObject prefabTailBody;
    [SerializeField] private GameObject[] envPrefabs;
    [SerializeField] private GameObject gridCeilPrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject finishPrefab;
    [SerializeField] private float ofsX;
    [SerializeField] private float ofsZ;

    private ShemaLevel _shemaLevel;

    private int[] _doorGrid = null;
    private GameObject[] _ceils = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetShemaLevel(ShemaLevel sh)
    {
        _shemaLevel = sh;
        int i, j, num, x, typeForest = 0;
        Vector3 pos = new Vector3(0, 0.25f, 0);
        Vector3 rot = new Vector3(-90f, 0, 0);
        int[] tails = _shemaLevel.GetGroundTails();
        int[] doors = _shemaLevel.GetDoorGrids();
        int[] fullDoors = _shemaLevel.GetDoorFulls();
        for (i = 0; i < tails.Length; i++)
        {
            pos.z = ofsZ - i * 1f;
            //pos.z = ofsZ - i * 2f;
            for (j = 0; j < 13; j++)
            {
                typeForest = 0;
                num = (tails[i] >> (2 * j)) & 0x3;
                pos.x = ofsX + j * 1f;
                //pos.x = ofsX + j * 2f;
                pos.y = (num == 1) ? 0f : 0.25f;
                if (num == 3) { typeForest = Random.Range(0, 2); }
                GameObject env = Instantiate(envPrefabs[num + typeForest], pos, Quaternion.Euler(rot));
                env.transform.parent = transform;
            }
            x = (tails[i] >> 26) & 0xf;
            if (x > 0)
            {
                pos.x = ofsX + x;
                num = (tails[i] >> 30) & 0x1;
                if (num > 0)
                {
                    pos.y = 0.5f;
                    GameObject env = Instantiate(envPrefabs[6], pos, Quaternion.identity);
                    env.transform.parent = transform;
                }
                else
                {
                    pos.y = 0.5f;
                    GameObject env = Instantiate(envPrefabs[5], pos, Quaternion.identity);
                    env.transform.parent = transform;
                }                
            }
        }
        _ceils = new GameObject[doors.Length * 26];
        _doorGrid = new int[doors.Length * 26];
        for (i = 0; i < _doorGrid.Length; i++)
        {
            _doorGrid[i] = -1;
        }
        pos.y = 0.6f;
        for (i = 0; i < doors.Length; i++)
        {
            pos.z = ofsZ - i * 0.5f + 0.25f;
            for (j = 0; j < 26; j++)
            {
                num = (doors[i] >> j) & 0x1;
                if ((doors[i] & (1 << j)) > 0)
                {
                    pos.x = ofsX + j * 0.5f - 0.25f;
                    GameObject ceil = Instantiate(gridCeilPrefab, pos, Quaternion.identity);
                    ceil.transform.parent = transform;
                    _ceils[26 * i + j] = ceil;
                    _doorGrid[26 * i + j] = 0;
                }
            }
        }
        for (i = 0; i < fullDoors.Length; i++)
        {
            pos.z = ofsZ - i * 0.5f + 0.25f;
            for (j = 0; j < 26; j++)
            {
                num = (fullDoors[i] >> j) & 0x1;
                if ((fullDoors[i] & (1 << j)) > 0)
                {
                    pos.x = ofsX + j * 0.5f - 0.25f;
                    GameObject ceil = Instantiate(prefabTailBody, pos, Quaternion.identity);
                    ceil.transform.parent = transform;
                    _ceils[26 * i + j] = ceil;
                    _doorGrid[26 * i + j] = 1;
                }
            }
        }
        Vector2 startPos = _shemaLevel.StartPoint;
        pos.y = 0f;
        pos.x = ofsX + 0.5f * startPos.x - 0.25f;
        pos.z = ofsZ - 0.5f * startPos.y + 0.45f;
        GameObject start = Instantiate(startPrefab, pos, Quaternion.identity);
        start.transform.parent = transform;
        Vector2 finishPos = _shemaLevel.FinishPoint;
        pos.y = 0.5f;
        pos.x = ofsX + 0.5f * finishPos.x - 0.25f;
        pos.z = ofsZ - 0.5f * finishPos.y + 0.25f;
        GameObject finish = Instantiate(finishPrefab, pos, Quaternion.identity);
        finish.transform.parent = transform;
    }

    public List<Vector3> GetPathPoints()
    {
        int[] fullDoors = _shemaLevel.GetDoorFulls();
        List<Vector3> pathPoints = new List<Vector3>();
        int x, y;
        Vector3 point = new Vector3(0, 1f, 0);
        for (y = 0; y < fullDoors.Length; y++)
        {
            x = (fullDoors[y] >> 26) & 0x0F;
            if (x > 0)
            {
                point.x = ofsX + 0.5f * x - 0.25f;
                point.z = ofsZ - 0.5f * y + 0.25f;
                pathPoints.Add(point);
            }
        }
        return pathPoints;
    }


    public bool CheckPacking(GameObject door)
    {
        DoorFigControl dfc = door.GetComponent<DoorFigControl>();
        Vector3 pos;
        int i, x, z, index, tailCount = 0;
        for (i = 0; i < dfc.Tails.Count; i++)
        {
            pos = dfc.Tails[i].transform.position;
            x = Mathf.RoundToInt((pos.x - ofsX + 0.25f) * 2);
            z = Mathf.RoundToInt((ofsZ - pos.z + 0.25f) * 2);
            index = 26 * z + x;
            //Debug.Log($"pos=<{pos}>   x={x}  z={z}   index={index}    zn={(((index >= 0) && (index < _doorGrid.Length)) ? _doorGrid[index] : -256)}");
            if (index >= 0 && index < _doorGrid.Length)
            {
                if (_doorGrid[index] == 0) tailCount++;
            }
        }
        if (tailCount > 0 && tailCount == dfc.Tails.Count)
        {
            for (i = 0; i < dfc.Tails.Count; i++)
            {
                pos = dfc.Tails[i].transform.position;
                x = Mathf.RoundToInt((pos.x - ofsX + 0.25f) * 2);
                z = Mathf.RoundToInt((ofsZ - pos.z + 0.25f) * 2);
                index = 26 * z + x;
                _doorGrid[index] = 1;
                dfc.Tails[i].transform.parent = transform;
                dfc.Tails[i].transform.localPosition = _ceils[index].transform.localPosition;
                Destroy(_ceils[index]);
                _ceils[index] = dfc.Tails[i];
            }
            return true;
        }
        return false;
    }

    public bool DoorGridFull()
    {
        for (int i = 0; i < _doorGrid.Length; i++)
        {
            if (_doorGrid[i] == 0) return false;
        }
        return true;
    }

    public void SetKolobok(Transform transformKolobok)
    {
        Vector3 startPos = Vector3.zero;
        startPos.x = ofsX + 0.5f * _shemaLevel.StartPoint.x - 0.25f;
        startPos.y = 1.5f;
        startPos.z = ofsZ - 0.5f * _shemaLevel.StartPoint.y + 1.3f;
        transformKolobok.position = startPos;
    }
}
