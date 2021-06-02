using System.Collections;

public class Challenge0 : AbsLevel {

    public override void Initialize() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 2;
    }
    
    public override IEnumerator Play(string[] args) {
        
        while (!CheckLevelPassed() && !CheckLevelFailed()) {
           if (robotActions.IsRockInFront(1)) {
                yield return robotActions.TurnRight();
            }
            else {
                if (robotActions.IsRockInFront(1)) {
                    yield return robotActions.TurnLeft();
                }
                yield return robotActions.MoveFoward();
            }
            
        }
        if (CheckLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
    }
}