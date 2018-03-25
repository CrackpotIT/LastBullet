using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTopState: AbstractState {

    private bool animationFinished = false;

    public FireTopState(PlayerController playerController) : base(playerController) {
    }

    public override void OnEnter() {
        Debug.Log("FireBottomState OnEnter");

        if (playerController.gunModel.HasAmmunition()) {
            playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.FIRE_TOP, true);
            playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.FIRE_TOP, true);
        } else {
            animationFinished = true;
        }
        playerController.gunModel.FireGun(playerController.currentDirectionX, true);
    }

    public override AbstractState UpdateState() {
        if (animationFinished) {
            return new IdleTopState(currentAction, playerController);
        }
        return null;
    }

    public override void OnExit() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.FIRE_TOP, false);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.FIRE_TOP, false);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.FIRE_TOP_RELEASE, false);
    }

    public override void HandleAnimEvent(string parameter) {
        if (parameter == AbstractModel.ANIM_EVENT_PARAMS.FIRE_RELEASE.ToString()) {
            playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.FIRE_TOP_RELEASE, true);
        } else {
            animationFinished = true;
        }
    }
}
