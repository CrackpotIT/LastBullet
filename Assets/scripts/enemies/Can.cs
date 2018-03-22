using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour {

    public AudioClip hitSound;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private bool destroyed = false;
    private float directionX;

    void Start() {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
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
                //Move();
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
