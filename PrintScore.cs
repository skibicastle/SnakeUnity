using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PrintScore : MonoBehaviour
{
    public Text scoreText;
    public GameObject food;
    void Update()
    {
        string score = Convert.ToString(food.GetComponent<SpawnPoint>().score);
        if(score.Length == 2)
            scoreText.text = "00" + score;
        if (score.Length == 3)
            scoreText.text = "0" + score;
        if (score.Length >= 4)
            scoreText.text =  score;
    }
}
