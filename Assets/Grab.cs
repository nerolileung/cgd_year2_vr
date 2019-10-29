using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grab : MonoBehaviour
{
    public bool debug;
    public SteamVR_Input_Sources handType;

    public SteamVR_Action_Boolean grab;
    public bool GetGrab() { return grab.GetState(handType); }

    private GameObject pickedUp;
    private Transform originalTransform;
    private Transform grabbedTransform;
    private bool hasPickedUp = false;


    private Vector3 previousPosition = Vector3.zero;
    private Quaternion previousRotation = Quaternion.identity;

    // Start is called before the first frame update, sets up the position and rotation of the controller
    void Start()
    {
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        // Process the letting go of the object

        if (pickedUp != null)
        {
            if (!GetGrab())
            {
                if (debug)
                    Debug.Log("Dropped: " + pickedUp.name + " with " + gameObject.name);
                pickedUp.GetComponent<Rigidbody>().isKinematic = false;

                pickedUp.transform.SetParent(originalTransform);

                ApplyLastVelocityAndAngularVelocity(pickedUp.GetComponent<Rigidbody>(), transform);

                pickedUp = null;
                originalTransform = null;
                grabbedTransform = null;
                hasPickedUp = false;


            }
        }
        // Update position and rotation of controller
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }


    // Process picking up

    void OnTriggerStay(Collider other)
    {
        if (hasPickedUp) return;
        grabbedTransform = other.transform;
        if (!grabbedTransform.CompareTag("Grabbable")) return;

        if (GetGrab() && pickedUp == null)
        {


            pickedUp = grabbedTransform.gameObject;
            pickedUp.GetComponent<Rigidbody>().isKinematic = true;
            originalTransform = grabbedTransform.parent;

            other.transform.gameObject.transform.SetParent(transform);

            hasPickedUp = true;
        }
    }

    // Allow object to inherit velocity from controller

    private void ApplyLastVelocityAndAngularVelocity(Rigidbody rigidBody, Transform controllerTransform)
    {
        rigidBody.velocity = ((controllerTransform.position - previousPosition) / Time.deltaTime);

        Quaternion deltaRotation = controllerTransform.rotation * Quaternion.Inverse(previousRotation);
        Vector3 eulerRotation = new Vector3(
     Mathf.DeltaAngle(0, Mathf.Round(deltaRotation.eulerAngles.x)),
     Mathf.DeltaAngle(0, Mathf.Round(deltaRotation.eulerAngles.y)),
     Mathf.DeltaAngle(0, Mathf.Round(deltaRotation.eulerAngles.z)));

        rigidBody.angularVelocity = (eulerRotation * Mathf.Deg2Rad / Time.deltaTime) * 0.75f;
    }

}
