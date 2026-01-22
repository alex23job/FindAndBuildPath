using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Main_UI_Control : MonoBehaviour
{
    [SerializeField] private Image[] _imgLevels;
    [SerializeField] private GameObject _selectPanel;

    private Color _selectColor = new Color(1f, 0.45f, 0.03f, 1f);
    private Color _noSelectColor = new Color(1f, 0.45f, 0.03f, 0f);

    private int _curLevel = 0;
    private int _maxLevel = 10;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelsButton();
        LevelList.CurrentLevel = _curLevel;
        LevelList.MaxLevel = _maxLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevelScene()
    {
        _selectPanel.SetActive(false);
        SceneManager.LoadScene("LevelScene");
    }

    public void LoadLevelEditor()
    {
        SceneManager.LoadScene("LevelEditorScene");
    }

    public void OnBtnLevelClick(int num)
    {
        
        if (num >= 0 && num < _imgLevels.Length)
        {
            _imgLevels[_curLevel].color = _noSelectColor;
            _curLevel = num;
            _imgLevels[num].color = _selectColor;
            LevelList.CurrentLevel = _curLevel;
        }
    }

    public void ViewSelectLevelPanel()
    {
        _selectPanel.SetActive(true);
    }

    private void UpdateLevelsButton()
    {
        for (int i = 0; i < _imgLevels.Length; i++)
        {
            _imgLevels[i].color = (i != _curLevel) ? _noSelectColor : _selectColor;
            _imgLevels[i].gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = (i < _maxLevel) ? true : false;
        }
    }
}
