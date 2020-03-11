using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Asteroid : MonoBehaviour
{
    public int scoreValue;
    protected Duster player;

    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 1;
        player = FindObjectOfType<Player>().GetComponent<Duster>();
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
