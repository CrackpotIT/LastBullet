using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : AbstractEnemyController {
    public enum BODYPART { HEAD, BODY };

    public float damageTime = 0;

    private float lastTimeDamaged = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (lastTimeDamaged < 0 || Time.time > lastTimeDamaged + damageTime) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void Killed() {
      Destroy(gameObject);
    }

    public void Damaged() {
        lastTimeDamaged = Time.time;
    }

    public override void DestroyEvent() {
        throw new System.NotImplementedException();
    }

    public override void DamageEvent() {
        throw new System.NotImplementedException();
    }
}
