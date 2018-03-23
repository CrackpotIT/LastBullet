using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBottomState: AbstractState {

    private bool animationFinished = false;

    public FireBottomState(PlayerController playerController) : base(playerController) {        
    }

    public override void OnEnter() {
        if (playerController.gun.HasAmmunition()) {
            playerController.playerModel.SetAnimatorBool(PlayerModel.FIRE_BOTTOM, true);
            playerController.gunModel.SetAnimatorBool(GunModel.FIRE_BOTTOM, true);
        } else {
            animationFinished = true;
        }
        playerController.gun.FireGun(playerController.currentDirectionX);
    }

    public override AbstractState UpdateState() {

        if (animationFinished) {
            return new IdleBottomState(currentAction, playerController);
        }
        return null;
    }

    public override void OnExit() {
        playerController.playerModel.SetAnimatorBool(PlayerModel.FIRE_BOTTOM, false);
        playerController.gunModel.SetAnimatorBool(GunModel.FIRE_BOTTOM, false);
        playerController.gunModel.SetAnimatorBool(GunModel.FIRE_BOTTOM_RELEASE, false);
    }

    public override void HandleAnimEvent(string parameter) {
        if (parameter == GunModel.ANIM_PARAM_FIRE_RELEASE) {
            playerController.gunModel.SetAnimatorBool(GunModel.FIRE_BOTTOM_RELEASE, true);
        } else {
            animationFinished = true;
        }
    }
}
