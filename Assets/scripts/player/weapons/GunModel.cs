using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModel : AbstractModel {

    public Transform muzzlePosition;
    
    public float shellBounceOffsetX;

    public GunStruct gunStruct;

    public int currentClip;

    private GameObject[] shellBounceArray;
        
    private float lastShot = 0;
    private bool reloading = false;


    public override void Start() {
        base.Start();
        currentClip = gunStruct.clipSize;
        GuiController.GetInstance().RefreshBulletCount(currentClip, Inventory.GetInstance().bullets[gunStruct.bullet.bulletType]);

        shellBounceArray = new GameObject[3];
        shellBounceArray[0] = (GameObject)Resources.Load("player/weapons/ShellBounceUp1");
        shellBounceArray[1] = (GameObject)Resources.Load("player/weapons/ShellBounceUp2");
        shellBounceArray[2] = (GameObject)Resources.Load("player/weapons/ShellBounceUp3");        
    }

    public override void SetAnimatorBool(ANIM_PARAMS_GUNS parameter, bool value) {
        base.SetAnimatorBool(parameter, value);
        if (parameter == ANIM_PARAMS_GUNS.FIRE_RELEASE) {
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

    public void Reload() {
        // check if ammunition left or inventory empty
        if (!reloading && Inventory.GetInstance().bullets[gunStruct.bullet.bulletType] > 0) {
            reloading = true;
            //Invoke("ReloadFinished", gunStruct.timeToReload);
        }
    }
    public bool IsReloading() {
        return reloading;
    }

    public void ReloadFinished() {
        // get bullets from inventory
        int bulletsInInventory = Inventory.GetInstance().bullets[gunStruct.bullet.bulletType];
        int remainingBullet = 0;
        if (currentClip > 0) {
            //Reload with bullet in chamber
            remainingBullet = currentClip;
        }
        if (bulletsInInventory >= gunStruct.clipSize) {
            currentClip = gunStruct.clipSize; // Full Clip reload
            Inventory.GetInstance().bullets[gunStruct.bullet.bulletType] = bulletsInInventory - (currentClip - remainingBullet);            
        } else {
            currentClip += bulletsInInventory; // not enough bullets for full clip
            Inventory.GetInstance().bullets[gunStruct.bullet.bulletType] = 0;
        }

        GuiController.GetInstance().RefreshBulletCount(currentClip, Inventory.GetInstance().bullets[gunStruct.bullet.bulletType]);
        reloading = false;
        ShowClipEmptyLayer(false);
    }

    public bool FireGun(int directionX, bool topPosition) {
        if (PullTriggerReady()) {
            lastShot = Time.time;

            if (HasAmmunition()) {
                SoundManager.PlaySFX(gunStruct.gunShotSound);
                Bullet newBullet = Instantiate(gunStruct.bullet, muzzlePosition.position, muzzlePosition.rotation);
                newBullet.InitBullet(gunStruct.bulletSpeed, gunStruct.damageModifier, directionX);
                newBullet.transform.parent = EffectParent.GetInstance().transform;
                

                // Create empty shell
                ThrowShell(directionX, topPosition);

                // remove bullet from clip
                currentClip--;
                // remove bullet from inventory
                //Inventory.instance.bullets[gunStruct.bullet.bulletType]--;

                GuiController.GetInstance().RefreshBulletCount(currentClip, Inventory.GetInstance().bullets[gunStruct.bullet.bulletType]);
                return true;
            } else {
                SoundManager.PlaySFX(gunStruct.gunEmptySound);
                return false;
            }
        }
        return false;
    }


    private void ThrowShell(int directionX, bool topPosition) {
        int random = Random.Range(0, this.shellBounceArray.Length);
        GameObject shellBounceInstance = Instantiate(shellBounceArray[random], transform.position, transform.rotation);
        Vector3 shellScale = shellBounceInstance.transform.localScale;
        shellBounceInstance.transform.localScale = new Vector3(directionX * shellScale.x, shellScale.y, shellScale.z);
        shellBounceInstance.transform.parent = EffectParent.GetInstance().transform;
    }
}
