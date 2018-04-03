using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAim : AbstractEnemyController {
    
    public bool moveUp;
    public GameObject sparkEmitter;
    public AudioClip hitSound;

    private Rigidbody2D rigidb;


    // Use this for initialization
    void Start () {
        rigidb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float directionY = (moveUp ? 1 : -1);
        Vector2 v = new Vector2(transform.position.x - (directionX * moveSpeed * 0.2f * Time.deltaTime), transform.position.y +(directionY * moveSpeed * Time.deltaTime));
        rigidb.MovePosition(v);
    }


    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Bullet") {
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            if (!bullet.destroyed) {
                // play sound
                SoundManager.PlaySFX(hitSound);
                transform.parent = null;

                Quaternion q = new Quaternion(transform.rotation.x, (directionX == -1 ? transform.rotation.y - 180 : transform.rotation.y), transform.rotation.z, transform.rotation.w);
                Instantiate(sparkEmitter, transform.position, q);
            }
            bullet.destroyed = true;
            // Destroy bullet
            Destroy(coll.gameObject);
            Destroy(gameObject);
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
