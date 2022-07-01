using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInputUI : MonoBehaviour
{
    public void OnEndEdit(string name)
    {
        MainManager.Instance.PlayerName = name;
        SceneManager.LoadScene("main");
    }
}
