using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : AbstractEnemyDestructable {

    public GameObject sparkEmitter;
    public AudioClip hitSound;
    private Animator anim;
    private bool destroyed = false;

    void Start() {
        anim = GetComponent<Animator>();
        directionX = transform.parent.transform.localScale.x;
    }

    void Update() {
        if (destroyed) {
            transform.Translate(new Vector3(directionX  * 3,1,0) * 6 * Time.deltaTime);
        }
    }
    

    public override void DestroyEvent() {
        // play sound
        SoundManager.PlaySFX(hitSound);
        // destroy can
        anim.SetBool("ROTATE", true);
        transform.parent = null;
        destroyed = true;
        // instanciate sparkEmitter
        Quaternion q = new Quaternion(transform.rotation.x, (directionX == -1 ? transform.rotation.y - 180 : transform.rotation.y), transform.rotation.z, transform.rotation.w);
        Instantiate(sparkEmitter, transform.position, q);
    }

    public override void DamageEvent() {
        // play sound
        SoundManager.PlaySFX(hitSound);
        // instanciate sparkEmitter
        Quaternion q = new Quaternion(transform.rotation.x, (directionX == -1 ? transform.rotation.y - 180 : transform.rotation.y), transform.rotation.z, transform.rotation.w);
        Instantiate(sparkEmitter, transform.position, q);
    }
}
