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

    private Rigidbody _rigidbody;
    private Duster player;

    // Start is called before the first frame update
    void Start()
    {
        value = 0;
        health = 1f;

        _rigidbody = GetComponent<Rigidbody>();

        player = GameObject.Find("Player").GetComponent<Duster>();

        transform.forward = player.transform.position - transform.position;
        _rigidbody.AddForce(transform.forward);
    }

    public void Initialise(float difficulty)
    {
        health *= 1 + difficulty;
        transform.localScale *= 1 + (difficulty * 0.01f);
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
