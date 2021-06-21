using UnityEngine;

public class CanvasTweening : AbsButton {

    public GameObject panel;
    public float closedPosition, displayPosition;
    public bool startsDisplayed;

    private bool isDisplayed;

    void Awake() {
        GameEvents.current.onSelectLevel += ClosePanel;
        isDisplayed = startsDisplayed;
    }

    public void AlternatePanel() {
        if (!isDisplayed) LeanTween.moveX(panel.GetComponent<RectTransform>(), displayPosition, 0.2f);
        else LeanTween.moveX(panel.GetComponent<RectTransform>(), closedPosition, 0.2f);
        
        isDisplayed = !isDisplayed;
    }

    private void ClosePanel() {
        LeanTween.moveX(panel.GetComponent<RectTransform>(), closedPosition, 0.2f);
        isDisplayed = false;
    }
}
