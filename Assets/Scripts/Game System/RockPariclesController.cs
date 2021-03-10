using UnityEngine;

/// <summary>
/// Simple class to play and pause the particle animations of movable rocks
/// </summary>
public class RockPariclesController : MonoBehaviour {

    ParticleSystem[] particles;

    private void Start() {
        particles = GetComponentsInChildren<ParticleSystem>();
        foreach( ParticleSystem p in particles) {
            p.Stop();
        }
    }

    private void OnMouseDown() {
        foreach( ParticleSystem p in particles) {
            p.Play();
        }
    }

    private void OnMouseUp() {
        foreach( ParticleSystem p in particles) {
            p.Stop();
        }
    }

}
