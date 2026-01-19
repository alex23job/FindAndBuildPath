using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoard : MonoBehaviour
{
    [SerializeField] private LevEdit_UI_Control editControl;

    [SerializeField] private GameObject holmPrefab;
    [SerializeField] private GameObject seePrefab;
    [SerializeField] private GameObject forestPrefab;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject conturPrefab;
    [SerializeField] private GameObject ceilPrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject finishPrefab;

    [SerializeField] private float ofsX;
    [SerializeField] private float ofsY;

    private List<GameObject> grid = new List<GameObject>();
    private GameObject selectPrefab = null;
    private int selectSize = 0;
    private List<GameObject> tails = new List<GameObject>();

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

    public void AddHolm()
    {
        selectPrefab = holmPrefab;
        selectSize = 2;
    }

    public void AddSee()
    {
        selectPrefab = seePrefab;
        selectSize = 2;
    }

    public void AddDoor()
    {
        selectPrefab = doorPrefab;
        selectSize = 1;
    }

    public void AddForest()
    {
        selectPrefab = forestPrefab;
        selectSize = 2;
    }

    public void AddContur()
    {
        selectPrefab = conturPrefab;
        selectSize = 1;
    }
    public void AddStart()
    {
        selectPrefab = startPrefab;
        selectSize = 3;
    }
    public void AddFinish()
    {
        selectPrefab = finishPrefab;
        selectSize = 3;
    }

    public void Undo()
    {
        if (tails.Count > 0)
        {
            GameObject tail = tails[tails.Count - 1];
            EditTail et = tail.GetComponent<EditTail>();


            Destroy(tail, 0.5f);
            tails.RemoveAt(tails.Count - 1);
        }
        editControl.InterUndo(tails.Count > 0);
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.RoundToInt(pos.x - ofsX);
            int y = Mathf.RoundToInt(2 * (ofsY - pos.z));
            
            if (editControl != null && selectPrefab != null)
            {
                Vector3 posTail = new Vector3(0, 0.5f, 0);
                if (selectSize == 1)
                {
                    posTail.x = ofsX + x;
                    posTail.z = ofsY - 0.5f * y;
                }
                if (selectSize > 1)
                {
                    posTail.x = ofsX + (x / 2) * 2 + 0.5f;
                    posTail.z = ofsY - y / 2 - 0.25f;
                }
                GameObject tail = Instantiate(selectPrefab, posTail, Quaternion.identity);
                //editControl.Level
                tails.Add(tail);
                editControl.InterUndo(true);
                selectPrefab = null;
                print($"{Input.mousePosition}    wordPos=>{pos}    x={x}   y={y}    tailPos=>{posTail}");
            }
        }
    }
}
