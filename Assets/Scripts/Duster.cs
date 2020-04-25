using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Duster : MonoBehaviour
{
    private static int score = 0;
    private static Text scoreText;

    private static float maxHealth = 100f;
    private static float currentHealth = maxHealth;
    private static RectTransform healthBar;

    public static Material[] Modes = new Material[4];

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            Material temp = Resources.Load<Material>("Materials/Mode " + i);
            if (temp !=null) Modes[i] = temp;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
        scoreText.text = "Score: " + score.ToString();

        healthBar = GameObject.Find("Health Bar").GetComponent<RectTransform>();
        healthBar.anchorMax.Set(1f, 1f);
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
