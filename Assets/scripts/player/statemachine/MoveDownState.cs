using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownState : AbstractState {

    public MoveDownState(ACTION action, PlayerController playerController) : base(playerController) {
        currentAction = action;
    }

    public override void OnEnter() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.MOVE_DOWN, true);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.MOVE_DOWN, true);
        playerController.MovePlayer(playerController.coordinates.bottom.position);
    }

    public override AbstractState UpdateState() {

        if (!playerController.isMoving) {
            if (playerController.gunModel.PullTriggerReady()) {
                return new FireBottomState(playerController);
            } else {
                return new IdleBottomState(currentAction, playerController);
            }
        }
        return null;
    }


    public override void OnExit() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.MOVE_DOWN, false);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.MOVE_DOWN, false);
    }
}
