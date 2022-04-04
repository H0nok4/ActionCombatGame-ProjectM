using System.Collections;
using System.Collections.Generic;
using PlotSystem;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {
    public GameObject MainCanvas;
    public GameState GameState;

    private void Update() {
        if (GameState == GameState.PlayScenario && GameDirecter.instance != null) {
            GameDirecter.instance.HandleUpdate();
        }
    }
}

public enum GameState {
    PlayScenario,
    Battle
}