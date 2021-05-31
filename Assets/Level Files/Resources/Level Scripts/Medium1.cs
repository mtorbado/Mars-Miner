using System.Collections;

public class Medium1 : AbsLevel {

    public override void Initialize() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 1;
    }
    
    public override IEnumerator Play(string[] args) {
        for (int i=0; i < 3 && !CheckLevelFailed(); i++) {
            yield return robotActions.MoveFoward();
        }
        CheckFail();
        yield return robotActions.TurnLeft();
        yield return robotActions.TurnRight();
        yield return robotActions.MoveFoward();
        CheckFail();
        yield return robotActions.MoveFoward();
        CheckFail();
        yield return robotActions.MoveFoward();
        if (CheckLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
        else {
            CheckLevelPassed();
        }
    }

    private IEnumerator CheckFail() {
        if(CheckLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
    }
}