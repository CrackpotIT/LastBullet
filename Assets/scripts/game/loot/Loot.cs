using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Loot : MonoBehaviour {

    public enum LOOT_TYPE { AMMUNITION, XP, HEALTH, COIN, UPGRADE };
    public LOOT_TYPE lootType;
    public AudioClip takeSound;

    [HideInInspector]
    public bool collected = false;

    [Tooltip("Horizontal speed, in units/sec")]
    public float speed = 10;

    Vector3 startPos;
    Vector3 targetPos;

    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        startPos = transform.position;

        GuiController gui = GuiController.GetInstance();
        if (lootType == LOOT_TYPE.XP || lootType == LOOT_TYPE.HEALTH) {
            targetPos = gui.backgroundLeft.transform.position;
        } else if (lootType == LOOT_TYPE.AMMUNITION) {
            targetPos = gui.backgroundRight.transform.position;
        } else {
            targetPos = gui.backgroundCenter.transform.position;
        }        
    }

    public void Collect() {
        collected = true;
        // play sound
        SoundManager.PlaySFX(takeSound);
        if (anim) {
            anim.enabled = false;
        }
    }

    // Update is called once per frame
    void Update () {
		if (collected) {
            // Compute the next position, with arc added in
            float x0 = startPos.x;
            float x1 = targetPos.x;
            float y1 = targetPos.y;
            float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
            float nextY = Mathf.MoveTowards(transform.position.y, y1, speed * Time.deltaTime);
            Vector3 nextPos = new Vector3(nextX, nextY, transform.position.z);
            Debug.Log("Next Pos: " + nextPos.x + " / " + nextPos.y + " / " + nextPos.z);
            transform.position = nextPos;

            // Do something when we reach the target
            if (nextPos == targetPos) Arrived();
        }
	}

    void Arrived() {

        TakeIt();
        Destroy(gameObject);
    }

    public abstract void TakeIt();

    public abstract string GetText();
}
