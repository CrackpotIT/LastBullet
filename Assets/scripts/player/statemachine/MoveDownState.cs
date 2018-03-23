using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownState : AbstractState {

    public MoveDownState(ACTION action) {
        currentAction = action;
    }

    public override void OnEnter(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        playerModel.animator.SetBool(PlayerModel.MOVE_DOWN, true);
        gunModel.animator.SetBool(GunModel.MOVE_DOWN, true);
        playerController.currentDirectionY = 0;
        playerController.MovePlayer(playerController.coordinates.bottom.position);
    }

    public override AbstractState UpdateState(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {

        if (!playerController.isMoving) {
            if (playerController.gun.IsReady()) {
                return new FireBottomState();
            } else {
                return new IdleBottomState(currentAction);
            }
        }
        return null;
    }


    public override void OnExit(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        playerModel.animator.SetBool(PlayerModel.MOVE_DOWN, false);
        gunModel.animator.SetBool(GunModel.MOVE_DOWN, false);
    }
}
