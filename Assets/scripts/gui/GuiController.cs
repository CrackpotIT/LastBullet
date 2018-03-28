using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

    public GameObject background;
    public GameObject backgroundLeft;
    public GameObject backgroundCenter;
    public GameObject backgroundRight;
    public PixelArtDisplayCounter bulletCounter;
    public PixelArtDisplayCounter timer;


    // Use this for initialization
    void Start () {
        
    }

    private void Update() {
        
        
    }

    public void RefreshPositions() {
        SetGUIPosition(background, 0.5f, 1.0f, 0, 0);
        SetGUIPosition(backgroundLeft, 0f, 1.0f, 0, 0);
        SetGUIPosition(backgroundCenter, 0.5f, 1.0f, 0, 0);
        SetGUIPosition(backgroundRight, 1f, 1.0f, 0, 0);
        SetGUIPosition(bulletCounter.gameObject, 1.0f, 1.0f, -.52f, -.12f); // 0,04 = 1 Pixel

        SetGUIPosition(timer.gameObject, 1.0f, 1.0f, -.8f, -1.24f); // 0,04 = 1 Pixel

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
}
