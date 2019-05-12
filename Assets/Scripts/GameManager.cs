﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void ActionDelegate();

    public ActionDelegate restart;

    public UIManager UIManager;

    public SpawnCircles circles;
    public GameMechanicsParameters GameMechanicsParameters;

    public string highscoreTextHeader;
    public string scoreTextHeader;

    public bool isGameRunning;

    #region SINGLETON
    private static GameManager _i;
    public static GameManager i
    {
        get
        {
            if(_i == null)
            {
                _i = GameObject.FindObjectOfType<GameManager>();
            }
            return _i;
        }
        set
        {
            _i = value;
        }
    }
    #endregion

    #region SAVEGAME
    private SaveGame _savegame;
    public SaveGame savegame
    {
        get
        {
            if (_savegame == null)
            {
                _savegame = new SaveGame();
                _savegame.Load();
            }
            return _savegame;
        }

    }
    #endregion

    void Awake()
    {
        restart += LoadGame;
        restart += ResumeGame;
    }

    void Update()
    {
        if (isGameRunning)
        {
            savegame.score += Mathf.Round(Time.unscaledDeltaTime * 100.0f) / 100.0f;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = GameMechanicsParameters.pausedTimeScale;
        isGameRunning = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = GameMechanicsParameters.defaultTimeScale;
        isGameRunning = true;
    }

    public void RestartGame()
    {
        if (restart != null)
        {
            restart.Invoke();
        }
    }

    void LoadGame()
    {
        savegame.Load();
        ResetCurrentScore();
        Debug.Log("Loading, score: " + savegame.score + ", highscore: " + savegame.highscore);
    }

    public void GameOver()
    {
        PauseGame();
        if (savegame.score > savegame.highscore)
            savegame.highscore = savegame.score;
        savegame.Save();
        UIManager.HidePanel(UIManager.gameUIPanel);
        UIManager.ShowPanel(UIManager.gameOverPanel);
    }

    public void ResetCurrentScore()
    {
        savegame.score = 0f;
    }
}
