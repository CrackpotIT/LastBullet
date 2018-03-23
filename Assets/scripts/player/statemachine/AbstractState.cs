using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState {

    public PlayerController playerController;

    public enum ACTION {NA, TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, RELOAD };
    public ACTION currentAction = ACTION.NA;


    public AbstractState(PlayerController playerController) {
        this.playerController = playerController;
    }

    public abstract void OnEnter();    
    public abstract void OnExit();

    public virtual AbstractState UpdateState() {
        // do nothing
        return null;
    }

    public virtual void HandleEvent(ACTION action) {
        currentAction = action;
    }

    public virtual void HandleAnimEvent(string parameter) {
        // do nothing
    }

    public void UpdateDirectionX() {
        if (currentAction == ACTION.TOP_LEFT || currentAction == ACTION.BOTTOM_LEFT) {
            playerController.SetDirectionX(-1);
        }
        if (currentAction == ACTION.TOP_RIGHT || currentAction == ACTION.BOTTOM_RIGHT) {
            playerController.SetDirectionX(1);
        }
    }
}
