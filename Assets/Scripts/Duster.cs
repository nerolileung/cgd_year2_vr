using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Duster : MonoBehaviour
{
    private static int score = 0;
    private static Text scoreText;

    private const float maxHealth = 20f;
    private static float currentHealth = maxHealth;
    private static RectTransform healthBar;

    private static Animator uiAnimator;

    public static Material[] Modes = new Material[4];
    public static GameObject[] Bullets = new GameObject[4];
    public static GameObject[] Asteroids = new GameObject[4];

    private void Awake()
    {
        // more sensibly this would be in a gamemanager
        for (int i = 0; i < 4; i++)
        {
            Material tempMat = Resources.Load<Material>("Materials/Mode " + i);
            if (tempMat !=null) Modes[i] = tempMat;

            GameObject tempBullet = Resources.Load<GameObject>("Prefabs/Bullet " + i);
            if (tempBullet != null) Bullets[i] = tempBullet;

            GameObject tempAsteroid = Resources.Load<GameObject>("Prefabs/Asteroid " + i);
            if (tempAsteroid != null) Asteroids[i] = tempAsteroid;
        }

        uiAnimator = GameObject.Find("Canvas").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
        scoreText.text = "Score: " + score.ToString();

        healthBar = GameObject.Find("Health Bar").GetComponent<RectTransform>();
        healthBar.anchorMax = new Vector2(currentHealth / maxHealth, 1f);
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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        uiAnimator.SetTrigger("Injured");
        
        if (currentHealth <= 0)
        {
            // die
        }

        healthBar.anchorMax = new Vector2(currentHealth / maxHealth, 1f);
    }

    public void Alert(bool left, bool on)
    {
        if (left) uiAnimator.SetBool("IncomingLeft", on);
        else uiAnimator.SetBool("IncomingRight", on);
    }
}