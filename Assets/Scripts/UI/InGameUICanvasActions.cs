using UnityEngine;
using TMPro;

public class InGameUICanvasActions : MonoBehaviour {

    public GameObject codeHolder;
    public GameObject docHolder;
    public GameObject scoreHolder;
    
    private void Start() {
        LoadHooverDoc();
        GameEvents.current.onLevelLoad += ShowInGameUI;
        GameEvents.current.onSelectLevel += HideInGameUI;
        GameEvents.current.onLevelFailed += HideInGameUI;
        GameEvents.current.onLevelPassed += HideInGameUI;
        this.gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void LoadCode(TextAsset txt) {
        codeHolder.GetComponent<TextMeshProUGUI>().SetText(txt.text);
    }

    public void LoadOreGoal(int oreGoal) {
        Debug.Log(oreGoal);
        scoreHolder.GetComponent<CountOreScript>().SetOreGoal(oreGoal);
    }

    private void LoadHooverDoc() {
        TextAsset txt = (TextAsset)Resources.Load("hoover_doc");
        docHolder.GetComponent<TextMeshProUGUI>().SetText(txt.text);
    }

    private void ShowInGameUI() {
        this.gameObject.GetComponent<Canvas>().enabled = true;
    }

    private void HideInGameUI() {
        this.gameObject.GetComponent<Canvas>().enabled = false;
    }
}
