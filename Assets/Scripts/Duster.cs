using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Duster : MonoBehaviour
{
    private int score = 0;
    private Text scoreText;
    private GameObject modeIndicatorLeft;
    private GameObject modeIndicatorRight;

    public struct ColourMode
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
        modeIndicatorLeft = GameObject.Find("Left Mode");
        modeIndicatorRight = GameObject.Find("Right Mode");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();
    }
}
