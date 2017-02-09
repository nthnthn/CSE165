using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualHand : MonoBehaviour {
    public GameObject target;
    public GameObject world;
    public Rigidbody rb;
    public bool grabbing = false;
    public float grip = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.tag == "LeftHand") grip = OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger);
        else grip = OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger);

        if (grip > 0.0f && target != null)
        {
            target.transform.SetParent(transform);
            rb = target.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            grabbing = true;
        }
        else if (grip == 0.0f && target != null && grabbing == true)
        {
            target.transform.SetParent(world.transform);
            rb.isKinematic = false;
            rb = null;
            grabbing = false;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Moveables")
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Moveables")
        {
            target = null;
        }
    }
}
