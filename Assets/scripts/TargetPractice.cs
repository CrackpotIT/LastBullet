using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPractice : MonoBehaviour {

    private Animator anim;
    private BoxCollider2D boxCollider;

    void Start() {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Bullet") {
            Bullet bullet = coll.gameObject.GetComponent<Bullet>();
            if (!bullet.destroyed) {
                // destroy can
                anim.SetBool("ROTATE", true);
                transform.parent = null;
                boxCollider.enabled = false;
                Move();
            }
            bullet.destroyed = true;
            // Destroy bullet
            Destroy(coll.gameObject);
        }
    }

    public void Move() {
        StartCoroutine(MoveOverSpeed(gameObject, new Vector3(10, 10, 0), 10));
    }


    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed) {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end) {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
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
