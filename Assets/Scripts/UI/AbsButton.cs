using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Abstract class to define game levels
/// </summary>
public abstract class AbsButton : MonoBehaviour {
    public Button button;
    public AudioManager audioManager;

    private void Start() {
        button = GetComponent<Button>();
        GameEvents.current.onDisableAllForTutorial += DisableButton;
        GameEvents.current.onEnableAllAfterTutorial += EnableButton;

        audioManager = FindObjectOfType<AudioManager>();
    }

    
    
    /// <summary>
    /// 
    /// </summary>
    private void DisableButton() {
        button.interactable = false;
    }

     /// <summary>
    /// 
    /// </summary>
    private void EnableButton() {
        button.interactable = true;
    }
}
