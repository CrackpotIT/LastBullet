using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GunStruct {

    public float timeBetweenShots;
    public float timeToReload;
    public float damageModifier;
    public float bulletSpeed;
    public Bullet bullet;
    public int clipSize;
    public AudioClip gunShotSound;
    public AudioClip gunEmptySound;
    public AudioClip gunReloadSound;
}
