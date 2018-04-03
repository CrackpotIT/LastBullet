using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    public AudioClip shellPlingSound;


    public void ShellHitGround() {
        SoundManager.PlaySFX(shellPlingSound);
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }
}
