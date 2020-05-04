using System.Collections.Generic;


/// <summary>
/// Serializable class to save game progress
/// </summary>
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