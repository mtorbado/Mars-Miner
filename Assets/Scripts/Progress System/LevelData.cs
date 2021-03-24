using System.Collections.Generic;


public class LevelData {

    public int levelNumber;
    public bool passed;
    public int numberOfAttempts;
    public int points;
    
    public LevelData(int levelNumber) {
        this.levelNumber = levelNumber;
        this.passed = false;
        this.numberOfAttempts = 0;
        this.points = 0;
    }
}