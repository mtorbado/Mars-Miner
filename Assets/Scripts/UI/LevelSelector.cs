using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class LevelSelector : MonoBehaviour {

    ScoreManager scoreManager;

    public GameObject[] dificultyButtons;

    private void Start() {

        GameEvents.current.onSelectLevel += ShowLevelSelection;
        GameEvents.current.onUpdateScores += UpdateScores;

        scoreManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<ScoreManager>();
    }

    public void PlayEasy() {
        GameEvents.current.LoadLevel((Int32)LevelInfo.FirstEasy);
        transform.GetComponent<Canvas>().enabled = false;
        GameEvents.current.DisableAllForTutorial();
    }

    public void PlayMedium() {
        GameEvents.current.LoadLevel((Int32)LevelInfo.FirstMedium);
        transform.GetComponent<Canvas>().enabled = false;
    }

    public void PlayHard() {
        GameEvents.current.LoadLevel((Int32)LevelInfo.FirstHard);
        transform.GetComponent<Canvas>().enabled = false;
    }

    public void PlayChallenge() {
        GameEvents.current.LoadLevel((Int32)LevelInfo.FirstChallenge);
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
        int[] scoreArray = scoreManager.finalScore.scoreArray;
        for (int i = 0; i < dificultyButtons.Length; i++) {
            if (i == 0 || scoreArray[i-1] > (scoreManager.maxScores[i-1] / 2)) { //TODO: decide score to pass to next dificulty
                dificultyButtons[i].GetComponent<Button>().interactable = true;
                dificultyButtons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(scoreArray[i] + "/" + scoreManager.maxScores[i]);
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
