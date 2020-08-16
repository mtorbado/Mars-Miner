using UnityEngine;

/// <summary>
/// 
/// </summary>
public class CanvasTweening : MonoBehaviour {

    public GameObject panel;
    public float ClosedPosition, DisplayPosition;
    public bool startsDisplayed;

    private bool isDisplayed;

    void Awake() {
        isDisplayed = startsDisplayed;
    }

    /// <summary>
    /// 
    /// </summary>
    public void AlternatePanel() {
        if (!isDisplayed) LeanTween.moveX(panel.GetComponent<RectTransform>(), DisplayPosition, 0.2f);
        else LeanTween.moveX(panel.GetComponent<RectTransform>(), ClosedPosition, 0.2f);
        
        isDisplayed = !isDisplayed;
    }
}
