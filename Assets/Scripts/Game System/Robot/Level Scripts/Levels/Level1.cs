using System.Collections;
using UnityEngine;

public class Level1 : AbsLevel {

    private RobotActions robotActions;

    private void Awake() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 1;
        dificulty = LevelDificulty.Easy;
    }
    
    public override IEnumerator Play() {
        
        while (!checkLevelPassed() && !checkLevelFailed()) {
            if (robotActions.IsRockInFront()) {
                yield return robotActions.TurnLeft();
            } else yield return robotActions.MoveFoward();
        }
        if (checkLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
    }
}