using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;

    internal int currentScore;

    void Start () {
        currentScore = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void SetScore(int v)
    {
        currentScore = v;
        scoreText.text = "SCORE: " + currentScore;
    }

    internal void AddScore(int v)
    {
        currentScore += v;
        scoreText.text = "SCORE: " + currentScore;
    }
}
