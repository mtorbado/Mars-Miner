using UnityEngine;

/// <summary>
/// Simple class to play and pause the particle animations of movable rocks
/// </summary>
public class RockPariclesController : MonoBehaviour {

    ParticleSystem particles;

    private void Start() {
        particles = GetComponentInChildren<ParticleSystem>();
        particles.Stop();
    }

    private void OnMouseDown() {
        particles.Play();
    }

    private void OnMouseUp() {
        particles.Stop();
    }

}
