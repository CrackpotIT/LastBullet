using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadTopState: AbstractState {

    private float time;
    private bool wonMiniGame;
    private bool isReloading;

    public ReloadTopState(bool wonMiniGame, PlayerController playerController) : base(playerController) {
        this.wonMiniGame = wonMiniGame;
    }

    public override void OnEnter() {
        playerController.playerModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.RELOAD_TOP, true);
        playerController.gunModel.SetAnimatorBool(AbstractModel.ANIM_PARAMS.RELOAD_TOP, true);
        playerController.gunModel.Reload();
        AnimationClip clipPlayer = playerController.playerModel.GetAnimationClip(PlayerModel.CLIP_RELOAD_TOP);
        float gunModelReloadTime = playerController.gunModel.gunStruct.timeToReload;
        float reloadTime = (wonMiniGame ? gunModelReloadTime / 2 : gunModelReloadTime);
        float reloadSpeedModifier = clipPlayer.length / reloadTime;
        Debug.Log("ReloadSpeed Mod:" + reloadSpeedModifier);
        playerController.playerModel.animator.speed = reloadSpeedModifier;
        playerController.gunModel.animator.speed = reloadSpeedModifier;

        isReloading = true;
        time = Time.time;
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
            isReloading = false;
        }
    }
}
