using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class RetryBotton : MonoBehaviour
{
    [SerializeField] public string retry = "MainScene";
    [SerializeField] public string title = "TitleScene";
    

    public void Retry()
    {
        SceneManager.LoadScene(retry    );

    }

    public void Title()
    {
        SceneManager.LoadScene(title);
    }
}
