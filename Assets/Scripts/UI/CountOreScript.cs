using UnityEngine;
using TMPro;

/// <summary>
/// Script to control the display of ores collected
/// </summary>
public class CountOreScript : MonoBehaviour
{

    private int oreCount = 0;
    private int oreGoal = 0;

    void Start() {
        GameEvents.current.onPickOreTriggerEnter += OnPickOre;
        GameEvents.current.onSetOreGoal += SetOreGoal;
        GameEvents.current.onLevelLoad += ResetCount;
        GameEvents.current.onRandomLevelLoad += ResetCount;
        GameEvents.current.onNextLevelLoad += ResetCount;
    }

    /// <summary>
    /// Sets the ore goal for the current playing level
    /// </summary>
    private void SetOreGoal () {
        oreGoal =  GameObject.FindGameObjectWithTag("CharacterCube").GetComponent<AbsLevel>().oreGoal;
        gameObject.GetComponent<TextMeshProUGUI>().SetText(0 + " / " + oreGoal);
    }

    /// <summary>
    /// Counts a picked ore and checks if the ore goal is reached
    /// </summary>
    private void OnPickOre() {
        oreCount ++;
        gameObject.GetComponent<TextMeshProUGUI>().SetText(oreCount + " / " + oreGoal);
    }

    /// <summary>
    /// Resets the ore counter each time a level is loaded
    /// </summary>
    /// <param name="n"> ignore parameter </param>
    private void ResetCount(LevelDificulty ld, int n) {
        oreCount = 0;
    }

    /// <summary>
    /// Resets the ore counter each time a level is loaded
    /// </summary>
    private void ResetCount() {
        oreCount = 0;
    }
}
