using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractModel : MonoBehaviour {

    public enum ANIM_PARAMS {IDLE_TOP, IDLE_BOTTOM, MOVE_UP, MOVE_DOWN, FIRE_TOP, FIRE_BOTTOM, RELOAD, FIRE_TOP_RELEASE, FIRE_BOTTOM_RELEASE };
    public enum ANIM_EVENT_PARAMS { FIRE_RELEASE };

    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public PlayerController playerController;


    public virtual void Start () {
        animator = GetComponent<Animator>();
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    public void AnimationEvent(string param) {
        if (playerController != null) {
            playerController.currentState.HandleAnimEvent(param);
        } else {
            Debug.LogError("AbstractModel: PlayerController not found!");
        }
    }

    public virtual void SetAnimatorBool(ANIM_PARAMS parameter, bool value) {
        animator.SetBool(parameter.ToString(), value);
    }
}
