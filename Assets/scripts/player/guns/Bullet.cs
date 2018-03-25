using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public enum BULLET_TYPE { MM9, MM10, Point40, Point45 };

    public BULLET_TYPE bulletType;
    public float damage;
    public bool destroyed = false;

    private float speed;       
    private Vector3 vector;

    public void InitBullet(float speed, float gunDamage, int directionX) {
        this.speed = speed;
        this.damage += gunDamage;

        if (directionX > 0) {
            this.vector = Vector3.right;            
        }
        if (directionX < 0) {
            this.vector = Vector3.left;
        }
        transform.localScale = new Vector2(directionX, transform.localScale.y);
    }
	
	void Update () {
        transform.Translate(vector * speed * Time.deltaTime);
	}
}
