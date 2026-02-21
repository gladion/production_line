using System;
using UnityEngine;

namespace _Sim.Scripts
{
    // Not loving this implementation, but good enough for now
    public class SimManager : MonoBehaviour
    {
        public static SimManager Instance;
        private ParticleSystem _cardParticleSystem;
        private int _collidedCount;
        private bool _isCardParticlesPositive;
        private bool _isCardParticlesActive;
        private event Action _onParticlesStopped;
        public event Action<int> OnParticaleChangedCount;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        public void SubscribeCardParticleSystem(ParticleSystem ps, Action onParticlesStopped)
        {
            _cardParticleSystem = ps;
            _onParticlesStopped  = onParticlesStopped;
        }

        public void UpdateLabelCount()
        {
            int count = _isCardParticlesPositive ? _cardParticleSystem.particleCount : -_cardParticleSystem.particleCount;
             OnParticaleChangedCount?.Invoke(count);
        }

        public void SetCardIsPositive(bool isPositive)
        {
            _isCardParticlesActive = true;
            _isCardParticlesPositive = isPositive;
        }

        public void OnParticlesCollided(bool isPositive, int collisionCount)
        {
            if (!_isCardParticlesActive || _cardParticleSystem.particleCount == 0 || collisionCount == 0)
            {
                return;
            }
            
            var isCanceling = ( isPositive && !_isCardParticlesPositive ) ||
                ( !isPositive && _isCardParticlesPositive );

            if (isCanceling)
            {
                _collidedCount += collisionCount;
                
                int collidedCalculation = _cardParticleSystem.particleCount - _collidedCount;
                collidedCalculation *= _isCardParticlesPositive ? 1 : -1;
               
                OnParticaleChangedCount?.Invoke(collidedCalculation);
                LogsManager.Instance.AddLog(collisionCount);
            }
            
            if (!_isCardParticlesActive || _cardParticleSystem.particleCount == 0)
            {
                return;
            }
            
            if (_collidedCount >= _cardParticleSystem.particleCount)
            {
                _collidedCount = 0;
                _isCardParticlesActive = false;
                _cardParticleSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                _onParticlesStopped?.Invoke();
                
                LogsManager.Instance.WriteLog();
            }
        }

        private void OnDestroy()
        {
            _cardParticleSystem = null;
            _onParticlesStopped = null;
        }
    }
}