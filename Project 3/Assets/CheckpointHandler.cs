using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour {

    private List<Vector3> points;
    static List<GameObject> checkpoints;
    static int activeCheckpoint = 1;
    private GameObject checkpoint;

    private void Start()
    {
        Load("data.txt");
        CreateCheckpoints(points);
    }

    /* Read in text file to create list of points for the checkpoints */
    private void Load(string file)
    {
        string[] lines = System.IO.File.ReadAllLines(file);
        foreach (string line in lines)
        {
            string[] list = line.Split();
            float x = float.Parse(list[0]);
            float y = float.Parse(list[1]);
            float z = float.Parse(list[2]);
            Debug.Log("x: " + x + "\ny: " + y + "\nz: " + z);
            Vector3 point = new Vector3(x, y, z);
            points.Add(point);
        }
    }

    /* Create a list of checkpoints using the list of points */
    private void CreateCheckpoints(List<Vector3> points)
    {
        foreach (Vector3 point in points)
        {
            /* TODO:Create checkpoint prefab
            //Create checkpoints at each checkpoint
            checkpoint = (GameObject)Instantiate(Resources.Load("Checkpoint"));
            checkpoint.transform.position = point;
            */

        }
    }

    /* Player reached a checkpoint, increment the index and update the current checkpoint and next checkpoint */
    static public void NextCheckpoint()
    {
        int oldActive = CheckpointHandler.activeCheckpoint;
        checkpoints[activeCheckpoint].SetActive(false);
        activeCheckpoint++;
        if (activeCheckpoint >= checkpoints.Count)
        {
            //winner, stop game and timer and display
        }
        else
        {
            checkpoints[activeCheckpoint].SetActive(true);
            Debug.Log("Checkpoint " + oldActive + " reached");
            if (activeCheckpoint + 1 < checkpoints.Count)
            {
                checkpoints[activeCheckpoint+1].SetActive(true); // Change this so that its different from the current checkpoint
            }
        }
    }
}
