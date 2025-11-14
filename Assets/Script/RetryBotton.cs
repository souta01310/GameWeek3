using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class RetryBotton : MonoBehaviour
{
    [SerializeField] public string sceneName;
    

    public void RetryBotton1()
    {
        SceneManager.LoadScene(sceneName);
    }
}
