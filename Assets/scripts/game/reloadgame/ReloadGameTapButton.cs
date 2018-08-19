using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadGameTapButton : MonoBehaviour {

    public enum ALIGN { CENTER, LEFT, RIGHT};

    public ALIGN alignment;
    public float updateInterval = 0.1F;
    public float offsetX = 1F;
    public float offsetY = .75F;


    private SpriteRenderer spriteRenderer;
    private Animator clipAnimator;


    // Use this for initialization
    void Awake () {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        clipAnimator = transform.GetComponent<Animator>();
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
        if (alignment == ALIGN.LEFT) {
            vector.x += halveWidth + offsetX;
        }
        if (alignment == ALIGN.RIGHT) {
            vector.x -= halveWidth + offsetX;
        }
        vector.y -= offsetY;
        transform.position = vector;
    }
    

    public void Click() {
        clipAnimator.speed = 1;
    }

    public void ReachedNewPosition() {
        clipAnimator.speed = 0;
    }

}
