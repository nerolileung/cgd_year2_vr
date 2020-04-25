using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private int mode;

    [SerializeField]
    private string aimAxisX;
    [SerializeField]
    private string aimAxisY;
    [SerializeField]
    private string cycleButton;
    [SerializeField]
    private string fireButton;

    private MeshRenderer _renderer;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material = Duster.Modes[mode];
    }

    // Update is called once per frame
    void Update()
    {
        //aim
        if (Input.GetButtonDown(fireButton)) Fire();
        if (Input.GetButtonDown(cycleButton)) CycleMode();
    }

    private void CycleMode()
    {
        mode++;
        if (mode > 3) mode = 0;
        _renderer.material = Duster.Modes[mode];
    }

    private void Fire()
    {

    }
}