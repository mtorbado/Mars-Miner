﻿using UnityEngine;

//TODO: remove?
public class CanvasTweening : MonoBehaviour {

    public GameObject panel, button;
    public float ClosedPosition = 280f;
    public float DisplayPosition = 520f;
    private bool isDisplayed;

    void Awake() {
        isDisplayed = true;
        panel = gameObject.transform.Find("Panel").gameObject;
    }

    public void AlternatePanel() {
        if (isDisplayed) LeanTween.moveLocalX(panel, DisplayPosition, 0.2f);
        else LeanTween.moveLocalX(panel, ClosedPosition, 0.2f);
        
        isDisplayed = !isDisplayed;
    }
}
