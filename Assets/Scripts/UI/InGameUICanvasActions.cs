using UnityEngine;

public class InGameUICanvasActions : MonoBehaviour {
    
    private void Start() {
        GameEvents.current.onLevelLoad += ShowInGameUI;
        this.gameObject.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>
    /// Sets the game UI pannels active
    /// </summary>
    /// <param name="levelNumber"> not used </param>
    private void ShowInGameUI(int levelNumber) {
        this.gameObject.GetComponent<Canvas>().enabled = true;
    }
}
