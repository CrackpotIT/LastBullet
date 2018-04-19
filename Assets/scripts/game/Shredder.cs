using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

    private const float DISTANCE_FROM_GAMEBORDER_X = 2;

    private BoxCollider2D boxCollider;

    private void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D coll) {
        Destroy(coll.gameObject);
	}
	

    public void RefreshPosition() {
        float x = -1;
        float offsetMultiplicator = 0;
        if (transform.position.x > 0) {
            // Right shredder
            x = 1;
            offsetMultiplicator = 1;
        }
        if (transform.position.x < 0) {
            // Left shredder
            x = 0;
            offsetMultiplicator = -1;
        }

        if (x >= 0) {
            float z = transform.position.z;
            Vector3 newPos = Camera.main.ViewportToWorldPoint(new Vector3(x, .5f, 0));
            float offsetX = offsetMultiplicator * ((boxCollider.size.x / 2) + DISTANCE_FROM_GAMEBORDER_X);
            transform.position = new Vector3(newPos.x + offsetX, newPos.y, z);
        }
    }
}
