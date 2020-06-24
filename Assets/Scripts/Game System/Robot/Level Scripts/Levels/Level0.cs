using System.Collections;

public class Level0 : AbsLevel {

    private RobotActions robotActions;
    private int OreGoal {get;}
    private int OreCount {get; set;} // move to interface?

    private void Awake() {
        robotActions = (RobotActions)transform.GetComponent<RobotActions>(); // si Level0 está en character cube 
    }
    
    public override IEnumerator Play() {
        
        while (OreCount < OreGoal) {
            yield return robotActions.MoveFoward();
        }
        robotActions.Stop();
    }
}
