using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teddy : AbstractEnemyDestructable {

    public GameObject destroyEffect;
    public GameObject textEffect;
    public AudioClip hitSound;

    private GameObject cart;

    void Start() {
        cart = transform.parent.transform.parent.gameObject;
    }
        

    public override void DestroyEvent() {
        // play sound
        SoundManager.PlaySFXDelay(hitSound, 0.15F);

        // instanciate DestroyEffect
        CreateDestroyEffect();

        Destroy(gameObject);
    }

    public override void DamageEvent() {
        // play sound
        SoundManager.PlaySFX(hitSound);
        // instanciate DestroyEffect
        CreateDestroyEffect();
    }

    


    private void CreateDestroyEffect() {
        GameObject explosionGO = Instantiate(destroyEffect, transform.position, transform.rotation);
        if (cart != null) {
            explosionGO.transform.localScale = new Vector3(cart.transform.localScale.x, cart.transform.localScale.y, cart.transform.localScale.z);
        }
        explosionGO.transform.parent = EffectParent.GetInstance().transform;

        GameObject textGO = Instantiate(textEffect, transform.position, transform.rotation);
        textGO.transform.parent = EffectParent.GetInstance().transform;
        TextMesh textMesh = textGO.GetComponentInChildren<TextMesh>();
        textMesh.text = "FOOBEAR";
    }
}
