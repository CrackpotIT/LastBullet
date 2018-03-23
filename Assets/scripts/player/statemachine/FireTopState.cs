using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTopState: AbstractState {

    private bool animationFinished = false;

    public FireTopState(PlayerController playerController) : base(playerController) {
    }

    public override void OnEnter() {
        Debug.Log("FireBottomState OnEnter");

        if (playerController.gun.HasAmmunition()) {
            playerController.playerModel.SetAnimatorBool(PlayerModel.FIRE_TOP, true);
            playerController.gunModel.SetAnimatorBool(GunModel.FIRE_TOP, true);
        } else {
            animationFinished = true;
        }
        playerController.gun.FireGun(playerController.currentDirectionX);
    }

    public override AbstractState UpdateState() {
        if (animationFinished) {
            return new IdleTopState(currentAction, playerController);
        }
        return null;
    }

    public override void OnExit() {
        playerController.playerModel.SetAnimatorBool(PlayerModel.FIRE_TOP, false);
        playerController.gunModel.SetAnimatorBool(GunModel.FIRE_TOP, false);
        playerController.gunModel.SetAnimatorBool(GunModel.FIRE_TOP_RELEASE, false);
    }

    public override void HandleAnimEvent(string parameter) {
        if (parameter == GunModel.ANIM_PARAM_FIRE_RELEASE) {
            playerController.gunModel.SetAnimatorBool(GunModel.FIRE_TOP_RELEASE, true);
        } else {
            animationFinished = true;
        }
    }
}
