using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyDestructable : MonoBehaviour {
    
    public float health = 1;
    public int xp = 1;

    public abstract void DestroyEvent();
    public abstract void DamageEvent();


    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Bullet") {
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            if (!bullet.destroyed) {
                health -= bullet.damage;
                if (health > 0) {
                    DamageEvent();
                } else {
                    DestroyEvent();
                    Player.GetInstance().AddXp(xp);
                }
            }

            bullet.destroyed = true;
            // Destroy bullet
            Destroy(coll.gameObject);
        }
    }


    void OnCollisionExit2D(Collision2D coll) {
        if (coll.gameObject.tag == "Bullet") {
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            bullet.destroyed = true;
            // Destroy bullet
            Destroy(coll.gameObject);
        }
    }
}
