using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointHandler : MonoBehaviour {

    private List<Vector3> points = new List<Vector3>();
    public static List<GameObject> checkpoints = new List<GameObject>();
    public static int activeCheckpoint = 1;
    private GameObject checkpoint;
    public GameObject player;

    private void Start()
    {
        Load("Assets/Sample-track.txt");
        CreateCheckpoints(points);
        transform.localScale = new Vector3(0.0254f, 0.0254f, 0.0254f);
        CreateLines();
        player.transform.position = checkpoints[0].transform.position;
        player.transform.LookAt(new Vector3(checkpoints[activeCheckpoint].transform.position.x, player.transform.position.y, checkpoints[activeCheckpoint].transform.position.z));
        checkpoints[activeCheckpoint].SetActive(true);
        checkpoints[activeCheckpoint + 1].SetActive(true);
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
            Debug.Log("x: " + x + " y: " + y + " z: " + z);
            Vector3 point = new Vector3(x, y, z);
            points.Add(point);
        }
    }

    /* Create a list of checkpoints using the list of points */
    private void CreateCheckpoints(List<Vector3> points)
    {
        foreach (Vector3 point in points)
        {
            //Create checkpoints at each checkpoint
            checkpoint = (GameObject)Instantiate(Resources.Load("Checkpoint"));
            checkpoint.transform.position = point;
            checkpoints.Add(checkpoint);
            Debug.Log("Creating checkpoint at " + point.x + " " + point.y + " " + point.z);
            checkpoint.transform.SetParent(transform);
            checkpoint.SetActive(false);
        }
    }

    private void CreateLines()
    {
        int index = 0;
        //Create line to previous checkpoint
        foreach (GameObject checkpoint in checkpoints)
        {

            if (index > 0)
            {
                checkpoint.AddComponent<LineRenderer>();
                LineRenderer toPrevious = checkpoint.GetComponent<LineRenderer>();
                toPrevious.startColor = Color.red;
                toPrevious.endColor = Color.red;
                toPrevious.startWidth = 1.0f;
                toPrevious.endWidth = 1.0f;
                toPrevious.SetPosition(0, checkpoint.transform.position);
                toPrevious.SetPosition(1, checkpoints[index - 1].transform.position);
            }
            index++;
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
            Timer.finish = true;
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
