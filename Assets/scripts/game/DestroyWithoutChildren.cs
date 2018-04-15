using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithoutChildren : MonoBehaviour {
    
	
	// Checks if children are alive, if not makes suicide
	void Update () {
		if (transform.childCount == 0) {
            Destroy(gameObject);
        }
	}
}
