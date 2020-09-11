﻿using UnityEngine;

public class InGameUICanvasActions : MonoBehaviour {
    
    private void Start() {
        GameEvents.current.onLevelLoad += ShowInGameUI;
        GameEvents.current.onSelectLevel += HideInGameUI;
        GameEvents.current.onLevelFailed += HideInGameUI;
        GameEvents.current.onLevelPassed += HideInGameUI;
        this.gameObject.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>
    /// Sets the game UI pannels active
    /// </summary>
    /// <param name="levelNumber"> not used </param>
    private void ShowInGameUI(int levelNumber) {
        this.gameObject.GetComponent<Canvas>().enabled = true;
    }

    private void HideInGameUI() {
        this.gameObject.GetComponent<Canvas>().enabled = false;
    }
}
