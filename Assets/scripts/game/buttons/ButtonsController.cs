using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour {

    public GameObject shootButtons;

    public GameObject reloadButtons;


    // Static instance
    static ButtonsController _instance;

    private void Awake() {
        _instance = this;
    }

    public static ButtonsController GetInstance() {
        if (!_instance) {
            Debug.LogError("Instance of ButtonsController not available");
        }
        return _instance;
    }

    public void SwitchNormalMode() {
        shootButtons.SetActive(true);
        reloadButtons.SetActive(false);
    }

    public void SwitchReloadGameMode() {
        shootButtons.SetActive(false);
        reloadButtons.SetActive(true);
    }
}
