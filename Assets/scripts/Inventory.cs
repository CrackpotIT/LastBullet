using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    public Dictionary<Bullet.BULLET_TYPE, int> bullets = new Dictionary<Bullet.BULLET_TYPE, int>();

    public Inventory() {
        // initial Inventory
        bullets[Bullet.BULLET_TYPE.MM9] = 100;
    }
}
