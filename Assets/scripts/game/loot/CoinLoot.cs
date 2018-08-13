using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLoot : Loot {


    public new void Start() {
        base.lootType = LOOT_TYPE.COIN;
        base.Start();
    }

    public override string GetText() {
        
        string text = "";
        text = "+" + amount.ToString() + " coins";
        return text;
    }

    public override void TakeIt() {
        Inventory.GetInstance().coins += amount;
    }
    
}
