using System;
using UnityEngine;

/// <summary>
/// Defines all events used in the game to notify the different systems when something happens
/// </summary>
public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake() {
        current = this;
    }

    public event Action onPickOreTriggerEnter;
    public event Action onSelectLevel;
    public event Action<int> onLevelLoad;
    public event Action onLevelPassed;
    public event Action onLevelFailed;
    public event Action onLevelReset;

    /// <summary>
    /// (Event) Character cube picks ore
    /// </summary>
    public void PickOreTriggerEnter() {
        if (onPickOreTriggerEnter != null) {
            onPickOreTriggerEnter();
        }
    }

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
        if (onLevelPassed != null) {
            onLevelPassed();
        }
    }

    /// <summary>
    /// (Event) The current playing level is reseted
    /// </summary>
    public void LevelReset() {
        if (onLevelPassed != null) {
            onLevelPassed();
        }
    }



}