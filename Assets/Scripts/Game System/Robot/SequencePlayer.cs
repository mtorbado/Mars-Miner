using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SequencePlayer : MonoBehaviour
{
    
    public IEnumerator PlaySequence(List<CoroutineWithData> sequence)
    {
        foreach (CoroutineWithData cd in sequence)
        {
          yield return cd.coroutine;
        }
    }

    public IEnumerator WhileLoop(List<CoroutineWithData> sequence, bool interrupt)
    {
        while(!interrupt)
        {
           yield return PlaySequence(sequence);
        }
    }
}