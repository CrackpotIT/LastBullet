using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBottomState: AbstractState {

    private bool animationFinished = false;

    public override void OnEnter(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        Debug.Log("FireBottomState OnEnter");

        if (playerController.gun.HasAmmunition()) {
            playerModel.animator.SetBool(PlayerModel.FIRE_BOTTOM, true);
            gunModel.animator.SetBool(GunModel.FIRE_BOTTOM, true);
        } else {
            animationFinished = true;
        }
        playerController.gun.FireGun(playerController.currentDirectionX);
    }

    public override AbstractState UpdateState(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {

        if (animationFinished) {
            return new IdleBottomState(currentAction);
        }
        return null;
    }

    public override void OnExit(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        playerModel.animator.SetBool(PlayerModel.FIRE_BOTTOM, false);
        gunModel.animator.SetBool(GunModel.FIRE_BOTTOM, false);
    }

    public override void HandleAnimEvent(string parameter) {
        animationFinished = true;
    }
}
