using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    
    public List<GameObject> lootList;
    public GameObject spawnObject;
    public GameObject lootCreate;
    public GameObject lootText;
    public AudioClip lootSound;

    public GameObject mouseClickEffect;

    private Coordinates coordinates;
    private GameObject lootBrackets;

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

    public void InstantiateLoot(Loot.LOOT_AMOUNT lootAmount) {
        // zuerst alte Loots löschen
        foreach (Transform child in coordinates.loot) {
            GameObject.Destroy(child.gameObject);
        }

        // play sound
        SoundManager.PlaySFX(lootSound);

        lootBrackets = Instantiate(lootCreate, coordinates.loot);
        
        int random = Random.Range(0, lootList.Count);
        GameObject lootGO = Instantiate(lootList[random], coordinates.loot);
        Loot loot = lootGO.GetComponent<Loot>();
        loot.InitAmount(lootAmount);
    }

    public void GetLoot() {
        Loot loot = coordinates.loot.GetComponentInChildren<Loot>();
        if (!loot) { 
            Debug.Log("No loot!");
        } else {
            if (!loot.collected) {
                loot.Collect();

                GameObject lootTextInstance = Instantiate(lootText);
                TextMesh textMesh = lootTextInstance.GetComponent<TextMesh>();
                textMesh.text = loot.GetText();

                Destroy(lootBrackets);
            }
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

        // Refresh Shoot Buttons
        ShootButtonController[] shootButtons = GameObject.FindObjectsOfType<ShootButtonController>();
        foreach (ShootButtonController shootButtonImage in shootButtons) {
            shootButtonImage.RefreshPosition();
        }        

        // Refresh Reload Buttons
        ReloadGameTapButton[] reloadButtons = GameObject.FindObjectsOfType<ReloadGameTapButton>();
        foreach (ReloadGameTapButton reloadButton in reloadButtons) {
            reloadButton.RefreshPosition();
        }
    }

    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Pressed Mouse Button!");

            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            
            GameObject mouseClick = Instantiate(mouseClickEffect);
            mouseClick.transform.position = pz;
            int randomX = Random.Range(0, 2);
            int randomY = Random.Range(0, 2);
            mouseClick.transform.localScale = new Vector3((randomX == 1 ? 1 : -1), (randomY == 1 ? 1 : -1), 1);
        }
    }

}
