using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootButtonController : MonoBehaviour {

    public enum VALIGN { CENTER, TOP, BOTTOM };
    public enum ALIGN { CENTER, LEFT, RIGHT };

    public VALIGN alignmentY;
    public ALIGN alignmentX;

    public float offsetX;
    public float unClickTimer = 0.1F;

    private SpriteRenderer spriteRenderer;
    private bool clicked = false;
    private float lastClickTime = 0;

    private Color colorUnclicked;
    private Color colorClicked;

    // Use this for initialization
    void Awake () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colorUnclicked = spriteRenderer.color;
        colorClicked = new Color();
        colorClicked.g = colorUnclicked.g;
        colorClicked.b = colorUnclicked.b;
        colorClicked.r = colorUnclicked.r;

        

        colorClicked.a = 0.0F;
    }

    private void Update() {
        if (clicked && Time.time - lastClickTime > unClickTimer) {
            spriteRenderer.color = colorUnclicked;
        }
    }

    public void OnClick() {
        clicked = true;
        spriteRenderer.color = colorClicked;
        lastClickTime = Time.time;
    }


    public void RefreshPosition() {
        if (alignmentX == ALIGN.CENTER) {
            SetPosition(0.5F, 0.5F);
        }
        if (alignmentX == ALIGN.LEFT) {
            SetPosition(0F, 0.5F);
        }
        if (alignmentX == ALIGN.RIGHT) {
            SetPosition(1F, 0.5F);
        }
    }


    private void SetPosition(float x, float y) {
        float halveWidth = spriteRenderer.sprite.bounds.size.x / 2F;
        // keep z-position
        float z = transform.position.z;
        Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 0));
        vector.z = z;
        if (alignmentX == ALIGN.LEFT) {
            vector.x += halveWidth + offsetX;
        }
        if (alignmentX == ALIGN.RIGHT) {
            vector.x -= halveWidth + offsetX;
        }
        if (alignmentY == VALIGN.TOP) {
            vector.y += 1F;
        }
        if (alignmentY == VALIGN.BOTTOM) {
            vector.y -= 2F;
        }
        transform.position = vector;
    }
}
