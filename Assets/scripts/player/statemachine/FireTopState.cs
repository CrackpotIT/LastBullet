using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTopState: AbstractState {

    private bool animationFinished = false;

    public override void OnEnter(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        Debug.Log("FireBottomState OnEnter");

        if (playerController.gun.HasAmmunition()) {
            playerModel.animator.SetBool(PlayerModel.FIRE_TOP, true);
            gunModel.animator.SetBool(GunModel.FIRE_TOP, true);
        } else {
            animationFinished = true;
        }
        playerController.gun.FireGun(playerController.currentDirectionX);
    }

    public override AbstractState UpdateState(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {

        if (animationFinished) {
            return new IdleTopState(currentAction);
        }
        return null;
    }

    public override void OnExit(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        playerModel.animator.SetBool(PlayerModel.FIRE_TOP, false);
        gunModel.animator.SetBool(GunModel.FIRE_TOP, false);
    }

    public override void HandleAnimEvent(string parameter) {
        animationFinished = true;
    }
}
