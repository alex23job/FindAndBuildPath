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
    private int curIndexNumbers = 0;
    private List<int> numbers = new List<int>();

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
        numbers = LevelList.Instance.GetLevelsNumbers();
        curIndexNumbers = 0;
        scrollbar.gameObject.SetActive(numbers.Count > items.Length);
        UpdateNumberLevelItems();
        selectLoadLevelPanel.SetActive(true);
    }

    private void UpdateNumberLevelItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            GameObject item = items[i].gameObject;
            if (curIndexNumbers + i < numbers.Count)
            {
                Text txtBtn = item.transform.GetChild(1).GetChild(0).GetComponent<Text>();
                if (txtBtn != null) txtBtn.text = numbers[i].ToString();
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }

    public void SelectLoadLevel(int numItem)
    {
        print($"NumItem => {numItem}");
        GameObject item = items[numItem].gameObject;
        Text btnText = item.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        if (btnText != null && int.TryParse(btnText.text, out int numLevel))
        {
            ShemaLevel tmp = LevelList.Instance.GetShemaLevel(numLevel);
            if (tmp != null)
            {
                curLevel = tmp;
            }
        }
        // ”ровень выбран, надо как-то сообщить в LevelBoard о перерисовке уровн€
        selectLoadLevelPanel.SetActive(false);
    }
}
