using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState {

    public static string IDLE_TOP = "IDLE_TOP";
    public static string IDLE_BOTTOM = "IDLE_BOTTOM";
    public static string MOVE_UP = "MOVE_UP";
    public static string MOVE_DOWN = "MOVE_DOWN";
    public static string FIRE_TOP = "FIRE_TOP";
    public static string FIRE_BOTTOM = "FIRE_BOTTOM";
    public static string RELOAD = "RELOAD";

    public enum ACTION {NA, TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT, RELOAD };
    public ACTION currentAction = ACTION.NA;

    public abstract void OnEnter(PlayerController playerController, Animator animator);    
    public abstract void OnExit(PlayerController playerController, Animator animator);

    public virtual AbstractState UpdateState(PlayerController playerController, Animator animator) {
        // do nothing
        return null;
    }

    public virtual void HandleEvent(ACTION action) {
        currentAction = action;
    }

    public virtual void HandleAnimEvent(string parameter, PlayerController playerController, Animator animator) {
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
