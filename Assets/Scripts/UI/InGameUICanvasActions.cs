using System.IO;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the UI elements displayed while playing a level
/// </summary>
public class InGameUICanvasActions : MonoBehaviour {

    public GameObject codeHolder;
    public GameObject docHolder;
    
    private void Start() {
        LoadHooverDoc();
        GameEvents.current.onLevelLoad += ShowInGameUI;
        GameEvents.current.onNextLevelLoad += ShowInGameUI;
        GameEvents.current.onRandomLevelLoad += ShowInGameUI;
        GameEvents.current.onRestartLevel += ShowInGameUI;
        GameEvents.current.onSelectLevel += HideInGameUI;
        GameEvents.current.onLevelFailed += HideInGameUI;
        GameEvents.current.onLevelPassed += HideInGameUI;
        this.gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void LoadCode(TextAsset txt) {
        codeHolder.GetComponent<TextMeshProUGUI>().SetText(txt.text);
    }

    /// <summary>
    /// Loads the code documentation file into de HooverDoc panel
    /// </summary>
    private void LoadHooverDoc() {
        TextAsset txt = (TextAsset)Resources.Load("hoover_doc");
        docHolder.GetComponent<TextMeshProUGUI>().SetText(txt.text);
    }

    /// <summary>
    /// Sets the game UI pannels active
    /// </summary>
    /// <param name="n"> not used </param>
    /// /// <param name="ld"> not used </param>
    private void ShowInGameUI(LevelDificulty ld, int n) {
        this.gameObject.GetComponent<Canvas>().enabled = true;
    }

    /// <summary>
    /// Sets the game UI pannels active
    /// </summary>
    private void ShowInGameUI() {
        this.gameObject.GetComponent<Canvas>().enabled = true;
    }

    private void HideInGameUI() {
        this.gameObject.GetComponent<Canvas>().enabled = false;
    }
}
