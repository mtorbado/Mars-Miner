using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

/// <summary>
/// Generates de level selection UI panels dinamically
/// Based on https://pressstart.vip/tutorials/2019/06/1/96/level-selector.html
/// </summary>
public class LevelSelector : MonoBehaviour {

    public GameObject levelHolder;
    public GameObject levelIcon;
    public GameObject thisCanvas;
    public Vector2 iconSpacing;

    private int numOfLevels;
    private Rect panelDimensions;
    private Rect iconDimensions;
    private int maxPerPage;
    private int currentLevelCount;

    const string LevelFolder = "LevelFiles";
    const string LevelFileNaming = "level_";

    private void Start() {
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;

        int maxInRow = Mathf.FloorToInt((panelDimensions.width + iconSpacing.x) / (iconDimensions.width + iconSpacing.x));
        int maxInCol = Mathf.FloorToInt((panelDimensions.height + iconSpacing.y) / (iconDimensions.height + iconSpacing.y));

        numOfLevels = GetNumOfLevels(new DirectoryInfo(LevelFolder));

        maxPerPage = maxInCol * maxInRow;
        int totalPages = Mathf.CeilToInt((float)numOfLevels / maxPerPage);

        GameEvents.current.onLevelLoad += HideLevelSelection;


        LoadPanels(totalPages);
    }

    /// <summary>
    /// Returns the current number of levels in the game
    /// </summary>
    /// <param name="di"> DirectoryInfo of folder that contains the level scripts </param>
    /// <returns></returns>
    private int GetNumOfLevels(DirectoryInfo di) {
        int numOfLevels = 0; 
        FileInfo[] fiList = di.GetFiles();
        foreach (FileInfo fi in fiList) {
            if (fi.Name.Contains(LevelFileNaming) && fi.Extension.Contains("cs")) {
                numOfLevels++;
            }
        }
        return numOfLevels;
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
             icon.name = "Level" + i;
             icon.GetComponentInChildren<TextMeshProUGUI>().SetText("Level " + i);
         }
    }

    private void HideLevelSelection(int levelNumber) {
        thisCanvas.SetActive(false);
    }
}
