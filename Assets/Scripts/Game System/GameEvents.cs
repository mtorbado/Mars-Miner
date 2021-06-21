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
    public event Action onLevelLoad;

    public event Action onPlayLevel;
    public event Action onPickOreTriggerEnter;
    public event Action onRockCollision;
    
    public event Action onLevelPassed;
    public event Action onLevelFailed;
    
    public event Action onDisableAllForTutorial;
    public event Action onEnableAllAfterTutorial;

    public event Action<string> onLoadGameData;
    public event Action onUpdateScores;
    public event Action onExitGame;

    /* ======================================================== SELECTING/LOADING LEVEL ======================================================== */

    /// <summary>
    /// (Event) Go to the level selection menu
    /// </summary>
    public void SelectLevel() {
        if (onSelectLevel != null) {
            onSelectLevel();
        }
    }

    /// <summary>
    /// (Event) A level is loaded and ready to be played
    /// </summary>
    public void LoadLevel() {
        if (onLevelLoad !=null) {
            onLevelLoad();
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

    /* ============================================================ PROGRESS SYSTEM ============================================================ */

    /// <summary>
    /// (Event) The game data from the external call has been received and can be loaded by the ScoreManager
    /// </summary>
    /// <param name="gameData"> GameData object in JSON string form</param>
    public void LoadGameData(String gameData) {
        if (onLoadGameData !=null) {
            onLoadGameData(gameData);
        }
    }

    /// <summary>
    /// (Event) The game data has been read and the displayed scores can be updated
    /// </summary>
    public void UpdateScores() {
        if (onUpdateScores !=null) {
            onUpdateScores();
        }
    }
}