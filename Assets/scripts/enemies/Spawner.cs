using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    
	public GameObject[] attackerPrefabArray;
    public float[] everySecondsArray;
    public float minTimeBetweenSpawns;

    private float lastSpawn = 0;

    private void Start() {
        
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < attackerPrefabArray.Length; i++) {
            if (IsTimeToSpawn(everySecondsArray[i])) {
                Spawn(attackerPrefabArray[i]);
            }
        }
	}
	
	void Spawn(GameObject thisAttacker) {
		GameObject newAttacker = (GameObject)Instantiate(thisAttacker, transform);
        AbstractEnemySpawn enemyController = newAttacker.GetComponent<AbstractEnemySpawn>();

        enemyController.Initialize(getTargetPosition(), GetDirectionX());
    }


    private float GetDirectionX() {
        float targetX = 0;
        if (transform.position.x > 0) {
            // spawner right from player
            targetX = -1;
        } else {
            // spawner left from player
            targetX = 1;
        }
        return targetX;
    }

    
    private Vector2 getTargetPosition() {
        float targetX = GetDirectionX();
        Vector2 result = new Vector2(-targetX, transform.position.y);
        return result;
    }
	
	bool IsTimeToSpawn(float meanSpawnTime) {
		float spawnsPerSecond = 1 / meanSpawnTime;
		
		if (Time.deltaTime > meanSpawnTime) {
			Debug.LogWarning("Spawnrate capped by frame rate!");
		}
		
		float threshold = spawnsPerSecond * Time.deltaTime / 4;
		float random = Random.value;
		
		if (random < threshold) {
            if (Time.time > (lastSpawn + minTimeBetweenSpawns)) {
                lastSpawn = Time.time;
                return true;
            }
            return false;
		} else {
            if (Time.time > (lastSpawn + (meanSpawnTime * 4 * 2))) {
                lastSpawn = Time.time;
                return true;
            }
            return false;
		}
	}
}
