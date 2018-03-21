using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpState : AbstractState {

    public MoveUpState(ACTION action) {
        currentAction = action;
    }

    public override void OnEnter(PlayerController playerController, Animator animator) {
        Debug.Log("MoveUpState OnEnter");
        
        animator.SetBool(MOVE_UP, true);
        playerController.currentDirectionY = 0;
        playerController.MovePlayer(playerController.coordinates.top.position);
    }

    public override AbstractState UpdateState(PlayerController playerController, Animator animator) {

        if (!playerController.isMoving) {
            if (playerController.gun.IsReady()) {
                return new FireTopState();
            } else {
                return new IdleTopState(currentAction);
            }
        }
        return null;
    }

    public override void OnExit(PlayerController playerController, Animator animator) {
        animator.SetBool(MOVE_UP, false);
    }
}
