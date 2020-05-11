using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SequencePlayer : MonoBehaviour
{
    // Coroutines without params or return (not using CoroutineWithData)
    private IEnumerator PlaySequence(List<String> methodSequence)
    {
        foreach (String method in methodSequence)
        {
           yield return StartCoroutine(method);
        }
    }

    void WhileLoop(List<String> methodSequence)
    {
        while(true)
        {
            PlaySequence(methodSequence);
        }
    }
}
