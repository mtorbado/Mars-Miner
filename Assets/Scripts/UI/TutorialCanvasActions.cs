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
    public GameObject image;

    public Sprite[] tutorialImages;

    const string LevelFolder = "LevelFiles";
    const string TutorialFileNaming = "tutorial";
    const string ImageFolder = "Assets/Prefabs/UI/Images/";

    private int currentPanel;
    private String[] tutorialText;

    private void Start() {
        GameEvents.current.onDisableAllForTutorial += LoadFirstTutorialPanel;
        tutorialText = ReadTutorialTextFile();
    }

    private void LoadFirstTutorialPanel() {
        tutorialPanel.SetActive(true);
        currentPanel = 0;
        UpdateText(currentPanel);
        UpdateImage(currentPanel);
    }

    public void LoadNextTutorialPanel() {
        if (currentPanel >= 0 && currentPanel < tutorialText.Length-1) {
            UpdateText(currentPanel+1);
            UpdateImage(currentPanel+1);
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
            UpdateText(currentPanel-1);
            UpdateImage(currentPanel-1);
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

    private void UpdateImage(int n) {
        image.GetComponent<Image>().sprite = tutorialImages[n] as Sprite;
    }

    private void UpdateText(int n) {
        textHolder.GetComponent<TextMeshProUGUI>().SetText(tutorialText[n]);
    }

    private String[] ReadTutorialTextFile() {
        StreamReader reader = new StreamReader(LevelFolder + "/" + TutorialFileNaming + ".txt"); 
        String[] tutorialText = reader.ReadToEnd().Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        return tutorialText;
    }

}