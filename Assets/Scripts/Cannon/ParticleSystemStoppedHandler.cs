using System;
using UnityEngine;

public class ParticleSystemStoppedHandler : MonoBehaviour
{
    private ParticleSystemPool pool;

    private void Awake()
    {
        var _ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = _ps.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    public void Init(ParticleSystemPool pool)
    {
        this.pool = pool;
    }

    void OnParticleSystemStopped()
    {
        pool.ReleaseParticleSystem(GetComponent<ParticleSystem>());
    }
}
