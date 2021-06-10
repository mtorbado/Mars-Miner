using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class LevelFinishedCanvasActions : MonoBehaviour {
    
    public float displayPosition1, closedPosition1; // for levelPassedPanel and levelFailedPanel
    public float displayPosition2, closedPosition2; // for nextDificultyPanel 

    public GameObject levelPassedPanel, levelFailedPanel, nextDificultyPanel;

    private GameObject background;
    private ScoreManager scoreManager;
    private LevelLoader levelLoader;
    private AudioManager audioManager;
    private TimerScript timerScript;

    private void Start() {
        GameEvents.current.onLevelFailed += ShowLevelFailedPanel;
        GameEvents.current.onLevelPassed += ShowLevelPassedPanel;

        scoreManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<ScoreManager>();
        levelLoader = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelLoader>();
        timerScript = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();

        background = gameObject.transform.Find("Background").gameObject;
        background.SetActive(false);

        audioManager = FindObjectOfType<AudioManager>();
    }

    /* ============================================================ EVENT CALL METHODS ============================================================ */

    private void ShowLevelFailedPanel() {
        StartCoroutine(LevelFailedCorroutine());
    }

    private void ShowLevelPassedPanel() {
        // levelPassedPanel.transform.Find("NextLevelButton").gameObject.SetActive(!levelLoader.IsLastLevel());
        levelPassedPanel.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().SetText(scoreManager.LevelPoints() + " puntos");
        levelPassedPanel.transform.Find("AttemptsText").GetComponent<TextMeshProUGUI>().SetText(scoreManager.LevelAttempts() + " intentos");
        levelPassedPanel.transform.Find("TimeText").GetComponent<TextMeshProUGUI>().SetText(timerScript.GetTime());
        StartCoroutine(LevelPassedCorroutine());
    }

    private IEnumerator LevelPassedCorroutine() {
        yield return new WaitForSeconds(0.5f);
        background.SetActive(true);
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), displayPosition1, 0.2f);

        if (levelLoader.playingDificulty != LevelDificulty.Challenge && levelLoader.IsNextDificultyUnlocked()) {
            LeanTween.moveY(nextDificultyPanel.GetComponent<RectTransform>(), displayPosition2, 0.2f);
        }
        
    }

    private IEnumerator LevelFailedCorroutine() {
        yield return new WaitForSeconds(2);
        background.SetActive(true);
        LeanTween.moveY(levelFailedPanel.GetComponent<RectTransform>(), displayPosition1, 0.2f);
    }

    /* ============================================================== BUTTON METHODS ============================================================== */

    public void LoadNextLevel() {
        ClosePanels();
        levelLoader.LoadNextLevel();
    }

    public void LoadRandomLevel() {
        ClosePanels();
        levelLoader.LoadRandomLevel();
    }

    public void SelectLevel() {
        ClosePanels();
        GameEvents.current.SelectLevel();
    }

    public void RestartLevel() {
        ClosePanels();
        levelLoader.RestartLevel();
    }

    public void PlayNextDificulty() {
        ClosePanels();
        levelLoader.LoadNextDificulty();
    }

    private void ClosePanels() {
        audioManager.Play("beep");
        background.SetActive(false);
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), closedPosition1, 0.1f);
        LeanTween.moveY(levelFailedPanel.GetComponent<RectTransform>(), closedPosition1, 0.1f);
        LeanTween.moveY(nextDificultyPanel.GetComponent<RectTransform>(), closedPosition2, 0.1f);
    }
}