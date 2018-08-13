using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadGameController : MonoBehaviour {



    public GameObject inputCanvasDefault;
    public GameObject inputCanvasReloadGame;

    public float maxTime;
    
    public Sprite[] spriteArray;

    public GameObject buttonTop;
    public GameObject buttonLeft;
    public GameObject buttonRight;

    public AudioClip successSound;
    public AudioClip failureSound;

    private enum BUTTONS {TOP, LEFT, RIGHT};

    private BUTTONS[] buttonArray = { BUTTONS.TOP, BUTTONS.LEFT, BUTTONS.RIGHT};
    private BUTTONS tempGO;

    private int clickCount = 0;
    private bool fault = false;
    private bool started = false;
    private float startTime = 0;

    private ReloadGameState stateMachine;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        bool ended = false;
        bool won = false;
        if (started) {
            // check Timeout
            if (Time.time - startTime > maxTime) {
                Debug.Log("END RELAOD GAME: TIMEOUT!!!");
                SoundManager.PlaySFX(failureSound);
                ended = true;
            } else {
                if (fault) {
                    Debug.Log("END RELAOD GAME: FAULT!!!");
                    SoundManager.PlaySFX(failureSound);
                    ended = true;
                }
                if (!fault && clickCount == 3) {
                    Debug.Log("END RELAOD GAME: WIN!!! " + (Time.time - startTime));
                    SoundManager.PlaySFX(successSound);
                    ended = true;
                    won = true;
                }
            }
        }

        if (ended) {
            inputCanvasDefault.SetActive(true);
            inputCanvasReloadGame.SetActive(false);
            started = false;

            stateMachine.GameOver(won);
        }
    }

    public void StartReloadGame(ReloadGameState stateMachine) {
        this.stateMachine = stateMachine;

        inputCanvasDefault.SetActive(false);
        inputCanvasReloadGame.SetActive(true);
        //
        clickCount = 0;
        fault = false;
        //Shuffle Random Button Order
        ShuffleButtons();
        // Init Buttons
        InitButtons();        
        // Start Timer
        startTime = Time.time;
        started = true;
    }

    private void ShuffleButtons() {
        for (int i = 0; i < buttonArray.Length; i++) {
            int rnd = Random.Range(0, buttonArray.Length);
            tempGO = buttonArray[rnd];
            buttonArray[rnd] = buttonArray[i];
            buttonArray[i] = tempGO;
        }
    }

    private void InitButtons() {
        buttonTop.SetActive(true);
        buttonLeft.SetActive(true);
        buttonRight.SetActive(true);
        Image imageTop = buttonTop.GetComponent<Image>();
        Image imageLeft = buttonLeft.GetComponent<Image>();
        Image imageRight = buttonRight.GetComponent<Image>();
        for (int i = 0; i < buttonArray.Length; i++) {
            if (buttonArray[i] == BUTTONS.TOP) {
                imageTop.sprite =  spriteArray[i];
            }
            if (buttonArray[i] == BUTTONS.LEFT) {
                imageLeft.sprite = spriteArray[i];
            }
            if (buttonArray[i] == BUTTONS.RIGHT) {
                imageRight.sprite = spriteArray[i];
            }
        }
    }

    public void PressTop() {
        Debug.Log("Top Pressed!");
        if (clickCount < buttonArray.Length && buttonArray[clickCount] == BUTTONS.TOP) {
            Debug.Log("Correct");
            buttonTop.SetActive(false);
            clickCount++;
        } else {
            fault = true;
        }
        
    }

    public void PressLeft() {
        Debug.Log("Left Pressed!");
        if (clickCount < buttonArray.Length && buttonArray[clickCount] == BUTTONS.LEFT) {
            Debug.Log("Correct");
            buttonLeft.SetActive(false);
            clickCount++;
        } else {
            fault = true;
        }
    }

    public void PressRight() {
        Debug.Log("Right Pressed!");
        if (clickCount < buttonArray.Length && buttonArray[clickCount] == BUTTONS.RIGHT) {
            Debug.Log("Correct");
            buttonRight.SetActive(false);
            clickCount++;
        } else {
            fault = true;
        }
    }

    
}
