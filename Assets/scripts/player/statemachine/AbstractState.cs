using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState {



    public enum ACTION {NA, TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, RELOAD };
    public ACTION currentAction = ACTION.NA;

    public abstract void OnEnter(PlayerController playerController, PlayerModel playerModel, GunModel gunModel);    
    public abstract void OnExit(PlayerController playerController, PlayerModel playerModel, GunModel gunModel);

    public virtual AbstractState UpdateState(PlayerController playerController, PlayerModel playerModel, GunModel gunModel) {
        // do nothing
        return null;
    }

    public virtual void HandleEvent(ACTION action) {
        currentAction = action;
    }

    public virtual void HandleAnimEvent(string parameter) {
        // do nothing
    }

    public void UpdateDirectionX(PlayerController playerController) {
        if (currentAction == ACTION.TOP_LEFT || currentAction == ACTION.BOTTOM_LEFT) {
            playerController.SetDirectionX(-1);
        }
        if (currentAction == ACTION.TOP_RIGHT || currentAction == ACTION.BOTTOM_RIGHT) {
            playerController.SetDirectionX(1);
        }
    }
}
