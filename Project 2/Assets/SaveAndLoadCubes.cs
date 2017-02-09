using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoadCubes : MonoBehaviour {
    public SaveAndLoad script;
    public GameObject measuringSphere;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == measuringSphere)
        {
            Debug.Log("Collide");
            if (name == "LoadCube") script.ReadFile();
            else if (name == "SaveCube") script.RecordData();
        }
    }
}
