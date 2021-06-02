using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelFinishedCanvasActions : MonoBehaviour {
    
    public float displayPosition1, closedPosition1; // for levelPassedPanel and levelFailedPanel
    public float displayPosition2, closedPosition2; // for nextDificultyPanel 

    public GameObject levelPassedPanel, levelFailedPanel, nextDificultyPanel;

    private GameObject background;
    private ScoreManager scoreManager;
    private LevelLoader levelLoader;

    private void Start() {
        GameEvents.current.onLevelFailed += ShowLevelFailedPanel;
        GameEvents.current.onLevelPassed += ShowLevelPassedPanel;

        scoreManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<ScoreManager>();
        levelLoader = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelLoader>();

        background = gameObject.transform.Find("Background").gameObject;
        background.SetActive(false);
    }

    /* ============================================================ EVENT CALL METHODS ============================================================ */

    private void ShowLevelFailedPanel() {
        background.SetActive(true);
        LeanTween.moveY(levelFailedPanel.GetComponent<RectTransform>(), displayPosition1, 0.2f);
    }

    private void ShowLevelPassedPanel() {

        // levelPassedPanel.transform.Find("NextLevelButton").gameObject.SetActive(!levelLoader.IsLastLevel());
    
        levelPassedPanel.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().SetText(scoreManager.LevelPoints() + " puntos");
        levelPassedPanel.transform.Find("AttemptsText").GetComponent<TextMeshProUGUI>().SetText(scoreManager.LevelAttempts() + " intentos");

        background.SetActive(true);
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), displayPosition1, 0.2f);

        if (levelLoader.playingDificulty != LevelDificulty.Challenge && levelLoader.IsNextDificultyUnlocked()) {
            LeanTween.moveY(nextDificultyPanel.GetComponent<RectTransform>(), displayPosition2, 0.2f);
        }
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

    public void GoToMainMenu() {
        GameEvents.current.ExitGame();
        SceneManager.LoadScene("Start Menu", LoadSceneMode.Single);
    }

    public void PlayNextDificulty() {
        ClosePanels();
        levelLoader.LoadNextDificulty();
    }

    private void ClosePanels() {
        background.SetActive(false);
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), closedPosition1, 0.1f);
        LeanTween.moveY(levelFailedPanel.GetComponent<RectTransform>(), closedPosition1, 0.1f);
        LeanTween.moveY(nextDificultyPanel.GetComponent<RectTransform>(), closedPosition2, 0.1f);
    }
}