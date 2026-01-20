using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevEdit_UI_Control : MonoBehaviour
{
    [SerializeField] private Text txtLevelName;
    [SerializeField] private Button btnUndo;
    [SerializeField] private Button[] btnArr;
    [SerializeField] private InputField inputNumber;

    [SerializeField] private GameObject selectLoadLevelPanel;
    [SerializeField] private Image[] items;
    [SerializeField] private Scrollbar scrollbar;

    private ShemaLevel curLevel = null;

    public ShemaLevel Level {  get { return curLevel; } }

    // Start is called before the first frame update
    void Start()
    {
        selectLoadLevelPanel.SetActive(false);
        InterUndo(false);
        InterBtnArr(false);
        Text placeholder = inputNumber.gameObject.transform.GetChild(0).GetComponent<Text>();
        placeholder.text = "¬ведите номер уровн€ ...";
        placeholder.alignment = TextAnchor.MiddleLeft;
        placeholder.color = new Color(0.5f, 0.5f, 0.8f, 1f);
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

    public void OnNumberChanged(string strNumber)
    {
        strNumber = inputNumber.text;
        print($"input => {strNumber}");
        if (int.TryParse(strNumber, out int number))
        {
            if (number > 0 && Level != null)
            {
                Level.SetNumber(number);
            }
        }
    }

    public void ViewSelectLoadLevelPanel()
    {
        selectLoadLevelPanel.SetActive(true);
    }

    public void SelectLoadLevel(int numItem)
    {
        print($"NumItem => {numItem}");
        selectLoadLevelPanel.SetActive(false);
    }
}
