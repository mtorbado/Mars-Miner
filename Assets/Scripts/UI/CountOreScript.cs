using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountOreScript : MonoBehaviour
{

    private int currentCount = 0;
    private int limitCount = 0;

    public void setOreLimit(int limitCount) {
        this.limitCount = limitCount;
    }

    void Start() {
        GameEvents.current.onPickOreTriggerEnter += OnPickOre;
    }

    private void OnPickOre() {
        currentCount ++;
        gameObject.GetComponent<TextMeshProUGUI>().SetText(currentCount + " / " + limitCount);
    }

}
