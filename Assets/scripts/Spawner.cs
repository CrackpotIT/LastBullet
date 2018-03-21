using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    
	public GameObject[] attackerPrefabArray;

    private void Start() {
        
    }

    // Update is called once per frame
    void Update () {
		foreach (GameObject thisAttacker in attackerPrefabArray) {
			if (IsTimeToSpawn(thisAttacker)) {
				Spawn(thisAttacker);
			}
		}
	}
	
	void Spawn(GameObject thisAttacker) {
		GameObject newAttacker = (GameObject)Instantiate(thisAttacker);
        EnemyController enemyController = newAttacker.GetComponent<EnemyController>();
        enemyController.targetPosition = getTargetPosition();
        newAttacker.transform.parent = transform;
		newAttacker.transform.position = transform.position;
	}

    private Vector2 getTargetPosition() {
        float targetX = 0;
        if (transform.position.x > 0) {
            // spawner right from player
            targetX = 1;
        } else {
            // spawner left from player
            targetX = -1;
        }
        Vector2 result = new Vector2(targetX, transform.position.y);
        return result;
    }
	
	bool IsTimeToSpawn(GameObject thisAttacker) {
        EnemyController attacker = thisAttacker.GetComponent<EnemyController>();
		float meanSpawnTime = attacker.seenEverySeconds;
		float spawnsPerSecond = 1 / meanSpawnTime;
		
		if (Time.deltaTime > meanSpawnTime) {
			Debug.LogWarning("Spawnrate capped by frame rate!");
		}
		
		float threshold = spawnsPerSecond * Time.deltaTime / 6;
		float random = Random.value;
		
		if (random < threshold) {
			return true;
		} else {
			return false;
		}
	}
}
