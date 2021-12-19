using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    private int pelletsConsumed = 0;

    public void AddScore()
    {
        pelletsConsumed++;
        scoreText.text = "Score: " + pelletsConsumed.ToString();
    }
}
