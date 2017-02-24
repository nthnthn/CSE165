using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campus : MonoBehaviour {

	// Use this for initialization
	void Start () {
		foreach(Transform child in transform)
        {
            child.gameObject.AddComponent<MeshCollider>();
            child.gameObject.GetComponent<MeshCollider>().convex = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
