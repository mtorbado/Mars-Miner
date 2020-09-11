using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public void PlayGame() {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }

    public void QuitGame() {
        Application.Quit();
        Debug.Log("Game was closed");
    }
}
