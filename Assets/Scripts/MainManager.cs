using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Highscore[] HighscoreTable;
    public string PlayerName;
    [SerializeField] private int NumberOfScores = 3;

    public class Highscore
    {
        public string PlayerName;
        public int Score;
    }

    [Serializable]
    private class SaveData
    {
        public Highscore[] HighscoreTable;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            // Initialize highscore array
            HighscoreTable = new Highscore[NumberOfScores];

            for (int i = 0; i < HighscoreTable.Length; i++)
            {
                HighscoreTable[i] = new Highscore();
            }

            // Create persistent singleton
            Instance = this;
            DontDestroyOnLoad(gameObject);

            //Load highscores
            LoadHighscore();
        }
    }

    public void SaveHighScore()
    {
        SaveData saveData = new SaveData();
        saveData.HighscoreTable = HighscoreTable;

        string HighscoreTableJSON = JsonUtility.ToJson(saveData.HighscoreTable);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", HighscoreTableJSON);
    }

    public void LoadHighscore()
    {
        string filePath = Application.persistentDataPath + "/highscore.json";

        if (File.Exists(filePath))
        {
            string HighscoreTableJSON = File.ReadAllText(filePath);
            SaveData saveData = new SaveData();

            saveData.HighscoreTable = JsonUtility.FromJson<Highscore[]>(HighscoreTableJSON);

            for (int i = 0; i < saveData.HighscoreTable.Length; i++)
            {
                if (saveData.HighscoreTable[i] != null)
                {
                    HighscoreTable[i] = saveData.HighscoreTable[i];
                }
            }
        }
    }
}
