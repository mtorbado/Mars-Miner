using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract class to define game levels
/// </summary>
public abstract class AbsLevel : MonoBehaviour, ILevel {

    // Must be overrided
    public abstract IEnumerator Play();

    // public void Pause() {}

    // public void ForceStop() {}
}
