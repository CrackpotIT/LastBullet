using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    
    public List<GameObject> lootList;
    public GameObject lootEffect;
    public GameObject lootText;
    public GameObject lootTextEffect;
    public AudioClip lootSound;

    private Coordinates coordinates;

    // Static instance
    static GameController _instance;

    private void Awake() {
        _instance = this;
    }

    void Start () {        
        coordinates = GameObject.FindObjectOfType<Coordinates>();        
	}


    public static GameController GetInstance() {
        if (!_instance) {
            Debug.LogError("Instance of GameController not available");
        }
        return _instance;
    }

    public void InstantiateLoot() {
        // zuerst alte Loots löschen
        foreach (Transform child in coordinates.loot) {
            GameObject.Destroy(child.gameObject);
        }

        // play sound
        SoundManager.PlaySFX(lootSound);

        Instantiate(lootEffect, coordinates.loot);
        
        int random = Random.Range(0, lootList.Count);
        Instantiate(lootList[random], coordinates.loot);
    }

    public void GetLoot() {
        Loot loot = coordinates.loot.GetComponentInChildren<Loot>();
        if (!loot) { 
            Debug.Log("No loot!");
        } else {
            loot.Collect();
            
            GameObject lootTextInstance = Instantiate(lootText);
            TextMesh textMesh = lootTextInstance.GetComponent<TextMesh>();
            textMesh.text = loot.GetText();

            Instantiate(lootTextEffect, lootTextInstance.transform);
        }
    }

    public void UpdateSize() {
        // Refresh Gui positions
        GuiController.GetInstance().RefreshPositions();

        // Refresh Shredders
        Shredder[] shredderList = GameObject.FindObjectsOfType<Shredder>();
        foreach(Shredder shredder in shredderList) {
            shredder.RefreshPosition();
        }

        // Refresh Spawners
        Spawner[] spawnerList = GameObject.FindObjectsOfType<Spawner>();
        foreach (Spawner spawner in spawnerList) {
            spawner.RefreshPosition();
        }
    }
    
}
