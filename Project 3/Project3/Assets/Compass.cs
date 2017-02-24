using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour {
    public GameObject player;
    public GameObject text;
    private Text words;
    private float distance;

	// Use this for initialization
	void Start () {
        words = text.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(player.transform.position, CheckpointHandler.checkpoints[CheckpointHandler.activeCheckpoint].transform.position);
        words.text = distance.ToString();
        transform.LookAt(CheckpointHandler.checkpoints[CheckpointHandler.activeCheckpoint].transform.position, Vector3.up);
    }
}
