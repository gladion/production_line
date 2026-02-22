using System.Collections.Generic;
using UnityEngine;

namespace _Sim.Scripts
{
    public class ParticleSystemEmitterListener : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private bool _isPositive;
        private List<ParticleCollisionEvent> _collisionEvents =  new List<ParticleCollisionEvent>();

        private void OnParticleCollision(GameObject other)
        {
            
            _particleSystem.GetCollisionEvents(other, _collisionEvents);
        
            if (_collisionEvents.Count != 0)
            {
                SimManager.Instance.OnParticlesCollided(_isPositive, _collisionEvents.Count);
            }
        }
    }
}