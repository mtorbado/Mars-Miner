using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class PlayButtonScript : AbsButton {

    public GameObject programInput;

    private void Awake() {
        GameEvents.current.onLevelLoad += EnableButton;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Play() {
        
        audioManager.Play("beep");

        button.interactable = false;
        GetComponentInChildren<Text>().text = "¡Iniciado!";
        GameEvents.current.PlayLevel();
        
        GameObject[] characterCubes = GameObject.FindGameObjectsWithTag("CharacterCube");
        foreach (GameObject characterCube in characterCubes) {
            StartCoroutine(characterCube.GetComponent<ILevel>().Play(programInput.GetComponent<ProgramInputScript>().GetInputArrayStr()));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void EnableButton() {
        GetComponentInChildren<Text>().text = "Start";
        button.interactable = true;
    }
}
