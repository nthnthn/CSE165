using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Measuring : MonoBehaviour {
    public Vector3 a;
    public Vector3 b;
    public bool reset = false;
    public GameObject text;
    private Text words;
    public int cycle = 0;
    private float distance;

    // Use this for initialization
    void Start () {
        words = text.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        // Squeeze both triggers on either hand
        if ((OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) == 1.0f && OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) == 1.0f) || (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) == 1.0f && OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) == 1.0f) && reset == false)
        {
            if (cycle == 0)
            {
                a = transform.position;
                words.text = "Point1";
            }
            else if (cycle == 1)
            {
                b = transform.position;
                distance = Vector3.Distance(a, b);
                words.text = distance.ToString();
            }
            cycle = (cycle + 1) % 2;
            reset = true;
        }
        // Release both triggers on both hands
        else if ((OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) == 0.0f && OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) == 0.0f) && (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) == 0.0f && OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) == 0.0f) && reset == true) reset = false;
	}
}
