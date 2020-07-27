using UnityEngine;
using TMPro;

/// <summary>
/// 
/// </summary>
public class LevelButtonScript : MonoBehaviour {
    public int levelNumber;
    public TextMeshProUGUI buttonText;  
    private LevelLoader ll;

    private void Start() {
        levelNumber = int.Parse(buttonText.text.Remove(0, 5)); 
    }

    /// <summary>
    /// 
    /// </summary>
    public void LoadLevel() {
        Debug.Log("Loading level " + levelNumber);
        GameEvents.current.LoadLevel(levelNumber);
    }
}
