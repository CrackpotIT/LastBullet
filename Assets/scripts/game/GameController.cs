using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public List<SerializeTest> testList;
    public List<GameObject> lootList;
    public GameObject lootEffect;
    public GameObject lootText;
    public GameObject lootTextEffect;
    public AudioClip lootSound;

    private Coordinates coordinates;

    // Static instance
    static GameController _instance;
    
    void Start () {
        _instance = this;
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
    
}
