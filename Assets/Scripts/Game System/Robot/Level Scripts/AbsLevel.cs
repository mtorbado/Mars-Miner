using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract class to define game levels
/// </summary>
public abstract class AbsLevel : MonoBehaviour, ILevel {

    public int oreGoal;
    public int oreCount = 0;

    /// <summary>
    /// Abstract method to override with game loop for each level 
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerator Play();

    // public void Pause() {}

    // public void ForceStop() {}

    public void pickOre() {
        this.oreCount++;
    }
}
