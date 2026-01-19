using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevEdit_UI_Control : MonoBehaviour
{
    [SerializeField] private Text txtLevelName;
    [SerializeField] private Button btnUndo;
    [SerializeField] private Button[] btnArr;

    private ShemaLevel curLevel = null;

    public ShemaLevel Level {  get { return curLevel; } }

    // Start is called before the first frame update
    void Start()
    {
        InterUndo(false);
        InterBtnArr(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void CreateNewLevelShema()
    {
        curLevel = LevelList.Instance.CreateNewShema();
        txtLevelName.text = curLevel.IDS_LEVEL;
        InterBtnArr(true);
    }

    public void SaveLevels()
    {
        LevelList.Instance.SaveLevels();
    }

    public void InterUndo(bool value)
    {
        btnUndo.interactable = value;
    }
    public void InterBtnArr(bool value)
    {
        foreach (var btn in btnArr)
        {
            btn.interactable = value;
        }
    }
}
