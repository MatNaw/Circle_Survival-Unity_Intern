using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;

[System.Serializable]
public class Score
{
    public float highscore;
    //public float score;
}

public class SaveGame
{
    public float highscore;
    public float score;

    public Score savedScore;

    void Start()
    {
        ResetCurrentScore();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        //Score savedScore = new Score();
        Debug.Log("Score: " + score + ", highscore: " + highscore);
        if(highscore > savedScore.highscore) savedScore.highscore = highscore;
        //savedScore.score = score;
        bf.Serialize(file, savedScore);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            savedScore = (Score)bf.Deserialize(file);
            GameManager.i.savegame.highscore = savedScore.highscore;
            file.Close();
        }
    }

    void ResetCurrentScore()
    {
        score = 0f;
    }
}
