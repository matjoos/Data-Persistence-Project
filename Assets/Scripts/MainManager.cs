using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Highscore[] HighscoreTable = new Highscore[3];
    public string PlayerName;

    public class Highscore
    {
        public string PlayerName;
        public int Score;
    }

    [Serializable]
    class SaveData
    {
        public Highscore[] HighscoreTable;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighscore();
        }
    }

    public void SaveHighScore()
    {
        //Debug.Log("SaveHighscore()");

        SaveData saveData = new SaveData();
        saveData.HighscoreTable = HighscoreTable;

        string HighscoreTableJSON = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", HighscoreTableJSON);
    }

    public void LoadHighscore()
    {
        //Debug.Log("LoadHighscore()");

        string filePath = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(filePath))
        {
            string HighscoreTableJSON = File.ReadAllText(filePath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(HighscoreTableJSON);

            HighscoreTable = saveData.HighscoreTable;
        }
    }
}
