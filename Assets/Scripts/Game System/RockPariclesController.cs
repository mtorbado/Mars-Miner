﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPariclesController : MonoBehaviour
{

    ParticleSystem particles;
    private void Start() 
    {
        particles = GetComponentInChildren<ParticleSystem>();
        particles.Stop();
    }
    private void OnMouseDown()
    {
        particles.Play();
    }

    private void OnMouseUp()
    {
        particles.Stop();
    }

}