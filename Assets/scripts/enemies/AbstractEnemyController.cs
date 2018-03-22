using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyController : MonoBehaviour {

    public float moveSpeed;

    [HideInInspector]
    public Vector2 targetPosition;
    [HideInInspector]
    public float directionX;

    public virtual void Initialize(Vector2 newTargetPosition, float directionX) {
        this.targetPosition = newTargetPosition;        
        this.directionX = directionX;
        transform.localScale = new Vector2(-directionX, transform.localScale.y);
    }
}
