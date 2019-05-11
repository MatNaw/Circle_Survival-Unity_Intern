using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void ActionDelegate();

    public ActionDelegate restart;

    public UIManager UIManager;

    public SpawnCircles circles;
    public GameMechanicsParameters GameMechanicsParameters;

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

    }

    public void ResumeGame()
    {

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
        Debug.Log("Score: " + savegame.score + ", highscore: " + savegame.highscore);
        isGameRunning = true; //test
    }

    public void GameOver()
    {
        if (savegame.score > savegame.highscore) savegame.highscore = savegame.score;
        savegame.Save();
        isGameRunning = false;
        UIManager.HidePanel(UIManager.gameUIPanel);
        UIManager.ShowPanel(UIManager.gameOverPanel);
    }
}
