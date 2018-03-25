using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModel : AbstractModel {

    public Transform muzzlePositionTop;
    public Transform muzzlePositionBottom;

    public GunStruct gunStruct;

    private int currentClip;
    private float lastShot = 0;
    private bool reloading = false;
    private GuiController uiController;


    public override void Start() {
        base.Start();
        currentClip = gunStruct.clipSize;
        uiController = GameObject.FindObjectOfType<GuiController>();
        uiController.RefreshBulletCount(currentClip, Inventory.instance.bullets[gunStruct.bullet.bulletType]);
    }

    public override void SetAnimatorBool(ANIM_PARAMS parameter, bool value) {
        base.SetAnimatorBool(parameter, value);
        if (parameter == ANIM_PARAMS.FIRE_TOP_RELEASE || parameter == ANIM_PARAMS.FIRE_BOTTOM_RELEASE) {
            if (!HasAmmunition()) {
                ShowClipEmptyLayer(true);
            }
        }
    }
    public void ShowClipEmptyLayer(bool show) {
        if (show) {
            animator.SetLayerWeight(1, 1);
        } else {
            animator.SetLayerWeight(1, 0);
        }
    }

    public bool HasAmmunition() {
        return currentClip > 0;
    }

    public bool PullTriggerReady() {
        if (!reloading && (lastShot == 0 || Time.time > (lastShot + gunStruct.timeBetweenShots))) {
            return true;
        } else {
            return false;
        }
    }

    public void Reload(GunModel gunModel) {
        // check if ammunition left or inventory empty
        if (!reloading && Inventory.instance.bullets[gunStruct.bullet.bulletType] > 0) {
            reloading = true;
            SoundManager.PlaySFX(gunStruct.gunReloadSound);
            Invoke("ReloadFinished", gunStruct.timeToReload);
        }
    }
    private void ReloadFinished() {
        // get bullets from inventory
        int bulletsInInventory = Inventory.instance.bullets[gunStruct.bullet.bulletType];
        if (currentClip > 0) {
            //Reload with bullet in chamber
            currentClip = 1;
        }
        if (bulletsInInventory > gunStruct.clipSize) {
            currentClip += gunStruct.clipSize; // Full Clip reload
        } else {
            currentClip += bulletsInInventory; // not enough bullets for full clip
        }

        uiController.RefreshBulletCount(currentClip, bulletsInInventory);
        reloading = false;
        ShowClipEmptyLayer(false);
    }

    public bool FireGun(int directionX, bool topPosition) {
        if (PullTriggerReady()) {
            lastShot = Time.time;

            if (HasAmmunition()) {
                SoundManager.PlaySFX(gunStruct.gunShotSound);
                Transform transformToUser = (topPosition ? muzzlePositionTop : muzzlePositionBottom);
                Bullet newBullet = Instantiate(gunStruct.bullet, transformToUser.position, transformToUser.rotation);
                newBullet.InitBullet(gunStruct.bulletSpeed, gunStruct.damageModifier, directionX);

                // remove bullet from clip
                currentClip--;
                // remove bullet from inventory
                Inventory.instance.bullets[gunStruct.bullet.bulletType]--;

                uiController.RefreshBulletCount(currentClip, Inventory.instance.bullets[gunStruct.bullet.bulletType]);
                return true;
            } else {
                SoundManager.PlaySFX(gunStruct.gunEmptySound);
                return false;
            }
        }
        return false;
    }
}
