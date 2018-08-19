using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCanvasController : MonoBehaviour {

    public GameObject shootButtonsParent;
    public GameObject reloadButton;
    public GameObject reloadGameButtonsParent;
    public GameObject lootButton;
    public GameObject weaponChangeButton;
    public GameObject utilityChangeButton;

    // Static instance
    static InputCanvasController _instance;

    private void Awake() {
        _instance = this;
    }

    public static InputCanvasController GetInstance() {
        if (!_instance) {
            Debug.LogError("Instance of InputCanvasController not available");
        }
        return _instance;
    }


    public void SwitchNormalMode() {
        shootButtonsParent.SetActive(true);
        reloadButton.SetActive(true);
        weaponChangeButton.SetActive(true);
        utilityChangeButton.SetActive(true);

        reloadGameButtonsParent.SetActive(false);

        // sichtbare Buttons switchen
        ButtonsController.GetInstance().SwitchNormalMode();
    }

    public void SwitchReloadGameMode() {
        shootButtonsParent.SetActive(false);
        reloadButton.SetActive(false);
        weaponChangeButton.SetActive(false);
        utilityChangeButton.SetActive(false);

        reloadGameButtonsParent.SetActive(true);

        // sichtbare Buttons switchen
        ButtonsController.GetInstance().SwitchReloadGameMode();
    }
}
