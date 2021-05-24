using System.Collections;

public class Level2 : AbsLevel {

    private RobotActions robotActions;

    private void Awake() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 1;
    }
    
    public override IEnumerator Play(string[] args) {
        yield return robotActions.MoveFoward();
        yield return CheckFail();
        yield return robotActions.MoveFoward();
        yield return CheckFail();
        if (robotActions.IsRockInFront(1)) {
            yield return robotActions.MoveFoward();
            yield return CheckFail();
            yield return robotActions.TurnLeft();
        }
        else {
            yield return robotActions.TurnLeft();
            yield return CheckFail();
            yield return robotActions.MoveFoward();
            yield return CheckFail();
        }
        yield return CheckFail();
        yield return robotActions.MoveFoward();
        yield return CheckFail();
        yield return robotActions.TurnLeft();
        yield return CheckFail();
        yield return robotActions.MoveFoward();
        yield return CheckFail();
        if (robotActions.IsRockInFront(1)) {
            yield return robotActions.TurnRight();
            yield return CheckFail();
            yield return robotActions.MoveFoward();
            yield return CheckFail();
        }
        yield return robotActions.MoveFoward();
        yield return CheckFail();
        yield return robotActions.MoveFoward();
        yield return CheckFail();
        if(!CheckLevelFailed()) {
            CheckLevelPassed();
        } 
    }

    private IEnumerator CheckFail() {
        if(CheckLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
    }
}