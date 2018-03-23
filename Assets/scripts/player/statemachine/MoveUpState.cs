using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpState : AbstractState {

    public MoveUpState(ACTION action) {
        currentAction = action;
    }

    public override void OnEnter(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        playerModel.animator.SetBool(PlayerModel.MOVE_UP, true);
        gunModel.animator.SetBool(GunModel.MOVE_UP, true);
        playerController.currentDirectionY = 0;
        playerController.MovePlayer(playerController.coordinates.top.position);
    }

    public override AbstractState UpdateState(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {

        if (!playerController.isMoving) {
            if (playerController.gun.IsReady()) {
                return new FireTopState();
            } else {
                return new IdleTopState(currentAction);
            }
        }
        return null;
    }

    public override void OnExit(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        playerModel.animator.SetBool(PlayerModel.MOVE_UP, false);
        gunModel.animator.SetBool(GunModel.MOVE_UP, false);
    }
}
