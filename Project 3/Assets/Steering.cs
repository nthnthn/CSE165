using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;

public class Steering : MonoBehaviour {
    public Frame currFrame;
    public Controller control;
    

	// Use this for initialization
	void Start () {
        control = new Controller();
	}
	
	// Update is called once per frame
	void Update () {
        currFrame = control.Frame();
        if(currFrame.Hands.Count > 0)
        {
            List<Hand> hands = currFrame.Hands;
            foreach(Hand hand in hands)
            {
                if(hand.IsRight)
                {
                    /*Quaternion addRot = Quaternion.identity;
                    Leap.LeapQuaternion lquat = hand.Arm.Rotation;
                    //addRot.eulerAngles = new Vector3(zBasis.Pitch, zBasis.Yaw, zBasis.Roll )* 0.1f;
                    Quaternion conversion = new Quaternion(lquat.x, lquat.y, lquat.z, lquat.w);
                    addRot.eulerAngles = new Vector3(conversion.eulerAngles.x * 0.001f, conversion.eulerAngles.y * 0.001f, conversion.eulerAngles.z * 0.001f);
                    transform.rotation *= addRot;*/
                    break;
                }
            }
        }
	}
}
