using UnityEngine;
using UnityEngine.UI;

public class TurfGameScore : MonoBehaviour
{
    [SerializeField] 
    public GameObject[] hexagons;

    public Text scoreText;

    int score = 0;

    // Start is called before the first frame update
    void CheckScore()
    {
        foreach(GameObject hexagon in hexagons)
        {
            if (hexagon.GetComponent<Renderer>().material.color == Color.green)
            {
                score++;
            }
            scoreText.text = "Player Score: " + score.ToString();
        }
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckScore();    
    }
}
