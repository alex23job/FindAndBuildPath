using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnviroment : MonoBehaviour
{
    [SerializeField] private GameObject prefabTailBody;
    [SerializeField] private GameObject[] envPrefabs;
    [SerializeField] private GameObject gridCeilPrefab;
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
        int i, j, num;
        Vector3 pos = new Vector3(0, 0.25f, 0);
        Vector3 rot = new Vector3(-90f, 0, 0);
        int[] tails = _shemaLevel.GetGroundTails();
        int[] doors = _shemaLevel.GetDoorGrids();
        for (i = 0; i < tails.Length; i++)
        {
            pos.z = ofsZ - i * 1f;
            for (j = 0; j < 13; j++)
            {
                num = (tails[i] >> (2 * j)) & 0x3;
                pos.x = ofsX + j * 1f;                
                GameObject env = Instantiate(envPrefabs[num], pos, Quaternion.Euler(rot));
                env.transform.parent = transform;
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
            Debug.Log($"pos=<{pos}>   x={x}  z={z}   index={index}    zn={(index < _doorGrid.Length ? _doorGrid[index] : -256)}");
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
}
