using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadGameTapButton : MonoBehaviour {

    public enum ALIGN { CENTER, LEFT, RIGHT};

    public ALIGN alignment;
    public float updateInterval = 0.1F;


    private SpriteRenderer spriteRenderer;

    private Animator clipAnimator;
    private ReloadGameController gameController;


    // Use this for initialization
    void Awake () {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();


        clipAnimator = transform.GetComponent<Animator>();

        gameController = transform.parent.GetComponent<ReloadGameController>();
        if (!gameController) {
            Debug.LogError("No ReloadGameController in parent found!");
        }

    }

    public void StartGame() {
        RefreshPosition();
        clipAnimator.SetTrigger("START");
        clipAnimator.speed = 1;
    }

    public void EndGame() {
        clipAnimator.SetTrigger("END");
        clipAnimator.speed = 1;
    }

    public void RefreshPosition() {
        if (alignment == ALIGN.CENTER) {
            SetPosition(0.5F, 0.5F);
        }
        if (alignment == ALIGN.LEFT) {
            SetPosition(0F, 0.5F);
        }
        if (alignment == ALIGN.RIGHT) {
            SetPosition(1F, 0.5F);
        }
    }

    private void SetPosition(float x, float y) {
        Debug.Log("Refresh Pos");
        float halveWidth = spriteRenderer.sprite.bounds.size.x / 2F;
        // keep z-position
        float z = transform.position.z;
        Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 0));
        vector.z = z;
        if (x == 0F) {
            vector.x += halveWidth;
        }
        if (x == 1F) {
            vector.x -= halveWidth;
        }
        transform.position = vector;
    }
    

    void OnMouseDown() {
        if (gameController) {
            gameController.Click();
        }
        clipAnimator.speed = 1;
    }

    public void ReachedNewPosition() {
        clipAnimator.speed = 0;
    }

}
