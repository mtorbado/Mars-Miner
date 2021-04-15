using System;
using UnityEngine;

/// <summary>
/// Defines all events used in the game to notify the different systems when something happens
/// </summary>
public class GameEvents : MonoBehaviour {
    public static GameEvents current;

    private void Awake() {
        current = this;
    }

    
    public event Action onSelectLevel;
    public event Action<int> onLevelLoad;
    public event Action onRestartLevel;
    public event Action onNextLevelLoad;
    public event Action onSetOreGoal;

    public event Action onPlayLevel;
    public event Action onPickOreTriggerEnter;
    public event Action onRockCollision;
    
    
    public event Action onLevelPassed;
    public event Action onLevelFailed;
    
    public event Action onDisableAllForTutorial;
    public event Action onEnableAllAfterTutorial;


    /* ======================================================== SELECTING/LOADING LEVEL ======================================================== */

    /// <summary>
    /// (Event) Load the level selection menu
    /// </summary>
    public void SelectLevel() {
        if (onSelectLevel != null) {
            onSelectLevel();
        }
    }

    /// <summary>
    /// (Event) A level is selected in the level selection menu
    /// </summary>
    /// <param name="levelNumber"> level to load </param>
    public void LoadLevel(int levelNumber) {
        if (onLevelLoad !=null) {
            onLevelLoad(levelNumber);
        }
    }

    /// <summary>
    /// (Event) The current playing level is reseted
    /// </summary>
    public void RestartLevel() {
        if (onRestartLevel != null) {
            onRestartLevel();
        }
    }

    /// <summary>
    /// (Event) The next level is loaded
    /// </summary>
    public void LoadNextLevel() {
        if (onNextLevelLoad != null) {
            onNextLevelLoad();
        }
    }

    /// <summary>
    /// (Event) The ore goal is set for the level
    /// </summary>
    public void SetOreGoal() {
        if (onSetOreGoal != null) {
            onSetOreGoal();
        }
    }

    /* ============================================================== GAME LOOP ============================================================== */

    /// <summary>
    /// (Event) The current level started to play (attempt)
    /// </summary>
    public void PlayLevel() {
        if (onPlayLevel != null) {
            onPlayLevel();
        }
    }

    /// <summary>
    /// (Event) Character cube picks ore
    /// </summary>
    public void PickOreTriggerEnter() {
        if (onPickOreTriggerEnter != null) {
            onPickOreTriggerEnter();
        }
    }

    /// <summary>
    /// (Event) The robot collided with something
    /// </summary>
    public void RockCollision() {
        if (onRockCollision != null) {
            onRockCollision();
        }
    }

    /* ============================================================ LEVEL FINISHED ============================================================ */

    /// <summary>
    /// (Event) The current playing level is completed
    /// </summary>
    public void LevelPassed() {
        if (onLevelPassed != null) {
            onLevelPassed();
        }
    }

    /// <summary>
    /// (Event) The current playing level is failed
    /// </summary>
    public void LevelFailed() {
        if (onLevelFailed != null) {
            onLevelFailed();
        }
    }

    

    /* ============================================================ TUTORIAL LEVEL ============================================================ */
    
    /// <summary>
    /// (Event) Disables all the interactable elements of the game screen for the tutorial
    /// </summary>
    public void DisableAllForTutorial() {
        if (onDisableAllForTutorial != null) {
            onDisableAllForTutorial();
        }
    }

    /// <summary>
    /// (Event) Disables all the interactable elements of the game screen for the tutorial
    /// </summary>
    public void EnableAllAfterTutorial() {
        if (onEnableAllAfterTutorial != null) {
            onEnableAllAfterTutorial();
        }
    }
}