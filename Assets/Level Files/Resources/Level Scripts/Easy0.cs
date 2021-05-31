using System.Collections;

public class Easy0 : AbsLevel {


    public override void Initialize() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>();
        oreGoal = 1;
    }
    
    public override IEnumerator Play(string[] args) {
        for (int i=0; i < 5 && !CheckLevelFailed(); i++) {
            yield return robotActions.MoveFoward();
        }
        if (CheckLevelFailed()) {
            yield return robotActions.BreakAnimation();
        }
        else {
            CheckLevelPassed();
        }
    }
}
