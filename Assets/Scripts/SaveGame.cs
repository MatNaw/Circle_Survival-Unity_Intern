using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System;

[System.Serializable]
public class Score
{
    public float highscore;
}

public class SaveGame
{
    public float highscore;
    public float score;

    public Score savedScore = new Score();

    public void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
            if (highscore > savedScore.highscore) savedScore.highscore = highscore;
            bf.Serialize(file, savedScore);
            file.Close();
        }
        catch (System.Exception)
        {
            Debug.LogError("saveingerror");
        }
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
                savedScore = (Score)bf.Deserialize(file);
                GameManager.i.savegame.highscore = savedScore.highscore;
                file.Close();
            }
            catch (System.Exception)
            {
                init();
                Save();
            }
        }
        else
        {
            init();
            Save();
        }
    }

    void init()
    {
        savedScore.highscore = score = highscore = 0f;
    }
}
