using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour {

    public int maxHealth = 1;
    public int health = 1;
    public float xp = 0;
    public int level = 0;

    // Static instance
    static Player _instance;

    private void Start() {
        _instance = this;
    }


    public static Player GetInstance() {
        if (_instance == null) {
            Debug.LogError("Player Obj does not exist");
        }
        return _instance;
    }

    public void AddXp(float xpAdd) {
        xp += xpAdd;
        float nextLevel = (level + 1) * 100;
        float xpPercent = xp / nextLevel;
        Debug.Log("XpPercent: " + xpPercent);
        GuiController.GetInstance().RefreshXpCount(xpPercent);
    }

    public void Damage(int damage) {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

    }

    public void Heal(int healing) {
        health = Mathf.Min(health + healing, maxHealth);

    }
}
