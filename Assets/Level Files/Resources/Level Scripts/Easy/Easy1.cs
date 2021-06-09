using System.Collections;

public class Easy1 : AbsLevel {

    public override void Initialize() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 1;
    }

    public override IEnumerator Play(string[] args) {
        for (int i=0; i < 3 && !CheckLevelFailed(); i++) {
            yield return robotActions.MoveFoward();
        }
        if (robotActions.IsRockInFront(1) && !CheckLevelFailed()) {
            yield return robotActions.TurnRight();
        }
        for (int i=0; i < 2 && !CheckLevelFailed(); i++) {
            yield return robotActions.MoveFoward();
        }
        if (CheckLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
        else {
            CheckLevelPassed();
        }
    }
}