﻿using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private Text scoreText;

    private void Awake()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
    }

    public void incrementScore()
    {
        scoreText.text = (int.Parse(scoreText.text) + 1).ToString();
    }
}