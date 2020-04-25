using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    private float timerFull = 5f;
    [SerializeField]
    private float timerCurrent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerCurrent -= Time.deltaTime;
        if (timerCurrent <= 0)
        {
            SpawnAsteroid();
            timerCurrent = timerFull - Random.Range(0f,2f); // possibly increase difficulty
        }
    }

    private void SpawnAsteroid()
    {
        // spawn asteroid in 150 degrees
        Instantiate(Duster.Asteroids[Random.Range(0, 4)], Vector3.zero, Quaternion.identity);
    }
}
