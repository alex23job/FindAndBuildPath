using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_control : MonoBehaviour
{
    [SerializeField] private GameObject _endPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewEndPanel()
    {
        _endPanel.SetActive(true);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
