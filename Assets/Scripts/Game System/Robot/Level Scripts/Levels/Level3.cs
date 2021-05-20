using System.Collections;

public class Level3 : AbsLevel {

    private RobotActions robotActions;

    private void Awake() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 2;
        dificulty = LevelDificulty.Medium;
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