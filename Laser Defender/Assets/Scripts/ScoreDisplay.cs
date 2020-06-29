using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using TMPro;


public class ScoreDisplay : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    GameScore gameScore;


    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameScore = FindObjectOfType<GameScore>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = gameScore.GetScore().ToString();
    }
}
