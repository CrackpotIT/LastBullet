using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTopState : AbstractState {

    public IdleTopState(ACTION action) {
        currentAction = action;
    }

    public override void OnEnter(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        playerModel.animator.SetBool(PlayerModel.IDLE_TOP, true);
        gunModel.animator.SetBool(GunModel.IDLE_TOP, true);
        playerController.currentDirectionY = -1;
    }

    public override AbstractState UpdateState(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {

        if (currentAction == ACTION.BOTTOM_LEFT || currentAction == ACTION.BOTTOM_RIGHT) {
            UpdateDirectionX(playerController);
            return new MoveDownState(currentAction);
        }

        if (currentAction == ACTION.TOP_LEFT || currentAction == ACTION.TOP_RIGHT) {
            UpdateDirectionX(playerController);
            if (playerController.gun.IsReady()) {
                return new FireTopState();
            }
        }

        return null;
    }

    public override void OnExit(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        playerModel.animator.SetBool(PlayerModel.IDLE_TOP, false);
        gunModel.animator.SetBool(GunModel.IDLE_TOP, false);
    }
}
