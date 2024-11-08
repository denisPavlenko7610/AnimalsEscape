using RDTools.AutoAttach;
using System;
using UnityEngine;

public class ParticleSystemStoppedHandler : MonoBehaviour
{
    [SerializeField, Attach] ParticleSystem _particleSystem;

    public event Action<ParticleSystem> onParticleStopped;
    
    void Awake()
    {
        ParticleSystem.MainModule main = _particleSystem.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        onParticleStopped?.Invoke(_particleSystem);
    }
}
