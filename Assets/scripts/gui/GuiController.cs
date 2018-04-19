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
    public XpBar xpDisplay;
    public PixelArtDisplayCounter timerDisplay;
    public HealthDisplay healthDisplay;


    // Static instance
    static GuiController _instance;

    private void Awake() {
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
        SetGUIPosition(bulletCounter.gameObject, 1.0f, 1.0f, -.52f, -0.24f); // 0,04 = 1 Pixel
        SetGUIPosition(xpDisplay.gameObject, 0f, 1.0f, 1.68f, -1.04f); // 0,04 = 1 Pixel
        SetGUIPosition(healthDisplay.gameObject, 0f, 1.0f, 1.20f, -0.24f); // 0,04 = 1 Pixel

        SetGUIPosition(timerDisplay.gameObject, 1.0f, 1.0f, -.8f, -1.24f); // 0,04 = 1 Pixel

        RefreshHealth(1);
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

    public void RefreshXpCount(float xpPercent) {
        xpDisplay.RefreshDisplay(xpPercent);
    }

    public void RefreshHealth(int health) {
        healthDisplay.RefreshDisplay(health);
    }

    public void RefreshTimer(int time) {
        timerDisplay.RefreshDisplay(time.ToString());
    }
}
