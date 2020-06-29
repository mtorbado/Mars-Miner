using System.Collections;

public class Level0 : AbsLevel {

    private RobotActions robotActions;

    private void Awake() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>(); // si Level0 está en character cube
        oreGoal = 1;
    }
    
    public override IEnumerator Play() {
        
        while (oreCount < oreGoal) {
            yield return robotActions.MoveFoward();
        }
        robotActions.Stop();
    }
}
