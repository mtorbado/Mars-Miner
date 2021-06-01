using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelFinishedCanvasActions : MonoBehaviour {
    
    public float displayPosition, closedPosition;

    private GameObject levelPassedPanel;
    private GameObject levelFailedPanel;
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
        levelPassedPanel = gameObject.transform.Find("LevelPassedPanel").gameObject;
        levelFailedPanel = gameObject.transform.Find("LevelFailedPanel").gameObject;
    }

    /* ============================================================ EVENT CALL METHODS ============================================================ */

    private void ShowLevelFailedPanel() {
        background.SetActive(true);
        LeanTween.moveY(levelFailedPanel.GetComponent<RectTransform>(), displayPosition, 0.2f);
    }

    private void ShowLevelPassedPanel() {

        // levelPassedPanel.transform.Find("NextLevelButton").gameObject.SetActive(!levelLoader.IsLastLevel());
    
        levelPassedPanel.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>().SetText(scoreManager.LevelPoints() + " puntos");
        levelPassedPanel.transform.Find("AttemptsText").GetComponent<TextMeshProUGUI>().SetText(scoreManager.LevelAttempts() + " intentos");

        background.SetActive(true);
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), displayPosition, 0.2f);
    }

    /* ============================================================== BUTTON METHODS ============================================================== */

    public void LoadNextLevel() {
        background.SetActive(false);
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), closedPosition, 0.1f);
        levelLoader.LoadNextLevel();
    }

    public void LoadRandomLevel() {
        background.SetActive(false);
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), closedPosition, 0.1f);
        LeanTween.moveY(levelFailedPanel.GetComponent<RectTransform>(), closedPosition, 0.1f);
        levelLoader.LoadRandomLevel();
    }

    public void SelectLevel() {
        background.SetActive(false);
        LeanTween.moveY(levelPassedPanel.GetComponent<RectTransform>(), closedPosition, 0.1f);
        LeanTween.moveY(levelFailedPanel.GetComponent<RectTransform>(), closedPosition, 0.1f);
        GameEvents.current.SelectLevel();
    }

    public void RestartLevel() {
        background.SetActive(false);
        levelLoader.RestartLevel();
        LeanTween.moveY(levelFailedPanel.GetComponent<RectTransform>(), closedPosition, 0.1f);
    }

    public void GoToMainMenu() {
        GameEvents.current.ExitGame();
        SceneManager.LoadScene("Start Menu", LoadSceneMode.Single);
    }
}