using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class HighscoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] NameTable = new TextMeshProUGUI[3];
    [SerializeField] private TextMeshProUGUI[] ScoreTable = new TextMeshProUGUI[3];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < NameTable.Length; i++)
        {
            NameTable[i].SetText(MainManager.Instance.HighscoreTable[i].PlayerName);
            ScoreTable[i].SetText("" + MainManager.Instance.HighscoreTable[i].Score);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("menu");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}
