using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public int mode = 0;
    public Vector2 leftInput;
    public Vector2 rightInput;
    public float pitch, yaw, roll;
    private Quaternion addRot;
    public GameObject heading;

    // Use this for initialization
    void Start () {
        heading.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.RawButton.Start))
        {
            mode = (mode + 1) % 3;
            if (mode == 0) heading.SetActive(false);
            else heading.SetActive(true);
        }
        leftInput = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        rightInput = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);


        switch (mode)
        {
            case 0: // Movement off
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.05f);
                break;
            case 1: // Plane mode
                addRot = Quaternion.identity;
                transform.position = transform.position + transform.forward * leftInput.y * 0.02f;
                pitch = rightInput.y * 0.3f;
                yaw = leftInput.x * 0.3f;
                roll = rightInput.x * 0.3f;
                addRot.eulerAngles = new Vector3(pitch, yaw, -roll);
                transform.rotation *= addRot;
                break;
            case 2: // Car mode
                addRot = Quaternion.identity;
                transform.position = transform.position + transform.forward * leftInput.y * 0.02f;
                pitch = 0.0f;
                yaw = leftInput.x * 0.3f;
                roll = 0.0f;
                addRot.eulerAngles = new Vector3(pitch, yaw, -roll);
                transform.rotation *= addRot;
                break;
        }
	}
}
