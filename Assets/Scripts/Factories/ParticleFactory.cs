using UnityEngine;

public class ParticleFactory : MonoBehaviour
{
    public void InstantiateParticleEffect(Transform transform, ParticleSystem particleEffect)
    {
        ParticleSystem particle = Instantiate(particleEffect, transform.position, transform.rotation);
        particle.Play();
    }
}
