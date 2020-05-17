using System.Collections;

public class Level0 : AbsLevel {

    private RobotActions robotActions;
    private int OreGoal {get;}
    private int OreCount {get; set;} // move to interface?

    private void Awake() {
        robotActions = new RobotActions();
    }

    public override IEnumerator Play() {
        
        while (OreCount < OreGoal) {
            yield return robotActions.MoveFoward();
        }
        robotActions.Stop();
    }
}
