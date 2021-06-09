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
        GameEvents.current.onLevelLoad += ResetCount;
    }

    /// <summary>
    /// Sets the ore goal for the current playing level
    /// </summary>
    public void SetOreGoal(int oreGoal) {
        this.oreGoal = oreGoal;
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
    private void ResetCount() {
        oreCount = 0;
    }
}
