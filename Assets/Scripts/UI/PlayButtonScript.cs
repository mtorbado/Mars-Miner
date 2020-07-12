﻿using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class PlayButtonScript : MonoBehaviour {

    bool isCoroutineStarted;
    Button button;

    private void Start() {
        button = GetComponent<Button>();
    }

    /// <summary>
    /// 
    /// </summary>
    public void Play() {
        
        button.interactable = false;
        GetComponentInChildren<Text>().text = "Running!";

        GameObject[] characterCubes = GameObject.FindGameObjectsWithTag("CharacterCube");
        foreach (GameObject characterCube in characterCubes) {
            StartCoroutine(characterCube.GetComponent<ILevel>().Play());
        }
    }
}