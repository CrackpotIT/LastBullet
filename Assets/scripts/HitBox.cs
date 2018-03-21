using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

    public bool invincible = false;
    public float health;
    public EnemyController.BODYPART bodypart;

    private EnemyController parentEnemyController;

	// Use this for initialization
	void Start () {
        parentEnemyController = transform.parent.GetComponent<EnemyController>();
        if (parentEnemyController == null) {
            Debug.LogError("Enemy HitBox: Parent has no EnemyController!");
        }
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Bullet") {
            if (!invincible) {
                Bullet bullet = coll.gameObject.GetComponent<Bullet>();
                health -= bullet.damage;

                if (health <= 0) {
                    //dead
                    parentEnemyController.Killed();
                } else {
                    parentEnemyController.Damaged();
                }
            } else {
                parentEnemyController.Damaged();
            }

            // Destroy bullet
            Destroy(coll.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D coll) {
        if (coll.gameObject.tag == "Bullet") {
            // Destroy bullet
            Destroy(coll.gameObject);
        }
    }

  }
