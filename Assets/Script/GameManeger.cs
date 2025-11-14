using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public string difficultyLevel;
    public static GameManeger Instance;

    void Awake()
    {
        // ƒVƒ“ƒOƒ‹ƒgƒ“
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
