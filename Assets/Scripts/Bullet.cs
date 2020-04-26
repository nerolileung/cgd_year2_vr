using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private Rigidbody _rigidbody;
    private Vector3 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        damage = 1f;

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(startingPos, transform.position) > 10) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
        if (asteroid != null)
        {
            asteroid.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
