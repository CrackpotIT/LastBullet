using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyController : MonoBehaviour {

    public float moveSpeed = 0;
    public float health = 1;
    public int xp = 1;

    [HideInInspector]
    public Vector2 targetPosition;
    [HideInInspector]
    public float directionX;

    public virtual void Initialize(Vector2 newTargetPosition, float directionX) {
        this.targetPosition = newTargetPosition;        
        this.directionX = directionX;
        transform.localScale = new Vector2(-directionX, transform.localScale.y);
    }

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
