using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadGameController : MonoBehaviour {
    
    public float countInterval = 1;
    public float maxClickRatioPerSecond = 5;
    public ReloadGameTapButton buttonLeft;
    public ReloadGameTapButton buttonRight;

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

        InputCanvasController.GetInstance().SwitchReloadGameMode();
        clickCount = 0;
        startTime = Time.time;
        lastTime = startTime;

        buttonLeft.StartGame();
        buttonRight.StartGame();

    }

    public void EndReloadGame() {
        buttonLeft.EndGame();
        buttonRight.EndGame();

        InputCanvasController.GetInstance().SwitchNormalMode();
    }

    public void EventReloadClick() {
        clickCount++;
    }
}
