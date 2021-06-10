using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsCanvasActions : MonoBehaviour {

    Slider musicSlider;
    Slider fxSlider;

    AudioManager audioManager;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        transform.GetComponent<Canvas>().enabled = false;

        fxSlider = GetComponentsInChildren<Slider>()[0];
        musicSlider = GetComponentsInChildren<Slider>()[1];

        fxSlider.onValueChanged.AddListener(delegate {UpdateFxVolume(); });
        musicSlider.onValueChanged.AddListener(delegate {UpdateMusicVolume(); });
    }

    private void UpdateMusicVolume() {
        audioManager.SetMusicVolume(musicSlider.value);
    }

    private void UpdateFxVolume() {
        audioManager.SetFxVolume(fxSlider.value);
    }

    public void OpenOptionsPanel() {
        audioManager.Play("beep");
        transform.GetComponent<Canvas>().enabled = true;
    }

    public void CloseOptionsPanel() {
        audioManager.Play("beep");
        transform.GetComponent<Canvas>().enabled = false;
    }
}
