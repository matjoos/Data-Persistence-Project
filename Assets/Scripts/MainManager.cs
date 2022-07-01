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

    [Serializable]
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
            //Initialize highscore table
            HighscoreTable = InitializeHighscoreTable(HighscoreTable);

            // Create persistent singleton
            Instance = this;
            DontDestroyOnLoad(gameObject);

            //Load highscores
            LoadHighscore();
        }
    }

    public void SaveHighscore()
    {
        SaveData saveData = new SaveData();
        saveData.HighscoreTable = InitializeHighscoreTable(saveData.HighscoreTable);
        saveData.HighscoreTable = HighscoreTable;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    public void LoadHighscore()
    {
        string filePath = Application.persistentDataPath + "/highscore.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            if (saveData.HighscoreTable != null)
            {
                HighscoreTable = saveData.HighscoreTable;
            }
        }
    }

    private Highscore[] InitializeHighscoreTable(Highscore[] Table)
    {
        Table = new Highscore[NumberOfScores];

        for (int i = 0; i < Table.Length; i++)
        {
            Table[i] = new Highscore();
        }

        return Table;
    }
}
