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
    

    public override void DestroyEvent() {
        // play sound
        SoundManager.PlaySFX(hitSound);
        transform.parent = null;

        Quaternion q = new Quaternion(transform.rotation.x, (directionX == -1 ? transform.rotation.y - 180 : transform.rotation.y), transform.rotation.z, transform.rotation.w);
        Instantiate(sparkEmitter, transform.position, q);

        // shake Camera
        CameraShake.GetInstance().StartShake(.1f, .2f);
        Destroy(gameObject);
    }

    public override void DamageEvent() {
        Debug.Log("Damage Moving Aim TODO");
    }
}
