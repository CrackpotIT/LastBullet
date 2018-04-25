using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAimSpawn : AbstractEnemySpawn {

    public bool moveUp;

    private Rigidbody2D rigidb;

    // Use this for initialization
    void Start() {
        rigidb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        float directionY = (moveUp ? 1 : -1);
        Vector2 v = new Vector2(transform.position.x + (directionX * moveSpeed * 0.2f * Time.deltaTime), transform.position.y + (directionY * moveSpeed * Time.deltaTime));
        rigidb.MovePosition(v);
    }
}
