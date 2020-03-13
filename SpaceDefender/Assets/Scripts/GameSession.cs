using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public int Score { get; set; } = 0;

    void Awake()
    {
        SetUpSingleton();
    }

    public void AddToScore(int score)
    {
        Score += score;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void SetUpSingleton()
    {
        var numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
