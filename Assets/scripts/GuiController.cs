using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

    public GameObject background;
    public GameObject bulletText;
    

	// Use this for initialization
	void Start () {
    }

    public void RefreshPositions() {
        var v3Pos = new Vector3(1.0f, 1.0f, 10f);
        bulletText.transform.position = Camera.main.ViewportToWorldPoint(v3Pos);

        v3Pos = new Vector3(0.5f, 1.0f, 15f);
        background.transform.position = Camera.main.ViewportToWorldPoint(v3Pos);
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


        bulletText.GetComponent<TextMesh>().text = clipTextToDisplay + "/" + inventoryTextToDisplay;
    }
}
