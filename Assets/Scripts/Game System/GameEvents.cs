using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake() {
        current = this;
    }

    public event Action onSelectLevel;
    public event Action<int> onLevelLoad;
    public event Action onPickOreTriggerEnter;
    public event Action onLevelPassed;
    public event Action onLevelFailed;
    public event Action onLevelReset;

    public void PickOreTriggerEnter() {
        if (onPickOreTriggerEnter != null) {
            onPickOreTriggerEnter();
        }
    }

    public void SelectLevel() {
        onSelectLevel();
    }
    public void LoadLevel(int levelNumber) {
        onLevelLoad(levelNumber);
    }

    public void LevelPassed() {
        if (onLevelPassed != null) {
            onLevelPassed();
        }
    }

    public void LevelFailed() {
        if (onLevelPassed != null) {
            onLevelPassed();
        }
    }

    public void LevelReset() {
        if (onLevelPassed != null) {
            onLevelPassed();
        }
    }



}