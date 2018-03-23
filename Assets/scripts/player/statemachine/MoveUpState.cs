using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpState : AbstractState {

    public MoveUpState(ACTION action, PlayerController playerController) : base(playerController) {
        currentAction = action;
    }

    public override void OnEnter() {
        playerController.playerModel.SetAnimatorBool(PlayerModel.MOVE_UP, true);
        playerController.gunModel.SetAnimatorBool(GunModel.MOVE_UP, true);
        playerController.currentDirectionY = 0;
        playerController.MovePlayer(playerController.coordinates.top.position);
    }

    public override AbstractState UpdateState() {

        if (!playerController.isMoving) {
            if (playerController.gun.IsReady()) {
                return new FireTopState(playerController);
            } else {
                return new IdleTopState(currentAction, playerController);
            }
        }
        return null;
    }

    public override void OnExit() {
        playerController.playerModel.SetAnimatorBool(PlayerModel.MOVE_UP, false);
        playerController.gunModel.SetAnimatorBool(GunModel.MOVE_UP, false);
    }
}
