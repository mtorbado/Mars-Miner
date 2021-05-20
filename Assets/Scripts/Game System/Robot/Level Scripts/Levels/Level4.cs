using System.Collections;

public class Level4 : AbsLevel {

    private RobotActions robotActions;

    private void Awake() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 2;
        dificulty = LevelDificulty.Medium;
    }
    
    public override IEnumerator Play(string[] args) {
        int power = 5;
        while ((power > 0) && (!CheckLevelPassed() && !CheckLevelFailed())) {
            int oldOC = oreCount;
            if (robotActions.IsRockInFront(1)) {
                if (power % 2 == 0) {
                    yield return robotActions.TurnRight();
                }
                else {
                    yield return robotActions.TurnLeft();
                }
            }
            else {
                yield return robotActions.MoveFoward();
            }
            power --;
            if (oreCount > oldOC) {
                power = 5;
            }
            oldOC = oreCount;
        }
        if (CheckLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
    }
}