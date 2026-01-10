using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnviroment : MonoBehaviour
{
    [SerializeField] private GameObject[] envPrefabs;
    [SerializeField] private GameObject gridCeilPrefab;
    [SerializeField] private float ofsX;
    [SerializeField] private float ofsZ;

    private ShemaLevel _shemaLevel;

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
                }
            }
        }
    }
}
