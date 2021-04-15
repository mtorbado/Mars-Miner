using UnityEngine;
using System.IO;
using System;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Displays the list of tutorial panels for the tutorial level and then re-enables the game interaction
/// </summary>
public class TutorialCanvasActions : MonoBehaviour {

    public GameObject tutorialPanel;
    public GameObject textHolder;
    public GameObject nextPanelButton;
    public GameObject previousPanelButton;

    const string LevelFolder = "LevelFiles";
    const string TutorialFileNaming = "tutorial";

    private int currentPanel;
    private String[] tutorialText;

    private void Start() {
        GameEvents.current.onDisableAllForTutorial += LoadFirstTutorialPanel;
        tutorialText = ReadTutorialTextFile();
    }

    private void LoadFirstTutorialPanel() {
        tutorialPanel.SetActive(true);
        textHolder.GetComponent<TextMeshProUGUI>().SetText(tutorialText[0]);
        currentPanel = 0;
    }

    public void LoadNextTutorialPanel() {
        if (currentPanel >= 0 && currentPanel < tutorialText.Length-1) {
            textHolder.GetComponent<TextMeshProUGUI>().SetText(tutorialText[currentPanel+1]);
            currentPanel ++;
        }
        else if (currentPanel == tutorialText.Length-1) {
            tutorialPanel.SetActive(false);
            GameEvents.current.EnableAllAfterTutorial();
        }
        UpdateButtons();
    }

    public void LoadPreviousTutorialPanel() {
        if (currentPanel >= 1 && currentPanel < tutorialText.Length-1) {
            textHolder.GetComponent<TextMeshProUGUI>().SetText(tutorialText[currentPanel-1]);
            currentPanel --;
        }
        else if (currentPanel == tutorialText.Length-1) {
            tutorialPanel.SetActive(false);
            GameEvents.current.EnableAllAfterTutorial();
        }
        UpdateButtons();
    }

    private void UpdateButtons() {
        if (currentPanel == 0) {
            previousPanelButton.SetActive(false);
            nextPanelButton.GetComponentInChildren<Text>().text = "Siguiente";
        }
        else if (currentPanel > 0 && currentPanel < tutorialText.Length-1) {
            previousPanelButton.SetActive(true);
            nextPanelButton.GetComponentInChildren<Text>().text = "Siguiente";
        }
        else if (currentPanel == tutorialText.Length-1) {
            nextPanelButton.GetComponentInChildren<Text>().text = "Comenzar";
        }
        
    }

    private String[] ReadTutorialTextFile() {
        StreamReader reader = new StreamReader(LevelFolder + "/" + TutorialFileNaming + ".txt"); 
        String[] tutorialText = reader.ReadToEnd().Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        return tutorialText;
    }

}