using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighscoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private float m_WaitBeforeHighscore = 3.0f; //seconds

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            StartCoroutine(WaitAndGoToHighscore());
        }

        ShowHighscore();
    }

    private IEnumerator WaitAndGoToHighscore()
    {
        yield return new WaitForSeconds(m_WaitBeforeHighscore);
        AddScoreToHighscoreTable(m_Points, MainManager.Instance.PlayerName);
        MainManager.Instance.SaveHighScore();
        SceneManager.LoadScene("highscore");
    }

    private void ShowHighscore()
    {
        string PlayerName = MainManager.Instance.PlayerName;
        int HighscoreScore = MainManager.Instance.HighscoreTable[0].Score;
        string HighscorePlayer = MainManager.Instance.HighscoreTable[0].PlayerName;

        if (m_Points <= HighscoreScore)
        {
            HighscoreText.text = $"Best Score : {HighscorePlayer} : {HighscoreScore}";
        }
        else
        {
            HighscoreText.text = $"Best Score : {PlayerName} : {m_Points}";
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    private void AddScoreToHighscoreTable(int score, string playerName)
    {
        string playerToBump;
        int scoreToBump;
       
        for (int i = 0; i < MainManager.Instance.HighscoreTable.Length; i++)
        {
            // Check if the player scored better than the three highscores
            if (score > MainManager.Instance.HighscoreTable[i].Score)
            {
                // Save the data of the surpassed player in variables
                playerToBump = MainManager.Instance.HighscoreTable[i].PlayerName;
                scoreToBump = MainManager.Instance.HighscoreTable[i].Score;

                // Add score to the highscore table
                MainManager.Instance.HighscoreTable[i].PlayerName = playerName;
                MainManager.Instance.HighscoreTable[i].Score = score;
                  
                // Put surpassed player in existing variables to bump down the list
                playerName = playerToBump;
                score = scoreToBump;
            }
        }
    }
}
