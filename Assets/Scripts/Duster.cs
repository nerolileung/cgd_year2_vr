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
    private static AudioSource hurtSound;

    private float timerFull = 5f;
    private float timerCurrent;

    public static Material[] Modes = new Material[4];
    public static GameObject[] Bullets = new GameObject[4];

    private static GameObject[] Asteroids = new GameObject[4];
    private Vector2 previousAsteroidAngles;
    private static float difficulty;
    private static int asteroidCount;

    public static bool deathFreeze;

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

        previousAsteroidAngles = new Vector2(0, 0);
        asteroidCount = 0;
        difficulty = 0;

        hurtSound = GetComponent<AudioSource>();

        deathFreeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!deathFreeze)
        {
            // asteroid spawnin
            timerCurrent -= Time.deltaTime;
            if (timerCurrent <= 0)
            {
                SpawnAsteroid();
                timerCurrent = timerFull - Random.Range(0f, 2f); // possibly increase difficulty
            }
        }
    }

    public void ChangeScore(int value)
    {
        if (!deathFreeze)
        {
            score += value;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    public void TakeDamage(float damage)
    {
        if (!hurtSound.isPlaying) hurtSound.Play();

        if (!deathFreeze)
        {
            currentHealth -= damage;
            uiAnimator.SetTrigger("Injured");

            if (currentHealth <= 0)
            {
                uiAnimator.SetTrigger("Death");
                deathFreeze = true;
            }

            healthBar.anchorMax = new Vector2(currentHealth / maxHealth, 1f);
        }
    }

    public void Alert(bool left, bool on)
    {
        if (left) uiAnimator.SetBool("IncomingLeft", on);
        else uiAnimator.SetBool("IncomingRight", on);
    }

    private void SpawnAsteroid()
    {
        Vector3 newPos;
        // xpos
        //determine distance of angle
        float angleMaxDist = 30f;
        if (Random.Range(0f, 1f) < 0.25) angleMaxDist = 55f;

        float angleH = GetRandomHorizontalAngle();
        while (Mathf.Abs(angleH - previousAsteroidAngles.x) > angleMaxDist)
        {
            angleH = GetRandomHorizontalAngle();
        }

        newPos.x = 10 * Mathf.Cos(angleH * Mathf.Deg2Rad);
        if (Random.Range(0f, 1f) <= 0.5f) newPos.x *= -1;

        // ypos - compensate for camera position
        float angleV = Random.Range(0f, 2f);
        newPos.y = (10 * Mathf.Sin(angleV * Mathf.Deg2Rad)) + 0.5f;

        // zpos
        newPos.z = 100 - (Mathf.Pow(newPos.x,2f) + Mathf.Pow(newPos.y,2f));
        newPos.z = Mathf.Sqrt(Mathf.Abs(newPos.z));
        if (Random.Range(0f, 1f) <= 0.5f) newPos.z *= -1;

        GameObject asteroid = Instantiate(Asteroids[Random.Range(0, 4)], newPos, Quaternion.identity);
        asteroid.GetComponent<Asteroid>().Initialise(difficulty);

        asteroidCount++;
        if (asteroidCount % 10 == 0) difficulty += 0.1f;

        previousAsteroidAngles.x = angleH;
        previousAsteroidAngles.y = angleV;
    }

    /// <summary> returns random angle in degrees for horizontal asteroid position </summary>
    private float GetRandomHorizontalAngle()
    {
        float angle;
        float odds = Random.Range(0f,1f);
        if (odds > 0.55f) angle = Random.Range(55f, 70f); // 1-0.55, 45%
        else if (odds > 0.25f) angle = Random.Range(0f, 55f); //0.55-0.25, 30%
        else if (odds > 0.05f) angle = Random.Range(70f, 85f); //0.25-0.05, 20%
        else angle = Random.Range(85f, 110f); //10%
        return angle;
    }
}