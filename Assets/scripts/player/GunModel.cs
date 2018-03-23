using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModel : MonoBehaviour {

    public static string IDLE_TOP = "IDLE_TOP";
    public static string IDLE_BOTTOM = "IDLE_BOTTOM";
    public static string MOVE_UP = "MOVE_UP";
    public static string MOVE_DOWN = "MOVE_DOWN";
    public static string FIRE_TOP = "FIRE_TOP";
    public static string FIRE_TOP_RELEASE = "FIRE_TOP_RELEASE";
    public static string FIRE_BOTTOM = "FIRE_BOTTOM";
    public static string FIRE_BOTTOM_RELEASE = "FIRE_BOTTOM_RELEASE";
    public static string RELOAD = "RELOAD";

    public static string ANIM_PARAM_FIRE_RELEASE = "FIRE_RELEASE";
    
    private Animator animator;
    private PlayerController playerController;
    private Gun gun;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
        playerController = transform.parent.GetComponent<PlayerController>();
        gun = transform.GetComponentInChildren<Gun>();
    }

    public void AnimationEvent(string param) {
        if (playerController != null) {
            playerController.currentState.HandleAnimEvent(param);
        } else {
            Debug.LogError("PlayerModel: PlayerController not found!");
        }
    }

    public void SetAnimatorBool(string parameter, bool value) {
        animator.SetBool(parameter, value);
        if (parameter == FIRE_TOP_RELEASE || parameter == FIRE_BOTTOM_RELEASE) {
            if (!gun.HasAmmunition()) {
                ShowClipEmptyLayer(true);
            }
        }
    }

    public void ShowClipEmptyLayer(bool show) {
        if (show) {
            animator.SetLayerWeight(1, 1);
        } else {
            animator.SetLayerWeight(1, 0);
        }
    }
}
