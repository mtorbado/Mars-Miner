using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevelButtonScript : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public void RestartLevel() {
        Debug.Log("Reloading current level");
        GameEvents.current.RestartLevel();
    }
}
