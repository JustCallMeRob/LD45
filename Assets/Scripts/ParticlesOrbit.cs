using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticlesOrbit : MonoBehaviour
{

    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;

    [SerializeField]
    Transform CenterOfGravity;

    public float strength = 1.0f;

    void Start()
    {
        InitializeIfNeeded();
        if (CenterOfGravity == null)
            CenterOfGravity = transform;
    }

    private void LateUpdate()
    {
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 gravitation = new Vector3(0, 0, 0);
            if (m_System.simulationSpace == ParticleSystemSimulationSpace.World)
            {
                gravitation = CenterOfGravity.position - m_Particles[i].position;
            }
            else
            {
                gravitation = Vector3.zero - m_Particles[i].position;
            }

            Vector3 normalizedGravitation = Vector3.Normalize(gravitation);
            m_Particles[i].velocity += normalizedGravitation * strength;
        }

        m_System.SetParticles(m_Particles, numParticlesAlive);
    }

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.maxParticles];
    }
}
