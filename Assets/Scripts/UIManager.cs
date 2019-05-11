using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameUIPanel;
    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;

    void Awake()
    {
        GameManager.i.UIManager = this;
    }

    void Update()
    {
        if (!GameManager.i.isGameRunning && !mainMenuPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPanel(mainMenuPanel);
        }
    }

    public void ShowPanel(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void HidePanel(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
