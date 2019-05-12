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
    public Text newHighscoreTextBox;
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
        if (GameManager.i.savegame.score >= GameManager.i.savegame.savedScore.highscore)
        {
            newHighscoreTextBox.enabled = true;
        }
        else newHighscoreTextBox.enabled = false;
        mainMenuHighscoreTextBox.text = gameOverHighscoreTextBox.text = GameManager.i.highscoreTextHeader + GameManager.i.savegame.highscore.ToString("F2");
        gameOverScoreTextBox.text = GameManager.i.scoreTextHeader + GameManager.i.savegame.score.ToString("F2");
    }

    public void SetTimeElapsedText()
    {
        timeElapsedTextBox.text = GameManager.i.timeElapsedTextHeader + GameManager.i.savegame.score.ToString("F2");
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
