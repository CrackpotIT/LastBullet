using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadTopState: AbstractReloadState {
    
    private bool isReloading;

    public ReloadTopState(PlayerController playerController) : base(playerController) {
    }

    public override AnimationClip GetPlayerAnimationClip() {
        return playerController.playerModel.GetAnimationClip(PlayerModel.CLIP_RELOAD_TOP);
    }

    public override void OnEnter() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.RELOAD_TOP, true);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.RELOAD_TOP, true);
        playerController.gunModel.Reload();
        SetReloadSpeed(1);        
        isReloading = true;

        playerController.reloadGameController.StartReloadGame(this);
    }

    public override AbstractState UpdateState() {

        if (isReloading) {
            return null;
        } else {
            return new IdleTopState(ACTION.NA, playerController);
        }
        
    }

    public override void OnExit() {
        playerController.playerModel.animator.speed = 1;
        playerController.gunModel.animator.speed = 1;

        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.RELOAD_TOP, false);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.RELOAD_TOP, false);
        playerController.gunModel.ReloadFinished();

        playerController.reloadGameController.EndReloadGame();
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
        if (parameter == "END") {
            isReloading = false;
        }
    }
}
