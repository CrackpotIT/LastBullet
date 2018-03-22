using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : AbstractEnemyController {

    public float offSetY = 0;
    public float damping = 0.5f;
    

    private Vector3 velocity = Vector3.zero;
    private bool moveBack = false;

    public override void Initialize(Vector2 newTargetPosition, float directionX) {
        base.Initialize(newTargetPosition, directionX);

        // manipulate position
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y + offSetY);
        transform.position = newPos;
        // manipulate targetPosition
        Vector2 newTargetPos = new Vector2(targetPosition.x - (directionX * 3), targetPosition.y + offSetY);
        targetPosition = newTargetPos;
    }

        // Update is called once per frame
    void Update() {
        if (moveBack) {
            Vector2 t = new Vector2(transform.position.x - directionX * moveSpeed, transform.position.y);
            transform.position = Vector3.SmoothDamp(transform.position, t, ref velocity, damping, moveSpeed);
        } else {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping, moveSpeed);
            if (Mathf.Abs(velocity.x) < 0.001f) {
                moveBack = true;
            }
        }
    }
}
