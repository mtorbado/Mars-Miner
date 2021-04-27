using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class to define game levels
/// </summary>
public abstract class AbsLevel : MonoBehaviour, ILevel {

    public bool isTutorial;
    public int oreGoal;
    public int oreCount;
    public int numOfAttempts;
    public LevelDificulty dificulty;

    private bool failLevel;
    
    private void Start() {
        isTutorial = false;
        failLevel = false;
        numOfAttempts = 0;
        GameEvents.current.onPickOreTriggerEnter += PickOre;
        GameEvents.current.onLevelFailed += FailLevel;
    }   

    /// <summary>
    /// Abstract method to override with game loop for each level 
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator Play(string [] args);

    public void PickOre() {
        this.oreCount++;
    }

    public void FailLevel() {
        failLevel = true;
    }

    public bool CheckLevelPassed() {
        if (oreCount == oreGoal) {
            GameEvents.current.LevelPassed();
            return true;
        }
        return false;
    }

    public bool CheckLevelFailed() {
        return failLevel;
    }

    /* ============================================================ LEVEL IMPUT CONVERSIONS ============================================================= */

    public bool[] InputToBool(string[] input) {
        try {
            return input.Select(bool.Parse).ToArray();
        } catch {
            return null;
        }
    }

    public int[] InputToInt(string[] input) {
        try {
            return input.Select(int.Parse).ToArray();
        } catch {
            return null;
        }
    }
}
