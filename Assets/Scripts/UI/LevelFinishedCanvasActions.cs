using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinishedCanvasActions : MonoBehaviour {
    
    public float displayPosition, closedPosition;

    private GameObject levelPassedPanel;
    private GameObject levelFailedPanel;
    private GameObject background;

    private void Start() {
        GameEvents.current.onLevelFailed += ShowLevelFailedPanel;
        GameEvents.current.onLevelPassed += ShowLevelPassedPanel;

        background = gameObject.transform.Find("Background").gameObject;
        background.SetActive(false);
        levelPassedPanel = gameObject.transform.Find("LevelPassedPanel").gameObject;
        levelFailedPanel = gameObject.transform.Find("LevelFailedPanel").gameObject;
    }

    private void ShowLevelFailedPanel() {
        background.SetActive(true);
        LeanTween.moveY(levelFailedPanel.GetComponent<RectTransform>(), displayPosition, 0.2f);
    }

    private void ShowLevelPassedPanel() {
        background.SetActive(true);
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), displayPosition, 0.2f);
    }

    public void LoadNextLevel() {
        background.SetActive(false);
        GameEvents.current.LoadNextLevel();
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), closedPosition, 0.1f);
    }

    public void SelectLevel() {
        background.SetActive(false);
        GameEvents.current.SelectLevel();
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), closedPosition, 0.1f);
    }

    public void RestartLevel() {
        background.SetActive(false);
        GameEvents.current.RestartLevel();
        LeanTween.moveY(levelFailedPanel.GetComponent<RectTransform>(), closedPosition, 0.1f);
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("Start Menu", LoadSceneMode.Single);
    }

}