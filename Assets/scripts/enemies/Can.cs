﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour {

    public GameObject sparkEmitter;
    public AudioClip hitSound;
    private Animator anim;
    private bool destroyed = false;
    private float directionX;

    void Start() {
        anim = GetComponent<Animator>();
        directionX = transform.parent.transform.localScale.x;
    }

    void Update() {
        if (destroyed) {
            transform.Translate(new Vector3(directionX  * 3,1,0) * 6 * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Bullet") {
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            if (!bullet.destroyed) {
                // play sound
                SoundManager.PlaySFX(hitSound);
                // destroy can
                anim.SetBool("ROTATE", true);
                transform.parent = null;
                //boxCollider.enabled = false;
                destroyed = true;

                Quaternion q = new Quaternion(transform.rotation.x, (directionX == -1 ? transform.rotation.y - 180 : transform.rotation.y), transform.rotation.z, transform.rotation.w);
                Instantiate(sparkEmitter, transform.position, q);
            }
            bullet.destroyed = true;
            // Destroy bullet
            Destroy(coll.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D coll) {
        if (coll.gameObject.tag == "Bullet") {
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            bullet.destroyed = true;
            // Destroy bullet
            Destroy(coll.gameObject);
        }
    }
}
