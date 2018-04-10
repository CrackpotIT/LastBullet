using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public int startSeconds;
    private int timer;

    // Use this for initialization
    void Start () {
        timer = startSeconds;
    }
	
	// Update is called once per frame
	void Update () {
        int levelStartSeconds = Mathf.RoundToInt(Time.timeSinceLevelLoad);
        if (startSeconds - levelStartSeconds != timer) {
            // es ist eine Sekunde oder mehr vergangen!
            timer = startSeconds - levelStartSeconds;
            GuiController.GetInstance().RefreshTimer(timer);
        }

    }
}
