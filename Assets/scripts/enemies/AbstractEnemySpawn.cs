using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemySpawn: MonoBehaviour {

    public float moveSpeed = 0;

    [HideInInspector]
    public Vector2 targetPosition;
    [HideInInspector]
    public float directionX;

    public virtual void Initialize(Vector2 newTargetPosition, float directionX) {
        this.targetPosition = newTargetPosition;
        this.directionX = directionX;
        transform.localScale = new Vector3(-directionX, transform.localScale.y, transform.localScale.z);
    }
    
}
