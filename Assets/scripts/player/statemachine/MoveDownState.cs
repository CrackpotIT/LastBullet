using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDownState : AbstractState {

    public MoveDownState(ACTION action) {
        currentAction = action;
    }

    public override void OnEnter(PlayerController playerController, Animator animator) {
        Debug.Log("MoveDownState OnEnter");
        
        animator.SetBool(MOVE_DOWN, true);
        playerController.currentDirectionY = 0;
        playerController.MovePlayer(playerController.coordinates.bottom.position);
    }

    public override AbstractState UpdateState(PlayerController playerController, Animator animator) {

        if (!playerController.isMoving) {
            if (playerController.gun.IsReady()) {
                return new FireBottomState();
            } else {
                return new IdleBottomState(currentAction);
            }
        }
        return null;
    }


    public override void OnExit(PlayerController playerController, Animator animator) {
        animator.SetBool(MOVE_DOWN, false);
    }
}
