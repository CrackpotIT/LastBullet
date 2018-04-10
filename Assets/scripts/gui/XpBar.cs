using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpBar : MonoBehaviour {

    public float offSetX;
    public float maxDimensionX;
    public int sortOrder;
    public Sprite barSprite;

    public void Start() {
        RefreshDisplay(1f);
    }

    public void RefreshDisplay(float xpPercent) {
        // zuerst alte Darstellung löschen
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }

        int maxCount = Mathf.FloorToInt(maxDimensionX / offSetX);
        int numberOfBars = Mathf.FloorToInt(maxCount * Mathf.Clamp(xpPercent, 0, 1));

        Debug.Log("Number Bars: " +numberOfBars);
        for (int i = 0; i < numberOfBars; i++) {
            CreateBarSprite(i);
        }
    }

    private void CreateBarSprite(int position) {
        GameObject objToSpawn = new GameObject("SpriteBar");
        objToSpawn.transform.parent = transform;

        float x = (position * offSetX) + offSetX;
        float y = 0;
        // aus irgendeinem Grund gibt es Probleme wenn alle die gleiche Z Position haben. Scalieren nicht mehr pixelgenau
        float z = transform.position.z + (position / 100f);
        objToSpawn.transform.localPosition = new Vector3(x, y, z);
        //Add Components
        objToSpawn.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = objToSpawn.GetComponent<SpriteRenderer>();
        sr.sortingOrder = sortOrder;
        sr.sprite = barSprite;
    }

}
