using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    public AudioClip shellPlingSound;


    public void ShellHitGround() {
        SoundManager.PlaySFX(shellPlingSound);
    }

    public void AnimationEnd() {
        //Destroy(gameObject);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Coordinates coordinates = GameObject.FindObjectOfType<Coordinates>();
        if (transform.position == coordinates.top.transform.position) {
            sr.sortingOrder = 0;
        }
    }
}
