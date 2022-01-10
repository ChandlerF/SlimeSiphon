using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    private ParticleSystem Particle;

    void Start()
    {
        Particle = GetComponent<ParticleSystem>();

        Invoke("PauseParticles", 0.15f);
    }

    private void PauseParticles()
    {
        Particle.Pause();
    }
}
