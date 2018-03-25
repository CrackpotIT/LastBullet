using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour {

    private TextMesh bulletText;

	// Use this for initialization
	void Start () {
        bulletText = GetComponent<TextMesh>();

        var v3Pos = new Vector3(1.0f, 1.0f, 10f);
        transform.position = Camera.main.ViewportToWorldPoint(v3Pos);
    }

    private void Update() {
        var v3Pos = new Vector3(1.0f, 1.0f, 10f);
        transform.position = Camera.main.ViewportToWorldPoint(v3Pos);
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

        bulletText.text = clipTextToDisplay + "/" + inventoryTextToDisplay;
    }
}
