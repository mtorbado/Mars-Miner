using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class PlayButtonScript : MonoBehaviour {

    bool isCoroutineStarted;
    Button button;

    private void Start() {
        button = GetComponent<Button>();
        GameEvents.current.onNextLevelLoad += ReEnableButton;
        GameEvents.current.onRestartLevel += ReEnableButton;
        GameEvents.current.onLevelLoad += ReEnableButton2;
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
    private void ReEnableButton() {
        GetComponentInChildren<Text>().text = "Play";
        button.interactable = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"> not used </param>
    private void ReEnableButton2(int n) {
        ReEnableButton();
    }
}
