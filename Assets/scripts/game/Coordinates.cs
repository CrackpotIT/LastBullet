using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinates : MonoBehaviour {

    [HideInInspector]
    public Transform top;
    [HideInInspector]
    public Transform bottom;
    [HideInInspector]
    public Transform loot;

    // Use this for initialization
    void Start () {
        top = transform.Find("Top").transform;
        bottom = transform.Find("Bottom").transform;
        loot = transform.Find("Loot").transform;
    }
    

    
	
}
