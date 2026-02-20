using UnityEngine;

namespace _Sim.Scripts
{
    public class ParticleSystemEmitterListener : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private bool _isPositive;

        private void OnParticleCollision(GameObject other)
        {
            SimManager.Instance.OnParticlesCollided(_isPositive);
        }
    }
}