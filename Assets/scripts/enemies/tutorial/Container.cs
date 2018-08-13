using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : AbstractEnemyDestructable {

    public Sprite[] damageSprites;

    public GameObject impactEffect;
    public GameObject explosionEffect;
    public AudioClip impactSound;
    public AudioClip explosionSound;
    public float chanceLoot;

    public Loot.LOOT_AMOUNT lootAmount;
    
    private GameObject cart;
    private SpriteRenderer spriteRenderer;
    private int damage;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        cart = transform.parent.transform.parent.gameObject;
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
        go.transform.localScale = new Vector3(cart.transform.localScale.x, cart.transform.localScale.y, cart.transform.localScale.z);
        go.transform.parent = EffectParent.GetInstance().transform;
    }

    public override void DestroyEvent() {
        SoundManager.PlaySFX(explosionSound);
        GameObject explosionGO = Instantiate(explosionEffect, transform.position, transform.rotation);
        if (cart != null) {
            explosionGO.transform.localScale = new Vector3(cart.transform.localScale.x, cart.transform.localScale.y, cart.transform.localScale.z);
        }
        explosionGO.transform.parent = EffectParent.GetInstance().transform;
        
        // shake Camera
        CameraShake.GetInstance().StartShake(.1f, .2f);

        if (Random.value < chanceLoot) {
            GameObject packageGO = Instantiate(GameController.GetInstance().spawnObject, transform.position, transform.rotation);
            Package lootPackage = packageGO.GetComponent<Package>();
            lootPackage.lootAmount = lootAmount;
        }
        
        Destroy(gameObject);
    }
}
