using UnityEngine;

/// <summary>
/// 
/// </summary>
public class CanvasTweening : AbsButton {

    public GameObject panel;
    public float closedPosition, displayPosition;
    public bool startsDisplayed;

    private bool isDisplayed;

    void Awake() {
        isDisplayed = startsDisplayed;
    }

    /// <summary>
    /// 
    /// </summary>
    public void AlternatePanel() {
        if (!isDisplayed) LeanTween.moveX(panel.GetComponent<RectTransform>(), displayPosition, 0.2f);
        else LeanTween.moveX(panel.GetComponent<RectTransform>(), closedPosition, 0.2f);
        
        isDisplayed = !isDisplayed;
    }
}
