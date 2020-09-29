using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileFirer))]
public class WizardFireballChargeParticles : MonoBehaviour
{
    [SerializeField]
    private float maxParticleRate;
    [SerializeField]
    private float particleRateIncreaseRate = 1.1f;
    [SerializeField]
    private int maxParticles;
    [SerializeField]
    private int particleIncreateRate;

    private ProjectileFirer fireballFirer;
    private new ParticleSystem particleSystem;

    private float particleSystemRate = 0.1f;
    private int particleSystemParticleCount = 1;

    void Awake()
    {
        fireballFirer = GetComponent<ProjectileFirer>();
        particleSystem = GetComponentInChildren<ParticleSystem>();

        fireballFirer.Fired += () =>
        {
            particleSystemRate = 1f;
            particleSystemParticleCount = 1;
            particleSystem.Stop();
            particleSystem.Play();
        };

        UpdateEmissionRate();

        UpdateParticleCount();
    }

    private void FixedUpdate()
    {
        particleSystemRate *= particleRateIncreaseRate;

        UpdateEmissionRate();

        particleSystemParticleCount += particleIncreateRate;

        UpdateParticleCount();
    }

    private void UpdateEmissionRate()
    {
        var emission = particleSystem.emission;
        emission.rateOverTime = Mathf.Min(particleSystemRate, maxParticleRate);
    }

    private void UpdateParticleCount()
    {
        var main = particleSystem.main;
        main.maxParticles = Mathf.Min(particleSystemParticleCount, maxParticles);
    }

}
