using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour {

    public static string IDLE_TOP = "IDLE_TOP";
    public static string IDLE_BOTTOM = "IDLE_BOTTOM";
    public static string MOVE_UP = "MOVE_UP";
    public static string MOVE_DOWN = "MOVE_DOWN";
    public static string FIRE_TOP = "FIRE_TOP";
    public static string FIRE_BOTTOM = "FIRE_BOTTOM";
    public static string RELOAD = "RELOAD";

    
    private Animator animator;
    private PlayerController playerController;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        playerController = transform.parent.GetComponent<PlayerController>();
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
    }
}
