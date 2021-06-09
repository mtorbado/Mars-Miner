using UnityEngine;
using TMPro;

public class CountOreScript : MonoBehaviour
{

    private int oreCount = 0;
    private int oreGoal = 0;

    void Start() {
        GameEvents.current.onPickOreTriggerEnter += OnPickOre;
        GameEvents.current.onLevelLoad += ResetCount;
    }

    public void SetOreGoal(int oreGoal) {
        this.oreGoal = oreGoal;
        gameObject.GetComponent<TextMeshProUGUI>().SetText(0 + " / " + oreGoal);
    }

    private void OnPickOre() {
        oreCount ++;
        gameObject.GetComponent<TextMeshProUGUI>().SetText(oreCount + " / " + oreGoal);
    }

    private void ResetCount() {
        oreCount = 0;
    }
}
