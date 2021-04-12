using System.IO;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the UI elements displayed while playing a level
/// </summary>
public class InGameUICanvasActions : MonoBehaviour {

    public GameObject codeHolder;
    private static int? lastLoadedLevel;

    const string LevelFolder = "LevelFiles";
    const string LevelFileNaming = "level_";
    
    private void Start() {
        GameEvents.current.onLevelLoad += LoadLevel;
        GameEvents.current.onNextLevelLoad += LoadNextLevel;
        GameEvents.current.onRestartLevel += ShowInGameUI;
        GameEvents.current.onSelectLevel += HideInGameUI;
        GameEvents.current.onLevelFailed += HideInGameUI;
        GameEvents.current.onLevelPassed += HideInGameUI;
        this.gameObject.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>
    /// Sets the game UI pannels for the given level
    /// </summary>
    /// <param name="levelNumber"> number of the loaded level </param>
    private void LoadLevel(int levelNumber) {
        StreamReader reader = new StreamReader(LevelFolder + "/" + LevelFileNaming + levelNumber + ".txt"); 
        codeHolder.GetComponent<TextMeshProUGUI>().SetText(reader.ReadToEnd());
        reader.Close();
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
