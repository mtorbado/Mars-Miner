[System.Serializable] public class Score {

    public int easyPoints;
    public int mediumPoints;
    public int hardPoints;
    public int challengePoints;

    public Score() {
        easyPoints = 0;
        mediumPoints = 0;
        hardPoints = 0;
        challengePoints = 0;
    }

    public int[] Array() {
        return new int[4] {easyPoints, mediumPoints, hardPoints, challengePoints};
    }

    public int GetPoints(LevelDificulty levelDificulty) {
        switch(levelDificulty) {
            case LevelDificulty.Easy: return easyPoints;
            case LevelDificulty.Medium: return mediumPoints;
            case LevelDificulty.Hard: return hardPoints;
            case LevelDificulty.Challenge: return challengePoints;
        }
        return 0;
    }

    public void SetPoints(LevelDificulty levelDificulty, int points) {
        switch(levelDificulty) {
            case LevelDificulty.Easy: 
                easyPoints = points;
                break;
            case LevelDificulty.Medium:
                mediumPoints = points;
                break;
            case LevelDificulty.Hard:
                hardPoints = points;
                break;
            case LevelDificulty.Challenge:
                challengePoints = points;
                break;
        }
    }

    public void AddPoints(LevelDificulty levelDificulty, int points) {
        switch(levelDificulty) {
            case LevelDificulty.Easy: 
                easyPoints += points;
                break;
            case LevelDificulty.Medium:
                mediumPoints += points;
                break;
            case LevelDificulty.Hard:
                hardPoints += points;
                break;
            case LevelDificulty.Challenge:
                challengePoints += points;
                break;
        }
    }

    public int TotalScore() {
        return easyPoints + mediumPoints + hardPoints + challengePoints;
    }
}