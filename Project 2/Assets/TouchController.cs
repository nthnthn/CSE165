using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class TouchController : MonoBehaviour {
    public LineRenderer leftLine;
    public LineRenderer rightLine;
    public bool leftTriggerTouch;
    public bool rightTriggerTouch;
    public bool leftGrip;
    public bool rightGrip;
    public bool menu = false;
    public bool leftTriggerReady = true;
    public bool rightTriggerReady = true;
    public OVRInput.Controller leftController;
    public OVRInput.Controller rightController;
    public Vector3 leftPosition;
    public Vector3 rightPosition;
    public Quaternion leftRotation;
    public Quaternion rightRotation;
    public int mode = 0;
    public GameObject leftSphere;
    public GameObject rightSphere;
    public GameObject canvas;
    public GameObject text;
    public int spawn = 0;
    public List<string> objects;
    public Text words;
    public List<GameObject> selected;
    public bool groupSelect = false;
    public GameObject group;
    public GameObject world;
    public float leftGrab = 0.0f;
    public float rightGrab = 0.0f;
    public bool grabbing = false;
    public Rigidbody rb;
    public Behaviour halo;

    // Use this for initialization
    void Start () {
        //leftSphere.SetActive(false);
        //rightSphere.SetActive(false);
        leftSphere.GetComponent<MeshRenderer>().enabled = false;
        rightSphere.GetComponent<MeshRenderer>().enabled = false;
        canvas.SetActive(false);
        objects.Add("3DTV");
        objects.Add("cabinet");
        objects.Add("chair");
        objects.Add("desk");
        objects.Add("locker");
        objects.Add("whiteboard");
    }
	
	// Update is called once per frame
	void Update () {
        leftTriggerTouch = OVRInput.Get(OVRInput.RawTouch.LIndexTrigger);
        rightTriggerTouch = OVRInput.Get(OVRInput.RawTouch.RIndexTrigger);
        leftGrip = OVRInput.Get(OVRInput.RawButton.LHandTrigger);
        rightGrip = OVRInput.Get(OVRInput.RawButton.RHandTrigger);
        leftPosition = OVRInput.GetLocalControllerPosition(leftController) + transform.position;
        rightPosition = OVRInput.GetLocalControllerPosition(rightController) + transform.position;
        leftRotation = OVRInput.GetLocalControllerRotation(leftController);
        rightRotation = OVRInput.GetLocalControllerRotation(rightController);
        leftGrab = OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger);
        rightGrab = OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger);

        Ray leftRay = new Ray(leftSphere.transform.position, leftSphere.transform.rotation * Vector3.forward);
        RaycastHit leftRayHit;
        Ray rightRay = new Ray(rightSphere.transform.position, rightSphere.transform.rotation * Vector3.forward);
        RaycastHit rightRayHit;

        leftLine.SetPosition(0, leftSphere.transform.position);
        rightLine.SetPosition(0, rightSphere.transform.position);

        if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) == 0) leftTriggerReady = true;
        if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) == 0) rightTriggerReady = true;

        if (OVRInput.GetDown(OVRInput.RawButton.A) || OVRInput.GetDown(OVRInput.RawButton.X)) {
            mode = (mode + 1) % 3;
            if (mode == 1)
            {
                //leftSphere.SetActive(true);
                //rightSphere.SetActive(true);
            }
            else
            {
                //leftSphere.SetActive(false);
                //rightSphere.SetActive(false);
            }
            if (mode == 2)
            {
                canvas.SetActive(true);
            }
            else canvas.SetActive(false);
        }

        switch (mode)
        {
            case 0: //Raycast select and manipulation and teleport
                if (OVRInput.GetDown(OVRInput.RawButton.LThumbstick) || OVRInput.GetDown(OVRInput.RawButton.RThumbstick)) groupSelect = !groupSelect;
                if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) == 1.0f || OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) == 1.0f)
                {
                    foreach (GameObject x in selected)
                    {
                        if (x == null) continue;
                        halo = (Behaviour)x.GetComponent("Halo");
                        if (halo) halo.enabled = false;
                        x.transform.SetParent(world.transform);
                    }
                    selected.Clear();
                }



                if (!leftTriggerTouch && leftGrip)
                {
                    if (Physics.Raycast(leftRay, out leftRayHit, Mathf.Infinity))
                    {
                        leftLine.SetPosition(1, leftRayHit.point);
                        if (leftRayHit.collider.gameObject.tag == "Floor" && OVRInput.GetDown(OVRInput.RawButton.Y))
                        {
                            transform.position = leftRayHit.point;
                        }
                        if (leftRayHit.collider.gameObject.tag == "Moveables" && !groupSelect && selected.Count == 0)
                        {
                            selected.Add(leftRayHit.collider.gameObject);
                            leftRayHit.collider.gameObject.transform.SetParent(group.transform);
                            halo = (Behaviour)leftRayHit.collider.gameObject.GetComponent("Halo");
                            if (halo) halo.enabled = true;
                        }
                        else if (leftRayHit.collider.gameObject.tag == "Moveables" && groupSelect)
                        {
                            selected.Add(leftRayHit.collider.gameObject);
                            leftRayHit.collider.gameObject.transform.SetParent(group.transform);
                            halo = (Behaviour)leftRayHit.collider.gameObject.GetComponent("Halo");
                            if (halo) halo.enabled = true;
                        }
                    }
                    else
                    {
                        leftLine.SetPosition(1, leftSphere.transform.position);
                    }
                }
                else
                {
                    leftLine.SetPosition(1, leftSphere.transform.position);
                }

                if (!rightTriggerTouch && rightGrip)
                {
                    if (Physics.Raycast(rightRay, out rightRayHit, Mathf.Infinity))
                    {
                        rightLine.SetPosition(1, rightRayHit.point);
                        if (rightRayHit.collider.gameObject.tag == "Floor" && OVRInput.GetDown(OVRInput.RawButton.B))
                        {
                            transform.position = rightRayHit.point;
                        }
                    }
                    else
                    {
                        rightLine.SetPosition(1, rightSphere.transform.position);
                    }
                }
                else
                {
                    rightLine.SetPosition(1, rightSphere.transform.position);
                }

                if (leftGrab > 0.0f && leftTriggerTouch)
                {
                    group.transform.SetParent(leftSphere.transform);
                    foreach (GameObject x in selected)
                    {
                        if (x == null) continue;
                        rb = x.GetComponent<Rigidbody>();
                        rb.isKinematic = true;
                    }
                    grabbing = true;
                }
                else if ((!leftTriggerTouch || leftGrab == 0.0f) && grabbing == true)
                {
                    group.transform.SetParent(world.transform);
                    foreach (GameObject x in selected)
                    {
                        if (x == null) continue;
                        rb = x.GetComponent<Rigidbody>();
                        rb.isKinematic = false;
                        rb = null;
                    }
                    grabbing = false;
                }

                break;

            case 1: // Gogo hand
                rightLine.SetPosition(1, rightSphere.transform.position);
                leftLine.SetPosition(1, leftSphere.transform.position);

                break;

            case 2: // Spawning
                if (Physics.Raycast(leftRay, out leftRayHit, Mathf.Infinity))
                {
                    leftLine.SetPosition(1, leftRayHit.point);
                    if (leftRayHit.collider.gameObject.tag == "Floor" && leftTriggerReady && (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) != 0))
                    {
                        leftTriggerReady = false;
                        GameObject spawned = (GameObject)Instantiate(Resources.Load(objects[spawn]));
                        spawned.transform.SetParent(world.transform);
                        spawned.GetComponent<Rigidbody>().position = new Vector3(leftRayHit.point.x, 1.0f, leftRayHit.point.z);
                    }
                    if (leftRayHit.collider.gameObject.tag == "Floor" && OVRInput.GetDown(OVRInput.RawButton.Y))
                    {
                        transform.position = leftRayHit.point;
                    }
                }
                else
                {
                    leftLine.SetPosition(1, leftSphere.transform.position);
                }
                if (Physics.Raycast(rightRay, out rightRayHit, Mathf.Infinity))
                {
                    rightLine.SetPosition(1, rightRayHit.point);
                    if (rightRayHit.collider.gameObject.tag == "Floor" && rightTriggerReady && (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) != 0))
                    {
                        rightTriggerReady = false;
                        GameObject spawned = (GameObject)Instantiate(Resources.Load(objects[spawn]));
                        spawned.transform.SetParent(world.transform);
                        spawned.GetComponent<Rigidbody>().position = new Vector3(rightRayHit.point.x, 1.0f, rightRayHit.point.z);
                    }
                    if(rightRayHit.collider.gameObject.tag == "Floor" && OVRInput.GetDown(OVRInput.RawButton.B))
                        {
                        transform.position = rightRayHit.point;
                    }
                }
                else
                {
                    rightLine.SetPosition(1, rightSphere.transform.position);
                }


                if (OVRInput.GetDown(OVRInput.RawButton.LThumbstick) || OVRInput.GetDown(OVRInput.RawButton.RThumbstick))
                {
                    spawn = (spawn + 1) % objects.Count;
                    words = text.GetComponent<Text>();
                    words.text = objects[spawn];
                }
                    break;
        }

    }


}
