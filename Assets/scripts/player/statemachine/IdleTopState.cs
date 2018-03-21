using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTopState : AbstractState {

    public IdleTopState(ACTION action) {
        currentAction = action;
    }

    public override void OnEnter(PlayerController playerController, Animator animator) {
        Debug.Log("IdleTopState OnEnter");
        animator.SetBool(IDLE_TOP, true);
        playerController.currentDirectionY = -1;
    }

    public override AbstractState UpdateState(PlayerController playerController, Animator animator) {

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

    public override void OnExit(PlayerController playerController, Animator animator) {
        animator.SetBool(IDLE_TOP, false);
    }
}
