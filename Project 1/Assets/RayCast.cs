using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RayCast : MonoBehaviour {
    public int mode = 0;
    public GameObject target;
    public float startTime;
    public GameObject cannonball;
    public Rigidbody cannonbody;
    public LineRenderer line;
    public Vector3 startPos;
    public float laserTime = -1.0f;
    public Canvas canvas;
    public Image circularSlider;
    public WallSpawner wallspawn;
    public OVRCameraRig mainCamera;
    public bool aimAssist = false;
    float resetCooldown;
    public Vector3 canvasScale;

    // Use this for initialization
    void Start () {
        circularSlider.fillAmount = 0.0f;
        wallspawn = GameObject.FindObjectOfType(typeof(WallSpawner)) as WallSpawner;
        line.startColor = Color.green;
    }

    void RebuildWall(){
        GameObject[] wall;
        wall = GameObject.FindGameObjectsWithTag("Brick");
        foreach (GameObject g in wall){
            Destroy(g);
        }
        wallspawn.StartCoroutine("BuildWall");
    }

    // Update is called once per frame
    void Update () {
        Ray myRay = new Ray(transform.position, transform.forward);
        RaycastHit rayHit;

        if (Input.GetKey("1")){
            mode = 1;
        }
        else if (Input.GetKey("2")){
            mode = 2;
        }
        else if (Input.GetKey("0")){
            mode = 3;
        }
        else if (Input.GetKey("r")){
            RebuildWall();
            transform.position = startPos;
            mode = 0;
        }

        if (laserTime > 0 && laserTime + 0.3f <= Time.time){
            laserTime = -1.0f;
            line.SetPosition(1, transform.position);
            line.startColor = Color.green;
        }
        if (Physics.Raycast(myRay, out rayHit, Mathf.Infinity)){
            canvas.transform.position = rayHit.point - Vector3.Normalize(rayHit.point - transform.position) * 0.01f ;
            canvas.transform.rotation = Quaternion.FromToRotation(Vector3.forward, rayHit.normal);
            canvas.transform.localScale = canvasScale * rayHit.distance;
            if (aimAssist)
            {
                line.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z));
                line.SetPosition(1, rayHit.point);
            }
            else
            {
                line.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z));
                line.SetPosition(1, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z));
            }
            if (target == null || target != rayHit.collider.gameObject){
                target = rayHit.collider.gameObject;
                startTime = Time.time;
            }
            else if (target == rayHit.collider.gameObject){
                circularSlider.fillAmount = (Time.time - startTime) / 2.0f;
                if ((startTime + 2.0f) <= Time.time){
                    startTime = Time.time;
                    if (rayHit.collider.gameObject.tag == "Brick"){
                        if (mode == 1){
                            line.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z));
                            line.SetPosition(1, rayHit.point);
                            laserTime = Time.time;
                            line.startColor = Color.red;
                            Destroy(rayHit.collider.gameObject);
                        }
                        else if (mode == 2){
                            cannonball = (GameObject)Instantiate(Resources.Load("Sphere"));
                            cannonbody = cannonball.GetComponent<Rigidbody>();
                            cannonbody.rotation = Quaternion.identity;
                            cannonbody.velocity = transform.forward * 10.0f;
                            cannonbody.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);                                                        
                        }
                    }
                    else if (rayHit.collider.gameObject.tag == "Look"){
                        mode = 0;
                        aimAssist = !aimAssist;
                    }
                    else if (rayHit.collider.gameObject.tag == "Laser"){
                        mode = 1;
                    }
                    else if (rayHit.collider.gameObject.tag == "Cannon"){
                        mode = 2;
                    }
                    else if (rayHit.collider.gameObject.tag == "Respawn"){
                        if (resetCooldown + 3.0f <= Time.time)
                        {
                            RebuildWall();
                            mode = 0;
                            mainCamera.transform.position = startPos;
                            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                            resetCooldown = Time.time;
                        }
                    }
                    else if (rayHit.collider.gameObject.tag == "Floor"){
                        Vector3 tempPos = rayHit.point;
                        tempPos.y = mainCamera.transform.position.y;
                        mainCamera.transform.position = tempPos;
                    }
                    Debug.Log(mode);
                }
            }
        }
        else{
            target = null;
            circularSlider.fillAmount = 0.0f;
            line.SetPosition(0, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z));
            line.SetPosition(1, new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z));
        }
	}
}
