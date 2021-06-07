using UnityEngine;

/// <summary>
/// Simple class to play and pause the particle animations of movable rocks
/// </summary>
public class RockPariclesController : MonoBehaviour {

    AudioManager audioManager;
    ParticleSystem[] particles;
    private bool showParticles;

    private void Start() {
        particles = GetComponentsInChildren<ParticleSystem>();
        audioManager = FindObjectOfType<AudioManager>();
        foreach( ParticleSystem p in particles) {
            p.Stop();
        }
    }

    private void Awake() {
        showParticles = true;
        GameEvents.current.onDisableAllForTutorial += DisableParticles;
        GameEvents.current.onEnableAllAfterTutorial += EnableParticles; 
    }

    private void OnMouseDown() {
        if(showParticles) {
            foreach( ParticleSystem p in particles) {
                p.Play();
            }
            audioManager.Play("magnet");
        }
    }

    private void OnMouseUp() {
        foreach( ParticleSystem p in particles) {
            p.Stop();
            audioManager.Stop("magnet");
            audioManager.Play("bump");
        }
    }

    private void EnableParticles() {
        showParticles = true;
    }

    private void DisableParticles() {
        showParticles = false;
    }

}
