using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    public int startSeconds;

    private PixelArtDisplayCounter displayCounter;
    private int timer;
    private int t;
    private int h;

    // Use this for initialization
    void Start () {
        displayCounter = GetComponent<PixelArtDisplayCounter>();
        timer = startSeconds;
        t = 0;
        h = 0;
    }
	
	// Update is called once per frame
	void Update () {
        int levelStartSeconds = Mathf.RoundToInt(Time.timeSinceLevelLoad);
        if (startSeconds - levelStartSeconds != timer) {
            t++;
            h++;
            // es ist eine Sekunde oder mehr vergangen!
            timer = startSeconds - levelStartSeconds;
            displayCounter.RefreshDisplay(timer.ToString());
            GuiController.GetInstance().RefreshXpCount(t);
            GuiController.GetInstance().RefreshBulletCount(10, t);

            if (h > 5) {
                h = 0;
            }
            GuiController.GetInstance().RefreshHealth(h);
        }

    }
}
