using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBottomState: AbstractState {

    public IdleBottomState(ACTION action, PlayerController playerController) : base(playerController) {
        currentAction = action;
    }
    
    public override void OnEnter() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.IDLE_BOTTOM, true);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.IDLE_BOTTOM, true);
    }

    public override AbstractState UpdateState() {
        if (currentAction == ACTION.RELOAD) {
            return new ReloadBottomState(playerController);
        }

        if (currentAction == ACTION.TOP_LEFT || currentAction == ACTION.TOP_RIGHT) {
            UpdateDirectionX();
            return new MoveUpState(currentAction, playerController);
        }

        if (currentAction == ACTION.BOTTOM_LEFT || currentAction == ACTION.BOTTOM_RIGHT) {
            UpdateDirectionX();
            if (playerController.gunModel.PullTriggerReady()) {
                return new FireBottomState(playerController);
            }            
        }

        return null;
    }

    public override void OnExit() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.IDLE_BOTTOM, false);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.IDLE_BOTTOM, false);
    }
}
