using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpState : AbstractState {

    public MoveUpState(ACTION action, PlayerController playerController) : base(playerController) {
        currentAction = action;
    }

    public override void OnEnter() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.MOVE_UP, true);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.MOVE_UP, true);
        playerController.MovePlayer(playerController.coordinates.top.position);
    }

    public override AbstractState UpdateState() {

        if (!playerController.isMoving) {
            if (playerController.gunModel.PullTriggerReady()) {
                return new FireTopState(playerController);
            } else {
                return new IdleTopState(currentAction, playerController);
            }
        }
        return null;
    }

    public override void OnExit() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.MOVE_UP, false);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.MOVE_UP, false);
    }
}
