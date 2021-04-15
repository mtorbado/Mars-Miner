using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class PlayButtonScript : AbsButton {

    bool isCoroutineStarted;

    private void Awake() {
        GameEvents.current.onNextLevelLoad += EnableButton;
        GameEvents.current.onRestartLevel += EnableButton;
        GameEvents.current.onLevelLoad += EnableButton2;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Play() {
        
        button.interactable = false;
        GetComponentInChildren<Text>().text = "Running!";
        GameEvents.current.PlayLevel();
        
        GameObject[] characterCubes = GameObject.FindGameObjectsWithTag("CharacterCube");
        foreach (GameObject characterCube in characterCubes) {
            StartCoroutine(characterCube.GetComponent<ILevel>().Play());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void EnableButton() {
        GetComponentInChildren<Text>().text = "Start";
        button.interactable = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"> not used </param>
    private void EnableButton2(int n) {
        EnableButton();
    }
}
