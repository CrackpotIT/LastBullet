using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cam : MonoBehaviour {

    const float TEXTURE_SIZE = 25f;
	private float lastHeight = 0;

    public RectTransform displayCanvas;

    // Use this for initialization
    void Start () {

        UpdateOrthographicSize();

	}



	
	// Update is called once per frame
	void Update () {
        UpdateOrthographicSize();

    }


    private void UpdateOrthographicSize() {

        if (lastHeight != Screen.height) {
            lastHeight = Screen.height;
            
            float scale = 5f;
            if (Screen.height < 1080) {
                scale = 4f;
            }
            if (Screen.height < 768) {
                scale = 3f;
            }
            if (Screen.height < 512) {
                scale = 2f;
            }
            
            float erg = (Screen.height / (TEXTURE_SIZE * 2f)) / scale;


            Camera.main.orthographicSize = erg;

            float ergDoubled = erg * 2;
            float newHeight = ergDoubled / displayCanvas.localScale.y;
            float newWidth = ((ergDoubled * Screen.width )/ Screen.height) / displayCanvas.localScale.x;

            Debug.Log(Screen.width + " / " + Screen.height);


            Vector2 v = new Vector2(newWidth, newHeight);
            displayCanvas.sizeDelta = v;
            //Debug.Log(cs.sizeDelta.x + "/" + cs.sizeDelta.y + "- scale:" + scale + "t: " + Camera.main.pixelRect.height + " - " + cs.localScale.y);

            
        }
    }
	

}
