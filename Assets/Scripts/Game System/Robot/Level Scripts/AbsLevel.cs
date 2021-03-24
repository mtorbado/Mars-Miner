using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class to define game levels
/// </summary>
public abstract class AbsLevel : MonoBehaviour, ILevel {

    public int oreGoal;
    public int oreCount;
    public int numOfAttempts;
    public LevelDificulty dificulty;
    private bool failLevel;
    
    private void Start() {
        failLevel = false;
        numOfAttempts = 0;
        GameEvents.current.onPickOreTriggerEnter += PickOre;
        GameEvents.current.onLevelFailed += FailLevel;
    }   

    /// <summary>
    /// Abstract method to override with game loop for each level 
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator Play();

    public void PickOre() {
        this.oreCount++;
    }

    public void FailLevel() {
        failLevel = true;
    }

    public bool checkLevelPassed() {
        if (oreCount == oreGoal) {
            GameEvents.current.LevelPassed();
            return true;
        }
        return false;
    }

    public bool checkLevelFailed() {
        return failLevel;
    }
}
