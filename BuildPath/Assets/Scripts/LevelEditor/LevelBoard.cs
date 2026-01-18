using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoard : MonoBehaviour
{
    [SerializeField] private GameObject holmPrefab;
    [SerializeField] private GameObject seePrefab;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject conturPrefab;
    [SerializeField] private GameObject ceilPrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject finishPrefab;

    [SerializeField] private float ofsX;
    [SerializeField] private float ofsY;

    private List<GameObject> grid = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateGrid()
    {
        int i, j;
        Vector3 pos = Vector3.zero;
        pos.y = 1f;
        for (i = 0; i < 26; i++)    //  Z
        {
            pos.z = ofsY - 0.5f * i;
            for (j = 0; j < 26; j += 2)    //  X
            {
                pos.x = ofsX + j + i % 2;
                GameObject ceil = Instantiate(ceilPrefab, pos, Quaternion.identity);
                grid.Add(ceil);
            }
        }
    }
}
