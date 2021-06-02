using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class LevelSelector : MonoBehaviour {

    ScoreManager scoreManager;
    LevelLoader levelLoader;

    public GameObject[] dificultyButtons;

    private void Start() {

        GameEvents.current.onSelectLevel += ShowLevelSelection;
        GameEvents.current.onUpdateScores += UpdateScores;

        scoreManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<ScoreManager>();
        levelLoader = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelLoader>();
    }

    public void PlayEasy() {
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
        levelLoader.LoadRandomLevel(LevelDificulty.Medium);
        transform.GetComponent<Canvas>().enabled = false;
    }

    public void PlayHard() {
        levelLoader.LoadRandomLevel(LevelDificulty.Hard);
        transform.GetComponent<Canvas>().enabled = false;
    }

    public void PlayChallenge() {
        levelLoader.LoadRandomLevel(LevelDificulty.Challenge);
        transform.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>
    /// Sets the level selection menu active (shows it)
    /// </summary>
    private void ShowLevelSelection() {
        transform.GetComponent<Canvas>().enabled = true;
    }

    /// <summary>
    /// Updates the displayed scores in the level selection menu and activates difficulty buttons if threshold score from previous difficulty is reached
    /// </summary>
    private void UpdateScores() {
        int[] scoreArray = scoreManager.finalScore.Array();
        int[] maxArray = (int[])Enum.GetValues(typeof(LevelDificulty));
        for (int i = 0; i < dificultyButtons.Length; i++) {
            if (i == 0 || scoreArray[i-1] > ScoreManager.PASS_LEVEL_SCORE) {
                dificultyButtons[i].GetComponent<Button>().interactable = true;
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(scoreArray[i] + "/" + maxArray[i]);
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(255,255,255,255);
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = 20;
            }
            else {
                // dificultyButtons[i].GetComponent<Button>().interactable = false;
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("BLOQUEADO");
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(194,78,82,255);
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().fontSize = 15;
            }
        }
    }

}
