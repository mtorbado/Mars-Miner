using UnityEngine;

/// <summary>
/// Simple class to play and pause the particle animations of movable rocks
/// </summary>
public class RockPariclesController : MonoBehaviour {

    ParticleSystem[] particles;
    private bool showParticles;

    private void Start() {
        particles = GetComponentsInChildren<ParticleSystem>();
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
        }
    }

    private void OnMouseUp() {
        foreach( ParticleSystem p in particles) {
            p.Stop();
        }
    }

    private void EnableParticles() {
        showParticles = true;
    }

    private void DisableParticles() {
        showParticles = false;
    }

}
