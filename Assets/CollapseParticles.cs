using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseParticles : MonoBehaviour
{
    public ParticleSystem debris;

    public void PlayCollapse()
    {
        debris.Play();
    }
}

