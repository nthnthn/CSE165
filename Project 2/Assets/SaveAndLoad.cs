using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveAndLoad : MonoBehaviour {
    public GameObject recordObject;
    public GameObject loadObject;
    private StreamWriter file;
    public string[] data;

    // Use this for initialization
    void Start () {

        //RecordData();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("s")) RecordData();
        if (Input.GetKeyDown("l")) ReadFile();
    }

    public void RecordData()
    {
        Debug.Log("Saving");
        file = new StreamWriter("data.txt");
        foreach (Transform trans in transform)
        {
            if (trans.gameObject.name == "MeasuringSphere" || trans.gameObject.name == "Group") continue;
            recordObject = trans.gameObject;
            file.WriteLine(recordObject.name.Split('(')[0] + " " + trans.position.x + " " + trans.position.y + " " + trans.position.z + " " + trans.rotation.x + " " + trans.rotation.y + " " + trans.rotation.z + " " + trans.rotation.w);
        }
        file.Close();
    }

    public void ReadFile()
    {
        string[] lines = System.IO.File.ReadAllLines("data.txt");

        foreach (Transform trans in transform)
        {
            if (trans.name == "Group") continue;
            Destroy(trans.gameObject);
        }

        Debug.Log("Contents of data.txt:");
        foreach(string line in lines)
        {
            data = line.Split(' ');
            loadObject = (GameObject)Instantiate(Resources.Load(data[0]));
            loadObject.transform.SetParent(transform);
            loadObject.GetComponent<Rigidbody>().position = new Vector3(float.Parse(data[1]), float.Parse(data[2]), float.Parse(data[3]));
            loadObject.GetComponent<Rigidbody>().rotation = new Quaternion(float.Parse(data[4]), float.Parse(data[5]), float.Parse(data[6]), float.Parse(data[7]));
        }
    }
}
