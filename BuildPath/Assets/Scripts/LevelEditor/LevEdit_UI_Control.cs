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

    [SerializeField] private Text txtAskDel;
    [SerializeField] private GameObject askDelPanel;
    [SerializeField] private GameObject selectLoadLevelPanel;
    [SerializeField] private Image[] items;
    [SerializeField] private Scrollbar scrollbar;

    private ShemaLevel curLevel = null;
    private int curIndexNumbers = 0;
    private List<int> numbers = new List<int>();

    // Делегат для уведомления о смене уровня
    public delegate void LevelChangedEventHandler(ShemaLevel level);
    public event LevelChangedEventHandler OnLevelChanged;


    public ShemaLevel Level {  get { return curLevel; } }

    // Start is called before the first frame update
    void Start()
    {
        selectLoadLevelPanel.SetActive(false);
        InterUndo(false);
        InterBtnArr(false);
        Text placeholder = inputNumber.gameObject.transform.GetChild(0).GetComponent<Text>();
        placeholder.text = "Введите номер уровня ...";
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
        inputNumber.text = "";
        // Уровень создан, надо как-то сообщить в LevelBoard о перерисовке уровня
        OnLevelChanged?.Invoke(curLevel); // Уведомляем подписчиков

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
        //print($"input => {strNumber}");
        if (int.TryParse(strNumber, out int number))
        {
            if (number > 0 && Level != null)
            {
                Level.SetNumber(number);
            }
        }
    }

    public void ViewNumberAndIDS()
    {
        txtLevelName.text = curLevel.IDS_LEVEL;
        inputNumber.text = curLevel.NumberLevel.ToString();
    }

    public void ViewSelectLoadLevelPanel()
    {
        numbers = LevelList.Instance.GetLevelsNumbers();
        curIndexNumbers = 0;
        if (numbers.Count > items.Length)
        {
            int index = Mathf.RoundToInt(scrollbar.value * numbers.Count);
            if (index > numbers.Count - items.Length) index = numbers.Count - items.Length;
            curIndexNumbers = index;
        }
        scrollbar.gameObject.SetActive(numbers.Count > items.Length);
        scrollbar.size = ((float)items.Length) / numbers.Count; 
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
                if (txtBtn != null) txtBtn.text = numbers[curIndexNumbers + i].ToString();
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
        //print($"NumItem => {numItem}");
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
        // Уровень выбран, надо как-то сообщить в LevelBoard о перерисовке уровня
        OnLevelChanged?.Invoke(curLevel); // Уведомляем подписчиков
        selectLoadLevelPanel.SetActive(false);
    }

    public void SelectDeletingLevel(int numItem)
    {
        GameObject item = items[numItem].gameObject;
        Text btnText = item.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        if (btnText != null && int.TryParse(btnText.text, out int numLevel))
        {
            ShemaLevel tmp = LevelList.Instance.GetShemaLevel(numLevel);
            if (tmp != null)
            {
                curLevel = tmp;
                ViewAskDelPanel();
            }
        }
    }

    private void ViewAskDelPanel()
    {
        txtAskDel.text = $"Удалить {curLevel.NumberLevel} уровень?";
        askDelPanel.SetActive(true);
    }

    public void DeletingLevel()
    {
        askDelPanel.SetActive(false);
        numbers = LevelList.Instance.GetLevelsNumbersAndDelLevel(curLevel.NumberLevel);
        curLevel = null;
        curIndexNumbers = 0;
        if (numbers.Count > items.Length)
        {
            int index = Mathf.RoundToInt(scrollbar.value * numbers.Count);
            if (index > numbers.Count - items.Length) index = numbers.Count - items.Length;
            curIndexNumbers = index;
        }
        scrollbar.gameObject.SetActive(numbers.Count > items.Length);
        scrollbar.size = ((float)items.Length) / numbers.Count;
        UpdateNumberLevelItems();
    }

    public void OnScrollValueChanged(int value)
    {
        float zn = scrollbar.value;
        int index = Mathf.RoundToInt(zn * numbers.Count);
        if (index > numbers.Count - 7) index = numbers.Count - 7;
        //print($"scrollValue = {value}   zn={zn}   index={index}");
        curIndexNumbers = index;
        UpdateNumberLevelItems();
    }
}
