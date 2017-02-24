using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {
    float timeUntilRelease;
    Quaternion lastOrientation;
    private bool start = true;
    public AudioSource source;
    public AudioSource source2;
    public AudioClip startSound, checkSound, finishSound;

	// Use this for initialization
	void Start () {
        timeUntilRelease = Time.time + 3.0f;
        Steering.canMove = false;
        source.PlayOneShot(startSound, 1.0f);
        source2 = GetComponent<AudioSource>();
        source2.loop = true;
        source2.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > timeUntilRelease)
        {
            Steering.canMove = true;
        }
        source2.pitch = Steering.speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Checkpoint")
        {
            if (other.gameObject == CheckpointHandler.checkpoints[CheckpointHandler.activeCheckpoint].gameObject)
            {
                start = false;
                CheckpointHandler.NextCheckpoint();
                Debug.Log("Hit a Checkpoint");
                lastOrientation = transform.rotation;
                if (Timer.finish)
                {
                    //source.PlayOneShot(finishSound, 1.0f);
                }
                else
                {
                    //source.PlayOneShot(checkSound, 1.0f);
                }
            }
        }
        
        else
        {
            Debug.Log("Crashed!");
            transform.position = CheckpointHandler.checkpoints[CheckpointHandler.activeCheckpoint - 1].transform.position;
            if (!start)
            {
                transform.rotation = lastOrientation;
            }
            else transform.LookAt(new Vector3(CheckpointHandler.checkpoints[CheckpointHandler.activeCheckpoint].transform.position.x, transform.position.y, CheckpointHandler.checkpoints[CheckpointHandler.activeCheckpoint].transform.position.z));
            timeUntilRelease = Time.time + 3.0f;
            Steering.canMove = false;
        }    
    }

}
