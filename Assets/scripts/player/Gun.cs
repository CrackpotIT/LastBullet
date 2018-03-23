﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float timeBetweenShots;
    public float timeToReload;
    public float damageModifier;
    public float bulletSpeed;    
    public Bullet bullet;
    public int clipSize;
    public AudioClip gunShotSound;
    public AudioClip gunEmptySound;
    public AudioClip gunReloadSound;

    private int currentClip;
    private float lastShot = 0;
    private bool reloading = false;
    
    private UiController uiController;

    // nur providorisch! Muss später in StateMachine
    private GunModel gunModel;

    private void Start() {
        currentClip = clipSize;
        uiController = GameObject.FindObjectOfType<UiController>();
        uiController.RefreshBulletCount(currentClip, PlayerInventory.instance.bullets[bullet.bulletType]);
    }

    public bool HasAmmunition() {
        return currentClip > 0;
    }

    public void Reload(GunModel gunModel) {
        this.gunModel = gunModel;
        // check if ammunition left or inventory empty
        if (!reloading && PlayerInventory.instance.bullets[bullet.bulletType] > 0) {
            reloading = true;
            SoundManager.PlaySFX(gunReloadSound);
            Invoke("ReloadFinished", timeToReload);
        }

    }

    public bool IsReady() {
        if (!reloading && (lastShot == 0 || Time.time > (lastShot + timeBetweenShots))) {
            return true;
        } else {
            return false;
        }
    }

	public bool FireGun(int directionX) {
        if (IsReady()) {
            lastShot = Time.time;

            if (HasAmmunition()) {
                SoundManager.PlaySFX(gunShotSound);

                Bullet newBullet = Instantiate(bullet, transform.position, transform.rotation);
                newBullet.InitBullet(bulletSpeed, damageModifier, directionX);

                // remove bullet from clip
                currentClip--;
                // remove bullet from inventory
                PlayerInventory.instance.bullets[bullet.bulletType]--;

                uiController.RefreshBulletCount(currentClip, PlayerInventory.instance.bullets[bullet.bulletType]);
                return true;
            } else {
                SoundManager.PlaySFX(gunEmptySound);
                return false;
            }
        }
        return false;
    }

    private void ReloadFinished() {
        // get bullets from inventory
        int bulletsInInventory = PlayerInventory.instance.bullets[bullet.bulletType];

        if (bulletsInInventory > clipSize) {
            currentClip = clipSize; // Full Clip reload
        } else {
            currentClip = bulletsInInventory; // not enough bullets for full clip
        }
        
        uiController.RefreshBulletCount(currentClip, bulletsInInventory);
        reloading = false;
        gunModel.ShowClipEmptyLayer(false);
    }
}
