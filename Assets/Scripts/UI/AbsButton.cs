using UnityEngine;
using UnityEngine.UI;

public abstract class AbsButton : MonoBehaviour {
    public Button button;
    public AudioManager audioManager;

    private void Start() {
        button = GetComponent<Button>();
        GameEvents.current.onDisableAllForTutorial += DisableButton;
        GameEvents.current.onEnableAllAfterTutorial += EnableButton;

        audioManager = FindObjectOfType<AudioManager>();
    }

    private void DisableButton() {
        button.interactable = false;
    }

    private void EnableButton() {
        button.interactable = true;
    }
}
