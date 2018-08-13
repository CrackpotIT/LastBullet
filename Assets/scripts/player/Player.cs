using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour {

    public int maxHealth = 1;
    public int health = 1;
    public float xp = 0;
    public int level = 0;

    private GunModel activeGun;

    // Static instance
    static Player _instance;

    private void Start() {
        _instance = this;
        PlayerController pc = this.GetComponent<PlayerController>();
        if (pc) {
            Debug.Log("PlayerController Found!");

            GameObject instance = Instantiate(Resources.Load("player/weapons/GunModel_SuckSour", typeof(GameObject))) as GameObject;
            instance.transform.position = transform.position;
            instance.transform.parent = transform;
            activeGun = instance.GetComponent<GunModel>();

            pc.gunModel = activeGun;
            pc.playerModel = transform.GetComponentInChildren<PlayerModel>();            
        }

        SoundManager.SetGlobalVolume(.5f);
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
        GuiController.GetInstance().RefreshXpCount(xpPercent);
    }

    public void AddAmmunition(Bullet.BULLET_TYPE bulletType, int amount) {
        Inventory inv = Inventory.GetInstance();
        inv.bullets[bulletType] += amount;
        GuiController.GetInstance().RefreshBulletCount(activeGun.currentClip, inv.bullets[bulletType]);
    }

    public void Damage(int damage) {
        health = Mathf.Clamp(health - damage, 0, maxHealth);

    }

    public void Heal(int healing) {
        health = Mathf.Min(health + healing, maxHealth);

    }
}
