using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class TutorialCanvasActions : MonoBehaviour {

    public GameObject tutorialPanel;
    public GameObject textHolder;
    public GameObject nextPanelButton;
    public GameObject previousPanelButton;
    public GameObject image;

    public Sprite[] tutorialImages;

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
        previousPanelButton.SetActive(false);
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
        TextAsset txt = (TextAsset)Resources.Load("tutorial");
        String[] tutorialText = txt.text.Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        return tutorialText;
    }

}