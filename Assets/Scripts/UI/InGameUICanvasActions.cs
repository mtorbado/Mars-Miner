using System.IO;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the UI elements displayed while playing a level
/// </summary>
public class InGameUICanvasActions : MonoBehaviour {

    public GameObject codeHolder;
    public GameObject docHolder;
    private static int? lastLoadedLevel;
    
    private void Start() {
        LoadHooverDoc();
        GameEvents.current.onLevelLoad += LoadLevel;
        GameEvents.current.onNextLevelLoad += LoadNextLevel;
        GameEvents.current.onRestartLevel += ShowInGameUI;
        GameEvents.current.onSelectLevel += HideInGameUI;
        GameEvents.current.onLevelFailed += HideInGameUI;
        GameEvents.current.onLevelPassed += HideInGameUI;
        this.gameObject.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>
    /// Loads the code documentation file into de HooverDoc panel
    /// </summary>
    private void LoadHooverDoc() {
        TextAsset txt = (TextAsset)Resources.Load("hoover_doc");
        docHolder.GetComponent<TextMeshProUGUI>().SetText(txt.text);
    }

    /// <summary>
    /// Sets the game UI pannels for the given level
    /// </summary>
    /// <param name="levelNumber"> number of the loaded level </param>
    private void LoadLevel(int levelNumber) {
        TextAsset txt=(TextAsset)Resources.Load("Level Code/level_" + levelNumber);
        codeHolder.GetComponent<TextMeshProUGUI>().SetText(txt.text);
        ShowInGameUI();
        lastLoadedLevel = levelNumber;
    }

    /// <summary>
    /// Sets the game UI pannels for the next level
    /// </summary>
    private void LoadNextLevel() {
        LoadLevel((int)lastLoadedLevel +1);
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
