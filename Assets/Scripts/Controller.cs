using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private int mode;
    private MeshRenderer _renderer;

    private Camera _camera;
    [SerializeField]
    private bool usingMouse;
    [SerializeField]
    private string cycleButton;
    [SerializeField]
    private string fireButton;

    private GameObject loadedBullet;

    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = Duster.Modes[mode];

        _camera = Camera.main;

        loadedBullet = Duster.Bullets[mode];

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        //aim
        Vector3 temp;
        if (usingMouse)
        {
            temp = Input.mousePosition;
            temp.z = 0.5f;
            temp = _camera.ScreenToWorldPoint(temp);
            if (temp != transform.position) transform.SetPositionAndRotation(temp, transform.rotation);

            // setting temp for reuse with turning logic
            temp = Input.mousePosition;
        }
        else
        {
            temp = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            if (temp != Vector3.zero)
            {
                temp.x += transform.localPosition.x;
                temp.y += transform.localPosition.y;
                temp.z = 1f;

                transform.localPosition = temp;
                KeyboardControllerLock();
            }
            // translate to screen for turn
            temp = _camera.WorldToScreenPoint(transform.position);
        }

        //turn if within left or right 10% of screen
        if (temp.x/_camera.pixelWidth <= 0.1f) transform.parent.Rotate(0f, -1f, 0f);
        else if (temp.x / _camera.pixelWidth >= 0.9f) transform.parent.Rotate(0f, 1f, 0f);

        if (Input.GetButtonDown(fireButton)) Fire();
        if (Input.GetButtonDown(cycleButton)) CycleMode();
    }

    private void CycleMode()
    {
        mode++;
        if (mode > 3) mode = 0;
        _renderer.material = Duster.Modes[mode];
        loadedBullet = Duster.Bullets[mode];
    }

    private void Fire()
    {
        Instantiate(loadedBullet,transform.position,transform.parent.rotation);
    }

    private void KeyboardControllerLock()
    {
        // if go off screen, don't
        Vector3 screenPosition = _camera.WorldToScreenPoint(transform.position);
        bool positionChanged = false;
        // horizontal
        if (screenPosition.x < 0.5f)
        {
            screenPosition.x = 0.5f;
            positionChanged = true;
        }
        else if (screenPosition.x > _camera.pixelWidth-0.5f)
        {
            screenPosition.x = _camera.pixelWidth - 0.5f;
            positionChanged = true;
        }
        //vertical
        if (screenPosition.y < 0.5f)
        {
            screenPosition.y = 0.5f;
            positionChanged = true;
        }
        else if (screenPosition.y > _camera.pixelHeight-0.5f)
        {
            screenPosition.y = _camera.pixelHeight - 0.5f;
            positionChanged = true;
        }

        if (positionChanged) transform.position = _camera.ScreenToWorldPoint(screenPosition);
    }
}