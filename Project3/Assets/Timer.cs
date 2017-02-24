using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    private float time = -3.0f;
    public GameObject text;
    private Text words;
    public static bool finish = false;

    // Use this for initialization
    void Start () {
        words = text.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!finish)
        {
            time += Time.deltaTime;
            words.text = time.ToString();
        }
        else { }
    }
}
