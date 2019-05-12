using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameUIPanel;
    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;
    public Text mainMenuHighscoreTextBox;
    public Text gameOverHighscoreTextBox;
    public Text gameOverScoreTextBox;
    public Text timeElapsedTextBox;

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

    public void SetHighscoreText()
    {
        mainMenuHighscoreTextBox.text = gameOverHighscoreTextBox.text = GameManager.i.highscoreTextHeader + GameManager.i.savegame.highscore.ToString();
        gameOverScoreTextBox.text = GameManager.i.scoreTextHeader + GameManager.i.savegame.score.ToString();
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
