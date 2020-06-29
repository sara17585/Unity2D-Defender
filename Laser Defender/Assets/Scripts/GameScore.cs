using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{

    int score = 0;

    private void Awake()
    {
        SetUpSingelton();
    }

    private void SetUpSingelton()
    {
        int numberGameSessions = FindObjectsOfType<GameScore>().Length;
        if (numberGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

        public void ResetGame()
    {
        Destroy(gameObject);
    }
   
}
