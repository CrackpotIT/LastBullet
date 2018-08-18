using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractReloadState : AbstractState{

    public AbstractReloadState(PlayerController playerController) : base(playerController){
    }

    public void SetReloadSpeed(float modifier) {
        float gunModelReloadTime = playerController.gunModel.gunStruct.timeToReload;
        float reloadSpeedModifier = GetPlayerAnimationClip().length / (gunModelReloadTime * modifier);
        Debug.Log("ReloadSpeed Mod:" + reloadSpeedModifier);
        playerController.playerModel.animator.speed = reloadSpeedModifier;
        playerController.gunModel.animator.speed = reloadSpeedModifier;
    }

    public abstract AnimationClip GetPlayerAnimationClip();

}
