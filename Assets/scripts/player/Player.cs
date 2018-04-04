using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player {

    private int xp = 0;

    // Static instance
    static Player _instance;


    public static Player GetInstance() {
        if (_instance == null) {
            _instance =  new Player();
        }
        return _instance;
    }

    public void AddXp(int xpAdd) {
        xp += xpAdd;
        GuiController.GetInstance().RefreshXpCount(xp);
    }
}
