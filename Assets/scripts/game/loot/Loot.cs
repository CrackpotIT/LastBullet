using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Loot : MonoBehaviour {

    public enum LOOT_TYPE { AMMUNITION, XP, HEALTH, COIN, UPGRADE };
    public enum LOOT_AMOUNT { LOW, MEDIUM, HIGH };

    public int defaultMinAmount;
    public int defaultMaxAmount;

    public AudioClip takeSound;
    public Sprite collectSprite;

    [Tooltip("Horizontal speed, in units/sec")]
    public float speed = 10;

    [HideInInspector]
    public bool collected = false;
    [HideInInspector]
    public LOOT_TYPE lootType;
    [HideInInspector]
    public int amount;

    Vector3 targetPos;
    Animator anim;

    public void Start() {
        anim = GetComponent<Animator>();

        GuiController gui = GuiController.GetInstance();
        if (lootType == LOOT_TYPE.XP || lootType == LOOT_TYPE.HEALTH) {
            targetPos = gui.backgroundLeft.transform.position;
        } else if (lootType == LOOT_TYPE.AMMUNITION) {
            targetPos = gui.backgroundRight.transform.position;
        } else {
            targetPos = gui.backgroundCenter.transform.position;
        }  
    }

    public void InitAmount(LOOT_AMOUNT lootAmount) {        
        int randomDefaultAmount = Random.Range(defaultMinAmount, (defaultMaxAmount + 1));

        if (lootAmount == LOOT_AMOUNT.LOW) {
            amount = randomDefaultAmount;
        }
        if (lootAmount == LOOT_AMOUNT.MEDIUM) {
            amount = randomDefaultAmount * 2;
        }
        if (lootAmount == LOOT_AMOUNT.HIGH) {
            amount = randomDefaultAmount * 4;
        }
    }

    public void Collect() {
        collected = true;
        // play sound
        SoundManager.PlaySFX(takeSound);
        if (anim) {
            anim.enabled = false;
        }
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = collectSprite;
    }

    // Update is called once per frame
    void Update () {
		if (collected) {
            // Compute the next position, with arc added in
            float x1 = targetPos.x;
            float y1 = targetPos.y;
            float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
            float nextY = Mathf.MoveTowards(transform.position.y, y1, speed * Time.deltaTime);
            Vector3 nextPos = new Vector3(nextX, nextY, transform.position.z);
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
