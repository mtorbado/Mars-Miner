using System.Collections;
using UnityEngine;

public class Level0 : AbsLevel {

    private RobotActions robotActions;

    private void Awake() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        isTutorial = true;
        oreGoal = 1;
        dificulty = LevelDificulty.Easy;
    }
    
    public override IEnumerator Play(string[] args) {
        while (!CheckLevelPassed() && !CheckLevelFailed()) {
            yield return robotActions.MoveFoward();
        }
        if (CheckLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
    }
}
