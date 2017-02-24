using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour {
    public GameObject brick;
    public float radius;
    public int numberOfObjects;
    public int numberOfLayers;
    public Vector3 origin = new Vector3(0f, 0f, 0f);
    public bool offset = false;

    public IEnumerator BuildWall(){
        Debug.Log("Building Wall...");
        brick = (GameObject)Resources.Load("Cube");
        radius = 2f;
        numberOfObjects = 29;
        numberOfLayers = 12;
        float angle;

        for (int j = 0; j < numberOfLayers; j++){
            for (int i = 0; i < numberOfObjects; i++){
                if (offset) angle = i * Mathf.PI * 2 / numberOfObjects;
                else angle = (i + 0.5f) * Mathf.PI * 2 / numberOfObjects;
                Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, origin - pos);
                pos.y = 3f + (j * .5f);
                Instantiate(brick, pos, rot);
                yield return new WaitForSeconds(0.005f);
            }
            offset = !offset;
        }
    }

    // Use this for initialization
    void Start() {
        StartCoroutine("BuildWall");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
