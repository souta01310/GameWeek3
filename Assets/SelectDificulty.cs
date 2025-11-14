using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectDificulty : MonoBehaviour
{
    string sceneName = "MainScene";
    public void Easy()
    {
        Debug.Log("easy");
        GameManeger.Instance.difficultyLevel = "easy";
        SceneManager.LoadScene(sceneName);
    }
    public void Nomal()
    {
        GameManeger.Instance.difficultyLevel= "normal";
        SceneManager.LoadScene(sceneName);
    }
    public void Hard()
    {
        GameManeger.Instance.difficultyLevel = "hard";
        SceneManager.LoadScene(sceneName);
    }
}
