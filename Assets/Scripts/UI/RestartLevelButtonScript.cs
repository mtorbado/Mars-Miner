using UnityEngine;

public class RestartLevelButtonScript : MonoBehaviour {

    LevelLoader levelLoader;

    private void Start() {
        levelLoader = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelLoader>();
    }

    public void RestartLevel() {
        Debug.Log("Reloading current level");
        levelLoader.RestartLevel();
    }
}
