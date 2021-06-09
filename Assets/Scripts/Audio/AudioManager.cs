using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

    private void Start() {
    GameEvents.current.onLevelFailed += LevelFailed;
    GameEvents.current.onLevelPassed += LevelPassed;
    GameEvents.current.onLevelLoad += LevelLoad;
    GameEvents.current.onExitGame += ExitGame;
    }

    private void Awake() {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop; 
        }

    }

    public void Play (string name) {
        Sound s = Array.Find(sounds, sound => sound.name.Equals(name));
        if (s != null) {
            s.source.Play();
        }
    }

    public void Stop (string name) {
        Sound s = Array.Find(sounds, sound => sound.name.Equals(name));
        s.source.Stop();
    }

    public void SetVolume (string name, int volume) {
        Sound s = Array.Find(sounds, sound => sound.name.Equals(name));
        s.source.volume = volume;
    }

    private void LevelFailed() {
        Stop("robot_motor");
        Stop("robot_motor2");
    }

    private void LevelPassed() {
        Stop("robot_motor2");
    }

    private void LevelLoad() {
        Play("wind");
        Play("robot_motor");
    }

    private void ExitGame() {
        Stop("wind");
        Stop("robot_motor");
    }

}
