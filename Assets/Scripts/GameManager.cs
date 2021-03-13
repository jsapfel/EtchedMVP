using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool AllUnlocked;
    public bool[] LevelUnlocked = {true, false, false};
    public int MaxLives = 10;
    public int PlayerLives = 10;
    
    void Awake ()   
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
