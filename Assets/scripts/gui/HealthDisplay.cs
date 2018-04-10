using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour {

    public float offSetX;
    public int sortOrder;
    public int maxHealth;

    public Sprite heartSprite;

    public void RefreshDisplay(int healthCount) {
        // zuerst alte Darstellung löschen
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }

        for(int i = 1; i<= (healthCount > maxHealth ? maxHealth : healthCount); i++) {
            CreateHeartSprite(i);
            }
    }

    private void CreateHeartSprite(int position) {
        GameObject objToSpawn = new GameObject("SpriteNumber");
        objToSpawn.transform.parent = transform;

        float x = ((position * offSetX) + offSetX);
        float y = 0;
        // aus irgendeinem Grund gibt es Probleme wenn alle die gleiche Z Position haben. Scalieren nicht mehr pixelgenau
        float z = transform.position.z + (position / 100f);
        objToSpawn.transform.localPosition = new Vector3(x, y, z);
        //Add Components
        objToSpawn.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = objToSpawn.GetComponent<SpriteRenderer>();
        sr.sortingOrder = sortOrder;
        sr.sprite = heartSprite;
    }
}
