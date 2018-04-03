using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : AbstractModel {



    public override void SetAnimatorBool(ANIM_PARAMS parameter, bool value) {
        base.SetAnimatorBool(parameter, value);
        // Sorting Order veraendern damit Patronenhuelsen oben im Hintergrund bleiben wenn man nach unten bewegt
        if (parameter == ANIM_PARAMS.MOVE_DOWN && value) {
            spriteRenderer.sortingOrder++;
        }
        if (parameter == ANIM_PARAMS.MOVE_UP && value) {
            spriteRenderer.sortingOrder--;
        }
    }
}
