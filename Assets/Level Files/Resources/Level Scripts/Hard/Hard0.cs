using System.Collections;

public class Hard0 : AbsLevel {

    public override void Initialize() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 1;
    }
    
    public override IEnumerator Play(string[] args) {
        
        while (!CheckLevelPassed() && !CheckLevelFailed()) {
            if (robotActions.IsRockInFront(1)) {
                yield return robotActions.TurnLeft();
            }
            else {
                yield return robotActions.MoveFoward();
            }
        }
        if (CheckLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
    }
}