using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadGameState: AbstractState {

    private bool top;
    private bool gameInProgress = false;
    private bool gameWon = false;

    public ReloadGameState(bool top, PlayerController playerController) : base(playerController) {
        this.top = top;
    }

    public override void OnEnter() {
        gameInProgress = true;
        gameWon = false;
        playerController.reloadGameController.StartReloadGame(this);
    }

    public override AbstractState UpdateState() {

        if (gameInProgress) {
            return null;
        } else {
            if (top) {
                return new ReloadTopState(gameWon, playerController);
            } else {
                return new ReloadBottomState(gameWon, playerController);
            }
        }
    }

    public void GameOver(bool won) {
        gameInProgress = false;
        gameWon = won;
    }

    public override void OnExit() {
        gameInProgress = false;
    }

}
