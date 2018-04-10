using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBottomState: AbstractState {

    private float time;

    public ReloadBottomState(PlayerController playerController) : base(playerController) {
    }

    public override void OnEnter() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.RELOAD_BOTTOM, true);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.RELOAD_BOTTOM, true);
        playerController.gunModel.Reload();
        AnimationClip clipPlayer = playerController.playerModel.GetAnimationClip(PlayerModel.CLIP_RELOAD_BOTTOM);
        float reloadTime = playerController.gunModel.gunStruct.timeToReload;
        float reloadSpeedModifier = clipPlayer.length / reloadTime;
        Debug.Log("ReloadSpeed Mod:" + reloadSpeedModifier);
        playerController.playerModel.animator.speed = reloadSpeedModifier;
        playerController.gunModel.animator.speed = reloadSpeedModifier;

        time = Time.time;
    }

    public override AbstractState UpdateState() {

        if (playerController.gunModel.IsReloading()) {
            return null;
        } else {
            return new IdleBottomState(ACTION.NA, playerController);
        }

    }

    public override void OnExit() {
        playerController.playerModel.animator.speed = 1;
        playerController.gunModel.animator.speed = 1;

        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.RELOAD_BOTTOM, false);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.RELOAD_BOTTOM, false);
    }

    public override void HandleAnimEvent(string parameter) {
        if (parameter == "CLIP_OUT") {
            SoundManager.PlaySFX(playerController.gunModel.gunStruct.gunReloadOutSound);
        }
        if (parameter == "CLIP_INSERT") {
            SoundManager.PlaySFX(playerController.gunModel.gunStruct.gunReloadInsertSound);
        }
        if (parameter == "CLIP_IN") {
            SoundManager.PlaySFX(playerController.gunModel.gunStruct.gunReloadInSound);
        }
        if (parameter == "CLIP_SNAP") {
            SoundManager.PlaySFX(playerController.gunModel.gunStruct.gunReloadSnapSound);
        }
        Debug.Log("Reload Anim Duration:" + (Time.time - time));
    }
}
