using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets32Loot: Loot {
    

    public new void Start() {
        base.lootType = LOOT_TYPE.AMMUNITION;
        base.Start();
    }

    public override string GetText() {
        string text = "";
        text = "+" + amount.ToString() + " Ammo .32";
        return text;
    }

    public override void TakeIt() {
        Player.GetInstance().AddAmmunition(Bullet.BULLET_TYPE.Point32, amount);
    }
}
