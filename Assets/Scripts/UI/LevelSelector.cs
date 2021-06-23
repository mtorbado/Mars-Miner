using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    ScoreManager scoreManager;
    LevelLoader levelLoader;
    AudioManager audioManager;

    public GameObject[] dificultyButtons;

    private void Start() {

        GameEvents.current.onSelectLevel += ShowLevelSelection;
        GameEvents.current.onUpdateScores += UpdateScores;

        scoreManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<ScoreManager>();
        levelLoader = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelLoader>();

        audioManager = FindObjectOfType<AudioManager>();
    }

    public void PlayEasy() {
        audioManager.Play("beep");
        if (scoreManager.finalScore.easyPoints == 0) {
            levelLoader.LoadLevel(LevelDificulty.Easy, 0);
            GameEvents.current.DisableAllForTutorial();
        }
        else {
            levelLoader.LoadRandomLevel(LevelDificulty.Easy);
        }
        transform.GetComponent<Canvas>().enabled = false;
    }

    public void PlayMedium() {
        audioManager.Play("beep");
        levelLoader.LoadRandomLevel(LevelDificulty.Medium);
        transform.GetComponent<Canvas>().enabled = false;
    }

    public void PlayHard() {
        audioManager.Play("beep");
        levelLoader.LoadRandomLevel(LevelDificulty.Hard);
        transform.GetComponent<Canvas>().enabled = false;
    }

    public void PlayChallenge() {
        audioManager.Play("beep");
        levelLoader.LoadRandomLevel(LevelDificulty.Challenge);
        transform.GetComponent<Canvas>().enabled = false;
    }
    
    public void QuitGame() {
        audioManager.Stop("music");
        scoreManager.SaveGlobalData();
        #pragma warning disable CS0618
        Application.ExternalCall ("salir");
        #pragma warning restore CS0618
        Application.Quit();
        Debug.Log("Game was closed");
    }

    private void ShowLevelSelection() {
        transform.GetComponent<Canvas>().enabled = true;
    }

    private void UpdateScores() {
        int[] scoreArray = scoreManager.finalScore.Array();
        int[] maxArray = (int[])Enum.GetValues(typeof(LevelDificulty));
        for (int i = 0; i < dificultyButtons.Length; i++) {
            if (i == 0 || scoreArray[i-1] >= ScoreManager.PASS_DIFICULTY_SCORE) {
                dificultyButtons[i].GetComponent<Button>().interactable = true;
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(scoreArray[i] + "/" + maxArray[i]);
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(255,255,255,255);
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = 20;
            }
            else {
                dificultyButtons[i].GetComponent<Button>().interactable = false;
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("BLOQUEADO");
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(194,78,82,255);
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = 15;
            }
        }
    }

}
