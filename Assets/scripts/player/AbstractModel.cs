using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractModel : MonoBehaviour {

    public enum ANIM_PARAMS {IDLE_TOP, IDLE_BOTTOM, MOVE_UP, MOVE_DOWN, FIRE_TOP, FIRE_BOTTOM, RELOAD_TOP, RELOAD_BOTTOM };
    public enum ANIM_PARAMS_GUNS { IDLE, MOVE_UP, MOVE_DOWN, FIRE, FIRE_RELEASE, RELOAD};
    public enum ANIM_EVENT_PARAMS { FIRE_RELEASE };

    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public PlayerController playerController;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;


    public virtual void Start () {
        animator = GetComponent<Animator>();
        playerController = transform.parent.GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    public virtual void SetAnimatorBool(ANIM_PARAMS_GUNS parameter, bool value) {
        animator.SetBool(parameter.ToString(), value);
    }

    public AnimationClip GetAnimationClip(string name) {
        if (!animator) return null; // no animator

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips) {
            if (clip.name == name) {
                return clip;
            }
        }
        return null; // no clip by that name
    }
}
