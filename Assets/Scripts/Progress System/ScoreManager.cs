using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    
    public Score finalScore;
    public int[] maxScores;

    public const int PASS_GAME_SCORE = 4000;
    public const int PASS_DIFICULTY_SCORE = 500;
    public const int MIN_LEVEL_SCORE = 100;

    public const int ATTEMP_PENALTY = 50; // each attempt reduces score by ATTEMP_PENALTY
    public const int SCORE_FACTOR = 4; // max score per level is (levelDificulty / SCORE_FACTOR)

    int attempts;

    LevelDificulty lastDificulty;
    int lastLevelNumber;

    LevelLoader levelLoader;
    TimerScript timerScript;
    
    void Start() {
        GameEvents.current.onLoadGameData += LoadScore;
        GameEvents.current.onLevelLoad += Reset;
        GameEvents.current.onPlayLevel += AddAttempt;
        GameEvents.current.onLevelPassed += SaveScore;

        levelLoader = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelLoader>();
        timerScript = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>();

        finalScore = new Score();
        GameEvents.current.UpdateScores();
        RetrieveScore();
    }

    public int LevelPoints() {
        return (Math.Max(((int)levelLoader.playingDificulty/SCORE_FACTOR) - (timerScript.GetPenalty() + (attempts-1)*ATTEMP_PENALTY), MIN_LEVEL_SCORE));
    }

    public int LevelAttempts() {
        return attempts;
    }

    private void Reset() {
        if (levelLoader.playingDificulty !=lastDificulty || levelLoader.playingLevel != lastLevelNumber) {
            attempts = 0;
        }
        lastDificulty = levelLoader.playingDificulty;
        lastLevelNumber = (int)levelLoader.playingLevel;        
    }

    private void AddAttempt() {
        attempts ++;
    }
   
    /* =============================================================== LOAD SCORE ============================================================= */

    private void RetrieveScore() {
        #pragma warning disable CS0618
        Application.ExternalCall ("getCampoLibre");
        #pragma warning restore CS0618
    }

    private void LoadScore(string scoreStr) {
        try {
            if (!scoreStr.Equals("") && scoreStr != null) {
                finalScore = JsonUtility.FromJson<Score>(scoreStr);
                GameEvents.current.UpdateScores();
            }
        } catch (Exception e) {
            Debug.Log("Could not read GameData string: " + e.ToString());
        }
    }

    /* =============================================================== SAVE SCORE ============================================================= */

    private void SaveScore() {
        
        int points = Math.Min(LevelPoints() + finalScore.GetPoints(levelLoader.playingDificulty), (int)levelLoader.playingDificulty);
        finalScore.SetPoints(levelLoader.playingDificulty, Math.Max(finalScore.GetPoints(levelLoader.playingDificulty), points));

        string jsonScore = JsonUtility.ToJson(finalScore);
        GameEvents.current.UpdateScores();
        
        #pragma warning disable CS0618
        Application.ExternalCall("setCampoLibre", jsonScore);
        #pragma warning restore CS0618
    }

    public void SaveGlobalData() {
        int completed = 0;
        if (finalScore.TotalScore() >= PASS_GAME_SCORE) completed = 1;
        int [] data = {finalScore.TotalScore(), completed};
        #pragma warning disable CS0618
        Application.ExternalCall("guardar", data);
        #pragma warning restore CS0618
    }
}
