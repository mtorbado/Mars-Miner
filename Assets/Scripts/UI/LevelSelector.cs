using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Generates de level selection UI panels dinamically
/// Based on https://pressstart.vip/tutorials/2019/06/1/96/level-selector.html
/// </summary>
public class LevelSelector : MonoBehaviour {

    public GameObject levelHolder;
    public GameObject levelIcon;
    public GameObject thisCanvas;
    public Vector2 iconSpacing;

    public GameObject levelManager;

    private int numOfLevels;
    private Rect panelDimensions;
    private Rect iconDimensions;
    private int maxPerPage;
    private int currentLevelCount;

    private List<GameObject> icons;

    const string LevelFolder = "LevelFiles/";

    private void Start() {
        icons = new List<GameObject>();

        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;

        int maxInRow = Mathf.FloorToInt((panelDimensions.width + iconSpacing.x) / (iconDimensions.width + iconSpacing.x));
        int maxInCol = Mathf.FloorToInt((panelDimensions.height + iconSpacing.y) / (iconDimensions.height + iconSpacing.y));

        numOfLevels = LevelLoader.GetNumOfLevels(LevelFolder);

        maxPerPage = maxInCol * maxInRow;
        int totalPages = Mathf.CeilToInt((float)numOfLevels / maxPerPage);

        GameEvents.current.onLevelLoad += HideLevelSelection;
        GameEvents.current.onSelectLevel += ShowLevelSelection;
        GameEvents.current.onSelectLevel += UpdateScores;
        GameEvents.current.onUpdateScores += UpdateScores;

        LoadPanels(totalPages);
    }

    /// <summary>
    /// Sets up the level selector menu panels
    /// </summary>
    /// <param name="numOfPanels"> desired number of panels </param>
    private void LoadPanels(int numOfPanels) {
        GameObject panelClone = Instantiate(levelHolder) as GameObject;
        levelHolder.AddComponent<PageSwiper>();

        for (int i=1; i <= numOfPanels; i++) {
            GameObject panel = Instantiate(panelClone) as GameObject;
            panel.transform.SetParent(thisCanvas.transform, false);
            panel.transform.SetParent(levelHolder.transform);
            panel.name = "Page-" + i;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelDimensions.width * (i-1), 0);
            SetUpGrid(panel);
            int numOfIcons = i == numOfPanels ? numOfLevels - currentLevelCount : maxPerPage;
            LoadIcons( numOfIcons, panel);
        }
        Destroy(panelClone);
    }
    
    /// <summary>
    /// Configures grid to place level buttons
    /// </summary>
    /// <param name="panel">UI panel to contain the grid</param>
    private void SetUpGrid(GameObject panel) {
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.spacing = iconSpacing;
    }

    /// <summary>
    /// Sets the level buttons (icons)
    /// </summary>
    /// <param name="numOfIcons"> number of level buttons </param>
    /// <param name="parentObject"> object to set as parent (panel) </param>
    private void LoadIcons(int numOfIcons, GameObject parentObject) {
        for (int i=0; i < numOfIcons; i++) {
            currentLevelCount++;
            GameObject icon = Instantiate(levelIcon) as GameObject;
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);
            icon.name = i.ToString();
            icon.GetComponent<Button>().interactable = false;
            icon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Nivel " + i);
            icons.Add(icon);
         }
    }

    /// <summary>
    /// Sets the level selection menu inactive (hides it)
    /// </summary>
    /// <param name="levelNumber"> not used </param>
    private void HideLevelSelection(int levelNumber) {
        thisCanvas.GetComponent<Canvas>().enabled = false;
    }

    /// <summary>
    /// Sets the level selection menu active (shows it)
    /// </summary>
    private void ShowLevelSelection() {
        thisCanvas.GetComponent<Canvas>().enabled = true;
    }

    /// <summary>
    /// Updates the displayed scores in the level selection menu
    /// </summary>
    private void UpdateScores() {
        int lastLevelAlowed = levelManager.GetComponent<ScoreManager>().GetLastLevelCompleted() + 1;
        foreach(GameObject icon in icons) {
            try {
                if(int.Parse(icon.name) <= lastLevelAlowed) {
                    icon.GetComponent<Button>().interactable = true;
                    int score = levelManager.GetComponent<ScoreManager>().GetLevelScore(int.Parse(icon.name));
                    icon.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(score.ToString());
                }
            } catch {}
        }
    }
}
