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
            
            _particleSystem.GetCollisionEvents(this.gameObject, _collisionEvents);
            //ParticlePhysicsExtensions.GetCollisionEvents(_particleSystem, this.gameObject, _collisionEvents);
            if (_collisionEvents.Count != 0)
            {
                SimManager.Instance.OnParticlesCollided(_isPositive, _collisionEvents.Count);
            }

            SimManager.Instance.OnParticlesCollided(_isPositive, 1);
            
        }
    }
}