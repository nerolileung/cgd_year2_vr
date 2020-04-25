using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // value increases as health decreases, damage = health
    private float value;
    private float health;
    //private int stage;
    //private int speed;

    private Duster player;

    // Start is called before the first frame update
    void Start()
    {
        value = 0;
        health = 1f;
        player = GameObject.Find("Player").GetComponent<Duster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Duster>() != null)
        {
            player.TakeDamage(health);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        value += Mathf.Min(damage, health);
        health -= damage;

        if (health <= 0)
        {
            player.ChangeScore(Mathf.RoundToInt(value));
            Destroy(gameObject);
        }
    }
}
