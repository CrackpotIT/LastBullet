﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : AbstractEnemyDestructable {

    public GameObject impactEffect;
    public GameObject explosionEffect;
    public AudioClip impactSound;
    public AudioClip explosionSound;

    public GameObject spawnObject;
    

    public override void DamageEvent() {
        // play sound
        SoundManager.PlaySFX(impactSound);
        // instanciate sparkEmitter
        
        GameObject go = Instantiate(impactEffect, transform.position, transform.rotation);
        go.transform.localScale = new Vector3(transform.parent.transform.localScale.x, transform.parent.transform.localScale.y, transform.parent.transform.localScale.z);
    }

    public override void DestroyEvent() {
        SoundManager.PlaySFX(explosionSound);
        GameObject go = Instantiate(explosionEffect, transform.position, transform.rotation);
        go.transform.localScale = new Vector3(transform.parent.transform.localScale.x, transform.parent.transform.localScale.y, transform.parent.transform.localScale.z);
        Instantiate(spawnObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
