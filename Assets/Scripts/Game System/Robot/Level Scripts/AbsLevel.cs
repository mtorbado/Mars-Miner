using System.Collections;
using UnityEngine;

public abstract class AbsLevel : MonoBehaviour, ILevel {

    // Must be overrided
    public abstract IEnumerator Play();

    // public void Pause() {}

    // public void ForceStop() {}
}
