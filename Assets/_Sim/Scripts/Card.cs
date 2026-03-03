using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Sim.Scripts
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private bool _isPositive;
        [SerializeField] private float _minWaitTime = 2;
        [SerializeField] private float _maxWaitTime = 5;
        [SerializeField] private Color _plusColor = Color.blue;
        [SerializeField] private Color _minusColor = Color.red;

        private bool _isWaiting;
        private ParticleSystem.MainModule _mainModule;
        
        private void Start()
        {
            _mainModule = _particleSystem.main;
            SimManager.Instance.SubscribeCardParticleSystem(_particleSystem, OnParticlesStopped);
            IsPositive();
            SimManager.Instance.SetCardIsPositive(_isPositive);
            
            _particleSystem.Play();
            SimManager.Instance.UpdateIonLabelCount();
        }

        private void OnParticlesStopped()
        {
            RestartTimer().Forget();
        }

        private async UniTaskVoid RestartTimer()
        {
            if (_isWaiting)
            {
                return;
            }
            _isWaiting = true;

            // wait for random time, and then select random positive/negative charge
            var randWaitTime = Random.Range(_minWaitTime, _maxWaitTime);
            await UniTask.Delay(TimeSpan.FromSeconds(randWaitTime), cancellationToken: this.GetCancellationTokenOnDestroy());
           
            IsPositive();            
            SimManager.Instance.SetCardIsPositive(_isPositive);

            _particleSystem.Play();
            SimManager.Instance.UpdateIonLabelCount();

            _isWaiting = false;
        }

        private void IsPositive()
        {
            _isPositive = Random.Range(0, 2) == 0;
            _mainModule.startColor = _isPositive ? _plusColor : _minusColor;
        }
    }
}