using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public enum BULLET_TYPE { Point32, MM9, Point45 };

    public BULLET_TYPE bulletType;
    public float damage;
    public bool destroyed = false;

    private float speed;   
    private int directionX;
    private Rigidbody2D rigidb;

    private void Start() {
       rigidb = GetComponent<Rigidbody2D>();
    }

    public void InitBullet(float speed, float gunDamage, int directionX) {
        this.speed = speed;
        this.damage += gunDamage;
        this.directionX = directionX;
        transform.localScale = new Vector2(directionX, transform.localScale.y);
    }
	
	void Update () {
        Vector2 v = new Vector2(transform.position.x + (directionX * speed * Time.deltaTime), transform.position.y);
        rigidb.MovePosition(v);
        //transform.Translate(vector * speed * Time.deltaTime);
	}
}
