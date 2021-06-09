using System.Collections;
using System.Linq;
using UnityEngine;

public abstract class AbsLevel : MonoBehaviour, ILevel {

    public int oreGoal;
    public int oreCount;
    public int numOfAttempts;

    private bool failLevel;
    
    public RobotActions robotActions;

    private void Start() {
        Initialize();
        failLevel = false;
        numOfAttempts = 0;
        GameEvents.current.onPickOreTriggerEnter += PickOre;
        GameEvents.current.onLevelFailed += FailLevel;
    }

    public abstract void Initialize();
    
    public abstract IEnumerator Play(string [] args);

    public void PickOre() {
        this.oreCount++;
    }

    public void FailLevel() {
        failLevel = true;
    }

    public bool CheckLevelPassed() {
        if (oreCount >= oreGoal) {
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
