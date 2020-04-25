using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    private Camera _camera;
    private Duster player;
    private List<GameObject> asteroids = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        player = transform.parent.gameObject.GetComponent<Duster>();
    }

    // Update is called once per frame
    void Update()
    {
        bool incomingRight = false;
        bool incomingLeft = false;

        Vector3 screenPos;

        for (int i = 0; i<asteroids.Count; i++)
        {
            if (asteroids[i] == null)
            {
                asteroids.RemoveAt(i);
                if (asteroids.Count == 0) continue;
            }
            screenPos = _camera.WorldToScreenPoint(asteroids[i].transform.position);
            if (screenPos.x < 0) incomingLeft = true;
            else if (screenPos.x > _camera.pixelWidth) incomingRight = true;
        }

        player.Alert(true, incomingLeft);
        player.Alert(false, incomingRight);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            asteroids.Add(other.gameObject);
        }
    }
}
