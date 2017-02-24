using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModes : MonoBehaviour {
    private int mode = 0;
    public GameObject ship;
    public GameObject eyeAnchor;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextMode() {
        mode = (mode + 1) % 3;
        switch (mode)
        {
            case 0:
                ship.SetActive(false);
                eyeAnchor.transform.position = transform.position;
                break;
            case 1:
                ship.SetActive(true);
                break;
            case 2:
                eyeAnchor.transform.position = transform.position + transform.forward * -4.0f;
                break;
        }
    }
}
