using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class Steering : MonoBehaviour {
    public float pitch = 0.0f;
    public float yaw = 0.0f;
    static public float speed = 0.0f;
    public static bool canMove = true;
    public Quaternion addRot = Quaternion.identity;

    private void Update()
    {
        if (canMove)
        {
            addRot.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + addRot.eulerAngles);
            transform.position = transform.position + transform.forward * speed;
        }
    }


    public void turn(int type)
    {
        switch (type)
        {
            case 0:
                pitch = -0.2f;
                break;
            case 1:
                yaw = 0.7f;
                break;
            case 2:
                pitch = 0.2f;
                break;
            case 3:
                yaw = -0.7f;
                break;
        }
    }

    public void offTurn(int type)
    {
        switch (type)
        {
            case 0:
                pitch = 0.0f;
                break;
            case 1:
                yaw = 0.0f;
                break;
            case 2:
                pitch = 0.0f;
                break;
            case 3:
                yaw = 0.0f;
                break;
        }
    }

    public void stop()
    {
        speed = 0.0f;
    }

    public void go()
    {
        speed = 0.2f;
    }

    public void inc()
    {
        speed += 0.1f;
        Debug.Log(speed.ToString());
    }
}