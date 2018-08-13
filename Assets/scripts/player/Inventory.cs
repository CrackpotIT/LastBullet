
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    static Inventory _instance;

    public int coins;
    public Dictionary<Bullet.BULLET_TYPE, int> bullets = new Dictionary<Bullet.BULLET_TYPE, int>();

    public static Inventory GetInstance() {
        if (!_instance) {
            Debug.LogError("Instance of GameController not available");
        }
        return _instance;
    }

    void Awake() {

        // initial Inventory
        bullets[Bullet.BULLET_TYPE.MM9] = 100;
        bullets[Bullet.BULLET_TYPE.Point32] = 100;

        //Check if instance already exists
        if (_instance == null)

            //if not, set instance to this
            _instance = this;

        //If instance already exists and it's not this:
        else if (_instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

    
    }
}
