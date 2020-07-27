using UnityEngine;
using TMPro;

public class LevelButtonScript : MonoBehaviour {
    public int levelNumber;
    public TextMeshProUGUI buttonText;  
    private LevelLoader ll;

    private void Start() {
        levelNumber = int.Parse(buttonText.text.Remove(0, 5)); 
    }

   public void LoadLevel() {
        Debug.Log("Loading level " + levelNumber);
        GameEvents.current.LoadLevel(levelNumber);
    }
}
