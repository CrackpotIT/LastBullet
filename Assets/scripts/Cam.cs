using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cam : MonoBehaviour {

    const float TEXTURE_SIZE = 25f;
	private float lastHeight = 0;
    private GuiController guiController;

    //public RectTransform displayCanvas;

    // Use this for initialization
    void Start () {

        guiController = GameObject.FindObjectOfType<GuiController>();
        UpdateOrthographicSize();

	}



	
	// Update is called once per frame
	void Update () {
        UpdateOrthographicSize();
    }


    private void UpdateOrthographicSize() {

        if (lastHeight != Screen.height) {
            lastHeight = Screen.height;

            float scaleN = Mathf.Max(Mathf.Round(Screen.height / 260f), 2);  // min scale is 2
            Debug.Log("ScaleNew: " + (Screen.height / 260f) + "-" + scaleN);

            float scale = 4f;
            if (Screen.height < 1080) {
                scale = 3f;
            }
            if (Screen.height < 512) {
                scale = 2f;
            }
            
            float erg = (Screen.height / (TEXTURE_SIZE * 2f)) / scaleN;


            Camera.main.orthographicSize = erg;

            guiController.RefreshPositions();

            /*
            float ergDoubled = erg * 2;
            float newHeight = ergDoubled / displayCanvas.localScale.y;
            float newWidth = ((ergDoubled * Screen.width )/ Screen.height) / displayCanvas.localScale.x;

            Debug.Log(Screen.width + " / " + Screen.height);


            Vector2 v = new Vector2(newWidth, newHeight);
            displayCanvas.sizeDelta = v;
            //Debug.Log(cs.sizeDelta.x + "/" + cs.sizeDelta.y + "- scale:" + scale + "t: " + Camera.main.pixelRect.height + " - " + cs.localScale.y);

            */

        }
    }
	

}
