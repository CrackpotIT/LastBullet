using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTopState : AbstractState {

    public IdleTopState(ACTION action, PlayerController playerController) : base(playerController) {
        currentAction = action;
    }

    public override void OnEnter() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.IDLE_TOP, true);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.IDLE_TOP, true);
    }

    public override AbstractState UpdateState() {
        if (currentAction == ACTION.RELOAD) {
            return new ReloadGameState(true, playerController);
        }

        if (currentAction == ACTION.BOTTOM_LEFT || currentAction == ACTION.BOTTOM_RIGHT) {
            UpdateDirectionX();
            return new MoveDownState(currentAction, playerController);
        }

        if (currentAction == ACTION.TOP_LEFT || currentAction == ACTION.TOP_RIGHT) {
            UpdateDirectionX();
            if (playerController.gunModel.PullTriggerReady()) {
                return new FireTopState(playerController);
            }
        }

        return null;
    }

    public override void OnExit() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.IDLE_TOP, false);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.IDLE_TOP, false);
    }
}
