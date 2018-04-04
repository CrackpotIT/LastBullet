﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

    public GameObject background;
    public GameObject backgroundLeft;
    public GameObject backgroundCenter;
    public GameObject backgroundRight;
    public PixelArtDisplayCounter bulletCounter;
    public PixelArtDisplayCounter xpCounter;
    public PixelArtDisplayCounter timer;


    // Static instance
    static GuiController _instance;

    private void Start() {
        _instance = this;
    }


    public static GuiController GetInstance() {
        if (!_instance) {
            Debug.LogError("GuiController Error, instance is null!");
            return null;
        }
        return _instance;
    }



    public void RefreshPositions() {
        SetGUIPosition(background, 0.5f, 1.0f, 0, 0);
        SetGUIPosition(backgroundLeft, 0f, 1.0f, 0, 0);
        SetGUIPosition(backgroundCenter, 0.5f, 1.0f, 0, 0);
        SetGUIPosition(backgroundRight, 1f, 1.0f, 0, 0);
        SetGUIPosition(bulletCounter.gameObject, 1.0f, 1.0f, -.52f, -.12f); // 0,04 = 1 Pixel
        SetGUIPosition(xpCounter.gameObject, 0f, 1.0f, 3.6f, -0.84f); // 0,04 = 1 Pixel

        SetGUIPosition(timer.gameObject, 1.0f, 1.0f, -.8f, -1.24f); // 0,04 = 1 Pixel


        RefreshXpCount(100);
    }

    private void SetGUIPosition(GameObject go, float x, float y, float offsetX, float offsetY) {
        float z = go.transform.position.z;
        go.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 0));
        go.transform.position = new Vector3(go.transform.position.x + offsetX, go.transform.position.y + offsetY, z);
    }

    public void RefreshBulletCount(int clip, int inventory) {
        string clipTextToDisplay = clip.ToString();
        if (clipTextToDisplay.Length < 2) {
            clipTextToDisplay = "0" + clipTextToDisplay;
        }

        string inventoryTextToDisplay = inventory.ToString();
        if (inventoryTextToDisplay.Length < 2) {
            inventoryTextToDisplay = "0" + inventoryTextToDisplay;
        }
        if (inventoryTextToDisplay.Length < 3) {
            inventoryTextToDisplay = "0" + inventoryTextToDisplay;
        }

        bulletCounter.RefreshDisplay(clipTextToDisplay + "/" + inventoryTextToDisplay);
    }

    public void RefreshXpCount(int xp) {
        string clipTextToDisplay = xp.ToString();        

        xpCounter.RefreshDisplay(clipTextToDisplay);
    }
}
