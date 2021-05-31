using System.Collections;

public class Hard2 : AbsLevel {

    public override void Initialize() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 1;
    }
    
    public override IEnumerator Play(string[] args) {
        
        for (int i=0; i < 10 && !CheckLevelFailed(); i++) {
            if (robotActions.IsRockInFront(1)) {
                yield return robotActions.TurnRight();
                yield return robotActions.MoveFoward();
            }
            else {
                yield return robotActions.MoveFoward();
            }
        }
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