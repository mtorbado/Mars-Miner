using System.Collections;

public class Challenge1 : AbsLevel {

    public override void Initialize() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 2;
    }
    
    public override IEnumerator Play(string[] args) {
        
        while (!CheckLevelPassed() && !CheckLevelFailed()) {
            if (robotActions.IsRockInFront(1)) {
                yield return robotActions.TurnRight();
                yield return robotActions.MoveFoward();
                CheckFail();
            }
            else {
                yield return robotActions.TurnLeft();
                if (robotActions.IsRockInFront(1)) {
                    yield return robotActions.TurnLeft();
                }
                else {
                    yield return robotActions.MoveFoward();
                    CheckFail();
                }
            }
            yield return robotActions.MoveFoward();
        }
        if (CheckLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
    }

    private IEnumerator CheckFail() {
        if(CheckLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
    }
}