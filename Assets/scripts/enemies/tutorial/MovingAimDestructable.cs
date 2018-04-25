using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAimDestructable : AbstractEnemyDestructable {
    
    public GameObject sparkEmitter;
    public AudioClip hitSound;
    
    private float directionX;

    // Use this for initialization
    void Start () {
        directionX = transform.localScale.x;
    }
	
    

    public override void DestroyEvent() {
        // play sound
        SoundManager.PlaySFX(hitSound);
        transform.parent = null;

        Quaternion q = new Quaternion(transform.rotation.x, (directionX == -1 ? transform.rotation.y - 180 : transform.rotation.y), transform.rotation.z, transform.rotation.w);
        Instantiate(sparkEmitter, transform.position, q);

        // shake Camera
        CameraShake.GetInstance().StartShake(.1f, .2f);
        Destroy(gameObject);
    }

    public override void DamageEvent() {
        Debug.Log("Damage Moving Aim TODO");
    }
}
