
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory instance;

    public int coins;
    public Dictionary<Bullet.BULLET_TYPE, int> bullets = new Dictionary<Bullet.BULLET_TYPE, int>();
        
    
    void Awake() {

        // initial Inventory
        bullets[Bullet.BULLET_TYPE.MM9] = 100;

        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

    
    }
}
