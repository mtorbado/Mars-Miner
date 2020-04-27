using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int lastLeveCompleted;
    public List<int> scoreTable;
    public float timePlayed;
    
     public GameData(int lastLeveCompleted, List<int> scoreTable, float timePlayed)
     {
         this.lastLeveCompleted = lastLeveCompleted;
         this.scoreTable = scoreTable;
         this.timePlayed = timePlayed;
     }

}