using System.Collections;

public class Level1Old : AbsLevel {

    private RobotActions robotActions;

    private void Awake() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 1;
    }
    
    public override IEnumerator Play(string[] args) {
        
        while (!CheckLevelPassed() && !CheckLevelFailed()) {
            if (robotActions.IsRockInFront(1)) {
                yield return robotActions.TurnLeft();
            } else yield return robotActions.MoveFoward();
        }
        if (CheckLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
    }
}