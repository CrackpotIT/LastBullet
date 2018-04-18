using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLoot : Loot {

    public int amount;

    public override string GetText() {
        string text = "";
        text = "+" + amount.ToString() + " coins";
        return text;
    }

    public override void TakeIt() {
        Inventory.instance.coins += amount;
    }
    
}
