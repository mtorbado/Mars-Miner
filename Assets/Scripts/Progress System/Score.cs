[System.Serializable] public class Score {

    public int[] scoreArray;

    public Score() {
        scoreArray = new int[4]{0,0,0,0};
    }

    public int TotalScore() {
        int totalScore = 0;
        for (int i = 0; i < scoreArray.Length; i++) {
            totalScore += scoreArray[i];
        }
        return totalScore;
    }
}