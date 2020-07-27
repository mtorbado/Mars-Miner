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
        oreGoal =  GameObject.FindGameObjectWithTag("CharacterCube").GetComponent<AbsLevel>().oreGoal;
        gameObject.GetComponent<TextMeshProUGUI>().SetText(oreCount + " / " + oreGoal);
        GameEvents.current.onPickOreTriggerEnter += OnPickOre;
    }

    private void OnPickOre() {
        oreCount ++;
        gameObject.GetComponent<TextMeshProUGUI>().SetText(oreCount + " / " + oreGoal);
    }

}
