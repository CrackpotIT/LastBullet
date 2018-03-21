using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBottomState: AbstractState {

    private bool animationFinished = false;

    public override void OnEnter(PlayerController playerController, Animator animator) {
        Debug.Log("FireBottomState OnEnter");

        if (playerController.gun.HasAmmunition()) {
            animator.SetBool(FIRE_BOTTOM, true);
        } else {
            animationFinished = true;
        }
        playerController.gun.FireGun(playerController.currentDirectionX);
    }

    public override AbstractState UpdateState(PlayerController playerController, Animator animator) {

        if (animationFinished) {
            return new IdleBottomState(currentAction);
        }
        return null;
    }

    public override void OnExit(PlayerController playerController, Animator animator) {
        animator.SetBool(FIRE_BOTTOM, false);
    }

    public override void HandleAnimEvent(string parameter, PlayerController playerController, Animator animator) {
        animationFinished = true;
    }
}
