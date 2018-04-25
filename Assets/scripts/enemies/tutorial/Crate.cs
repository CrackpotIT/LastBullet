using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : AbstractEnemyDestructable {

    public Sprite[] damageSprites;

    public GameObject impactEffect;
    public GameObject explosionEffect;
    public AudioClip impactSound;
    public AudioClip explosionSound;

    public GameObject spawnObject;

    private SpriteRenderer spriteRenderer;
    private int damage;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public override void DamageEvent() {
        // change sprite
        if (damageSprites.Length > damage) {
            spriteRenderer.sprite = damageSprites[damage];
            damage++;
        }
        // play sound
        SoundManager.PlaySFX(impactSound);
        // instanciate sparkEmitter
        
        GameObject go = Instantiate(impactEffect, transform.position, transform.rotation);
        go.transform.localScale = new Vector3(transform.parent.transform.localScale.x, transform.parent.transform.localScale.y, transform.parent.transform.localScale.z);
        go.transform.parent = EffectParent.GetInstance().transform;
    }

    public override void DestroyEvent() {
        SoundManager.PlaySFX(explosionSound);
        GameObject go = Instantiate(explosionEffect, transform.position, transform.rotation);
        go.transform.localScale = new Vector3(transform.parent.transform.localScale.x, transform.parent.transform.localScale.y, transform.parent.transform.localScale.z);
        go.transform.parent = EffectParent.GetInstance().transform;

        Instantiate(spawnObject, transform.position, transform.rotation);
        
        // shake Camera
        CameraShake.GetInstance().StartShake(.1f, .2f);

        Destroy(gameObject);
    }
}
