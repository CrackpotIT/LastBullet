using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBottomState: AbstractState {

    public IdleBottomState(ACTION action) {
        currentAction = action;
    }

    public override void OnEnter(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        playerModel.animator.SetBool(PlayerModel.IDLE_BOTTOM, true);
        gunModel.animator.SetBool(GunModel.IDLE_BOTTOM, true);
        playerController.currentDirectionY = -1;
    }

    public override AbstractState UpdateState(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        
        if (currentAction == ACTION.TOP_LEFT || currentAction == ACTION.TOP_RIGHT) {
            UpdateDirectionX(playerController);
            return new MoveUpState(currentAction);
        }

        if (currentAction == ACTION.BOTTOM_LEFT || currentAction == ACTION.BOTTOM_RIGHT) {
            UpdateDirectionX(playerController);
            if (playerController.gun.IsReady()) {
                return new FireBottomState();
            }            
        }

        return null;
    }

    public override void OnExit(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        playerModel.animator.SetBool(PlayerModel.IDLE_BOTTOM, false);
        gunModel.animator.SetBool(GunModel.IDLE_BOTTOM, false);
    }
}
