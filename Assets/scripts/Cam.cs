using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cam : MonoBehaviour {

    const float TEXTURE_SIZE = 25f;
	private float lastHeight = 0;
    private float lastWidth = 0;
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

        if (lastHeight != Screen.height || lastWidth != Screen.width) {
            lastHeight = Screen.height;
            lastWidth = Screen.width;

            float modifier = TEXTURE_SIZE * 10;
            float scale = Mathf.Max(Mathf.Round(Screen.height / modifier), 2);  // min scale is 2
            Debug.Log("ScaleNew: " + (Screen.height / modifier) + "-" + scale);
            
            float erg = (Screen.height / (TEXTURE_SIZE * 2f)) / scale;


            Camera.main.orthographicSize = erg;

            guiController.RefreshPositions();
        }
    }
	

}
