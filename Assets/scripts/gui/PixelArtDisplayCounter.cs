using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelArtDisplayCounter : MonoBehaviour {

    public float offSetX;
    public int sortOrder;
    public bool writeToLeft = false;
    
    public Sprite[] spriteNumberArray;


    public void RefreshDisplay(string text) {
        Debug.Log(text);
        // zuerst alte Darstellung löschen
        Debug.Log("ChildCount0:" + transform.childCount);
        int childs = transform.childCount;
        for (int i = 0; i < childs; i++) {            
            DestroyImmediate(transform.GetChild(0).gameObject);
            Debug.Log("Destroyed " +i);
        }
        Debug.Log("ChildCount1:" + transform.childCount);

        if (writeToLeft) {
            for (int i = text.Length - 1; i >= 0; i--) {
                int value = CalculateArrayIndex(text[i]);
                CreateNumberSprite(spriteNumberArray[value], (text.Length) - i);
            }
        } else {
            for (int i = 0; i < text.Length; i++) {
                int value = CalculateArrayIndex(text[i]);
                CreateNumberSprite(spriteNumberArray[value], i+1);
            }
        }

        Debug.Log("ChildCount2:" + transform.childCount);
    }

    private int CalculateArrayIndex(char c) {
        int val = 10;
        if (c == ' ') {
            val = 11;
        } else {
            int number = (int)char.GetNumericValue(c);
            if (number >= 0) {
                val = number;
            }
        }
        return val;
    }


    private void CreateNumberSprite(Sprite newSprite, int position) {
        GameObject objToSpawn = new GameObject("SpriteNumber");
        objToSpawn.transform.parent = transform;
        int writeToLeftMultiplicator = (writeToLeft ? -1 : 1);
        
        float x = writeToLeftMultiplicator * ((position * offSetX) + (writeToLeft ? 0 : offSetX)) ;
        float y = 0;
        // aus irgendeinem Grund gibt es Probleme wenn alle die gleiche Z Position haben. Scalieren nicht mehr pixelgenau
        float z = transform.position.z + (writeToLeftMultiplicator * (position / 100f));
        objToSpawn.transform.localPosition = new Vector3(x,y,z);
        //Add Components
        objToSpawn.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = objToSpawn.GetComponent<SpriteRenderer>();
        sr.sortingOrder = sortOrder;
        sr.sprite = newSprite;
    }
}
