using System.Collections;

public class Hard1 : AbsLevel {

    public override void Initialize() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 3;
    }
    
    public override IEnumerator Play(string[] args) {
        
        while (!CheckLevelFailed() && oreCount < oreGoal) {
            if (robotActions.IsRockInFront(1)) {
                yield return robotActions.TurnRight();
            }
            yield return robotActions.TurnRight();
            for (int i=0; i < 3 && !CheckLevelFailed(); i++) {
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