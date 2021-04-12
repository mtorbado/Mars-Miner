using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelFinishedCanvasActions : MonoBehaviour {
    
    public float displayPosition, closedPosition;

    private GameObject levelPassedPanel;
    private GameObject levelFailedPanel;
    private GameObject background;
    private ScoreManager scoreManager;

    private void Start() {
        GameEvents.current.onLevelFailed += ShowLevelFailedPanel;
        GameEvents.current.onLevelPassed += ShowLevelPassedPanel;

        scoreManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<ScoreManager>();

        background = gameObject.transform.Find("Background").gameObject;
        background.SetActive(false);
        levelPassedPanel = gameObject.transform.Find("LevelPassedPanel").gameObject;
        levelFailedPanel = gameObject.transform.Find("LevelFailedPanel").gameObject;
    }

    /* ============================================================ EVENT CALL METHODS ============================================================ */

    private void ShowLevelFailedPanel() {
        background.SetActive(true);
        LeanTween.moveY(levelFailedPanel.GetComponent<RectTransform>(), displayPosition, 0.2f);
    }

    private void ShowLevelPassedPanel() {

        if (LevelLoader.IsLastLevel()) {
            levelPassedPanel.transform.Find("NextLevelButton").gameObject.SetActive(false);
            //TODO: change panel text to inform that it's the last level?
        }
        
        levelPassedPanel.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().SetText(scoreManager.GetCurrentScore() + " puntos");
        levelPassedPanel.transform.Find("AttemptsText").GetComponent<TextMeshProUGUI>().SetText(scoreManager.GetCurrentAttempts() + " intentos");

        background.SetActive(true);
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), displayPosition, 0.2f);
    }

    /* ============================================================== BUTTON METHODS ============================================================== */

    public void LoadNextLevel() {
        background.SetActive(false);
        GameEvents.current.LoadNextLevel();
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), closedPosition, 0.1f);
    }

    public void SelectLevel() {
        background.SetActive(false);
        GameEvents.current.SelectLevel();
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), closedPosition, 0.1f);
        LeanTween.moveY(levelFailedPanel.GetComponent<RectTransform>(), closedPosition, 0.1f);
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