using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class TimerScript : MonoBehaviour {

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI penaltyText;

    public const int MAX_PENALTY = 200;
    public const int PENALTY = 50;
    public const int PENALTY_LAPSE = 30;

    int seconds = 0;
    int penalty = 0;
    bool stop = false;

    private void Start() {
        GameEvents.current.onLevelLoad += ResetTime;
        StartCoroutine(Timer());
    }

    private void ResetTime() {
        seconds = 0;
        penalty = 0;
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        timeText.SetText(time.ToString(@"mm\:ss"));
    }

    private IEnumerator Timer() {
        seconds = 0;
        penalty = 0;
        while (!stop) {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            timeText.SetText(time.ToString(@"mm\:ss"));
            penaltyText.SetText("- " + penalty);
            yield return new WaitForSeconds(1.0f);
            seconds++;
            if (seconds > 30) {
                penalty = Math.Min(MAX_PENALTY, (seconds/PENALTY_LAPSE) * PENALTY);
            }
        }
    }

    public int GetSeconds() {
        return seconds;
    }

    public int GetPenalty() {
        return penalty;
    }

    public string GetTime() {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return time.ToString(@"mm\:ss");
    }
}
