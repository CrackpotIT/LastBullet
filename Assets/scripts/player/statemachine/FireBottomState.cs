using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBottomState: AbstractState {

    private bool animationFinished = false;

    public FireBottomState(PlayerController playerController) : base(playerController) {        
    }

    public override void OnEnter() {
        if (playerController.gunModel.HasAmmunition()) {
            playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.FIRE_BOTTOM, true);
            playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.FIRE_BOTTOM, true);
        } else {
            animationFinished = true;
        }
        playerController.gunModel.FireGun(playerController.currentDirectionX, false);
    }

    public override AbstractState UpdateState() {

        if (animationFinished) {
            return new IdleBottomState(currentAction, playerController);
        }
        return null;
    }

    public override void OnExit() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.FIRE_BOTTOM, false);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.FIRE_BOTTOM, false);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.FIRE_BOTTOM_RELEASE, false);
    }

    public override void HandleAnimEvent(string parameter) {
        if (parameter == AbstractModel.ANIM_EVENT_PARAMS.FIRE_RELEASE.ToString()) {
            playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.FIRE_BOTTOM_RELEASE, true);
        } else {
            animationFinished = true;
        }
    }
}
