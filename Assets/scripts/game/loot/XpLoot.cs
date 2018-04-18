using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpLoot: Loot {

    public float amount;

    public override string GetText() {
        string text = "";
        text = "+" + amount.ToString() + " XP";
        return text;
    }

    public override void TakeIt() {
        Player.GetInstance().AddXp(amount);
    }
}
