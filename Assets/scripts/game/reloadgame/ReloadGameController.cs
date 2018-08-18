using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadGameController : MonoBehaviour {

    public GameObject inputGameObject;
    public float countInterval = 1;
    public float maxClickRatioPerSecond = 5;
    

    private AbstractReloadState currentReloadState;
    private float startTime;
    private float lastTime;
    private int clickCount;
    private ReloadGameTapButton[] children;

    // Use this for initialization
    void Awake () {
        
        children = transform.GetComponentsInChildren<ReloadGameTapButton>();
        Debug.Log("Children: " + children.Length);
	}



    // Update is called once per frame
    void Update() {
        if ((Time.time - lastTime) > countInterval) {
            float currentTimer = Time.time - startTime;
            float clickRatioPerSecond = (clickCount / currentTimer);

            float modifierReloadSpeed = 1 - Mathf.Clamp(((clickRatioPerSecond / maxClickRatioPerSecond) * 0.5F), 0, 0.8F);
            
            Debug.Log("Click Rate:" + modifierReloadSpeed);
            if (currentReloadState != null) {
                currentReloadState.SetReloadSpeed(modifierReloadSpeed);
            }

            lastTime = Time.time;
        }
    }

    public void StartReloadGame(AbstractReloadState reloadState) {
        Debug.Log("Start Reload game:");
        currentReloadState = reloadState;

        inputGameObject.SetActive(false);
        gameObject.SetActive(true);
        clickCount = 0;
        startTime = Time.time;
        lastTime = startTime;        

        foreach (ReloadGameTapButton button in children) {
            button.StartGame();
            button.RefreshPosition();
        }
    }

    public void EndReloadGame() {
        foreach (ReloadGameTapButton button in children) {
            button.EndGame();
        }

        inputGameObject.SetActive(true);
        gameObject.SetActive(false);


    }

    public void Click() {
        clickCount++;
    }
}
