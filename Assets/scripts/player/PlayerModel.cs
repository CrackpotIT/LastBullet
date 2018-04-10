using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : AbstractModel {

    public static string CLIP_RELOAD_TOP = "PlayerReloadTop";
    public static string CLIP_RELOAD_BOTTOM = "PlayerReloadBottom";

    public override void SetAnimatorBool(ANIM_PARAMS parameter, bool value) {
        base.SetAnimatorBool(parameter, value);
        // Sorting Order veraendern damit Patronenhuelsen oben im Hintergrund bleiben wenn man nach unten bewegt
        if (parameter == ANIM_PARAMS.MOVE_DOWN && value) {
            spriteRenderer.sortingOrder = 2;
        }
        if (parameter == ANIM_PARAMS.MOVE_UP && value) {
            spriteRenderer.sortingOrder = 0;
        }
    }
}
