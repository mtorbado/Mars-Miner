using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public void PlayGame() {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }

    public void QuitGame() {
        #pragma warning disable CS0618
        Application.ExternalCall ("salir");
        #pragma warning restore CS0618
        Application.Quit();
        Debug.Log("Game was closed");
    }
}
