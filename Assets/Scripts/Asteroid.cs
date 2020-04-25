using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int scoreValue;
    private Duster player;

    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 1;
        player = GameObject.Find("Player").GetComponent<Duster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        player.ChangeScore(scoreValue);
        Destroy(gameObject);
    }
}
