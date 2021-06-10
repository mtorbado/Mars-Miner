using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

    private void Start() {
    GameEvents.current.onLevelFailed += StopRobotNoise;
    GameEvents.current.onLevelPassed += StopRobotNoise;
    GameEvents.current.onLevelLoad += StartSceneSound;
    GameEvents.current.onExitGame += StopSceneSound;
    GameEvents.current.onSelectLevel += StopSceneSound;
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
        if (s != null) {
            s.source.Stop();
        }
    }

    public void SetVolume (string name, float volume) {
        Sound s = Array.Find(sounds, sound => sound.name.Equals(name));
        if (s != null) {
            s.source.volume = volume;
        }
    }

    public void SetMusicVolume(float volume) {
        SetVolume("music", volume);
    }

    public void SetFxVolume(float volume) {
        SetVolume("robot_motor", volume);
        SetVolume("robot_motor2", volume);
        SetVolume("robot_motor3", volume);
        SetVolume("bump", volume);
        SetVolume("wind", volume * 0.3f);
        SetVolume("magnet", volume);
        SetVolume("score", volume);
        SetVolume("beep", volume);
        
    }

    private void StopRobotNoise() {
        Stop("robot_motor");
        Stop("robot_motor2");
    }

    private void StartSceneSound() {
        Play("wind");
        Play("robot_motor");
    }

    private void StopSceneSound() {
        Stop("wind");
        Stop("robot_motor");
    }

}
