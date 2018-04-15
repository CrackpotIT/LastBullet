using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public 

    // Static instance
    static GameController _instance;
    
    void Start () {
        _instance = this;
	}

    public static GameController GetInstance() {
        if (!_instance) {
            Debug.LogError("Instance of GameController not available");
        }
        return _instance;
    }
    
}
