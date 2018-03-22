using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

    public bool invincible = false;
    public float health;
    public ZombieController.BODYPART bodypart;

    private ZombieController parentEnemyController;

	// Use this for initialization
	void Start () {
        parentEnemyController = transform.parent.GetComponent<ZombieController>();
        if (parentEnemyController == null) {
            Debug.LogError("Enemy HitBox: Parent has no EnemyController!");
        }
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Bullet") {
            if (!invincible) {
                Bullet bullet = coll.gameObject.GetComponent<Bullet>();
                if (!bullet.destroyed) {
                    health -= bullet.damage;

                    if (health <= 0) {
                        //dead
                        parentEnemyController.Killed();
                    } else {
                        parentEnemyController.Damaged();
                    }
                }
                bullet.destroyed = true;

            } else {
                parentEnemyController.Damaged();
            }

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
