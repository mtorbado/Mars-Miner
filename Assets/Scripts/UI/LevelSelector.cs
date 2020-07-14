using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// From https://pressstart.vip/tutorials/2019/06/1/96/level-selector.html
/// </summary>
public class LevelSelector : MonoBehaviour {

    public GameObject levelHolder;
    public GameObject levelIcon;
    public GameObject thisCanvas;
    public int numOfLevels; //?
    public Vector2 iconSpacing;

    private Rect panelDimensions;
    private Rect iconDimensions;
    private int maxPerPage;
    private int currentLevelCount;

    private void Start() {
        panelDimensions = levelHolder.GetComponent<RectTransform>().rect;
        iconDimensions = levelIcon.GetComponent<RectTransform>().rect;
        int maxInRow = Mathf.FloorToInt((panelDimensions.width + iconSpacing.x) / (iconDimensions.width + iconSpacing.x));
        int maxInCol = Mathf.FloorToInt((panelDimensions.height + iconSpacing.y) / (iconDimensions.height + iconSpacing.y));
        maxPerPage = maxInCol * maxInRow;
        int totalPages = Mathf.CeilToInt((float)numOfLevels / maxPerPage);
        LoadPanels(totalPages);
    }

    private void LoadPanels(int numOfPanels) {
        GameObject panelClone = Instantiate(levelHolder) as GameObject;
        // levelHolder.AddComponent<PageSwiper>();

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
    
    private void SetUpGrid(GameObject panel) {
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(iconDimensions.width, iconDimensions.height);
        grid.childAlignment = TextAnchor.MiddleCenter;
        grid.spacing = iconSpacing;
    }

    private void LoadIcons(int numOfIcons, GameObject parentObject) {
         for (int i=1; i <= numOfIcons; i++) {
             currentLevelCount++;
             GameObject icon = Instantiate(levelIcon) as GameObject;
             icon.transform.SetParent(thisCanvas.transform, false);
             icon.transform.SetParent(parentObject.transform);
             icon.name = "Level" + i;
             icon.GetComponentInChildren<TextMeshProUGUI>().SetText("Level " + currentLevelCount);
         }
    }
}
