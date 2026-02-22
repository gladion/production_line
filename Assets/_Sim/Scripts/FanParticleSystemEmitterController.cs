using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Sim.Scripts
{
    public class FanParticleSystemEmitterController : MonoBehaviour
    {
        [SerializeField] private Slider _speedSlider;
        [SerializeField] private float _initSpeed;
        [SerializeField] private float _maxSpeed = 10;
        [SerializeField] private Slider _distributionSlider;
        [SerializeField] private TMP_Text _distributionTextPlus;
        [SerializeField] private TMP_Text _distributionTextMinus;
        [SerializeField] private float _initDistribution = 0.5f;

        [Header("Particle system")]
        [SerializeField] private ParticleSystem _psPlus;
        [SerializeField] private ParticleSystem _psMinus;
        [SerializeField] private int _particlesEmissionAmount = 100;

        private ParticleSystem.MainModule _plusMainModule;
        private ParticleSystem.MainModule _minusMainModule;
        
        private ParticleSystem.EmissionModule _plusEmissionModule;
        private ParticleSystem.EmissionModule _minusEmissionModule;
        
        private bool _isPlaying;
        
        private void Awake()
        {
            _speedSlider.minValue = 0;
            _speedSlider.maxValue = _maxSpeed;
            _speedSlider.value = _initSpeed;
            
            _distributionSlider.minValue = 0;
            _distributionSlider.maxValue = 1;
            _distributionSlider.value = _initDistribution;

            _speedSlider.onValueChanged.AddListener(OnSpeedSliderChange);
            _distributionSlider.onValueChanged.AddListener(OnDistributionSliderChange);
            
            _plusMainModule = _psPlus.main;
            _minusMainModule = _psMinus.main;
            
            _plusEmissionModule = _psPlus.emission;
            _minusEmissionModule = _psMinus.emission;
            
            OnDistributionSliderChange(_distributionSlider.value);
            OnSpeedSliderChange(_initSpeed);
        }

        private void OnDistributionSliderChange(float sliderValue)
        {
            var plusEmission = (int)(sliderValue * _particlesEmissionAmount);
            var minusEmission = _particlesEmissionAmount - plusEmission;
            _distributionTextPlus.text =  ((1 - sliderValue) * 100).ToString("N2") + "%";    
            _distributionTextMinus.text = (sliderValue * 100).ToString("N2") + "%";    
            
            _plusEmissionModule.rateOverTime = plusEmission;
            _minusEmissionModule.rateOverTime = minusEmission;
        }

        private void OnSpeedSliderChange(float sliderValue)
        {
            SimManager.Instance.UpdateDistance(sliderValue);

            if (sliderValue == 0)
            {
                _psPlus.Stop();
                _psMinus.Stop();
                _isPlaying = false;
                return;
            }

            if (!_isPlaying)
            {
                _psPlus.Play();
                _psMinus.Play();
                _isPlaying = true;
            }
            
            _plusMainModule.startSpeed = sliderValue;
            _minusMainModule.startSpeed = sliderValue;
        }

        private void OnDestroy()
        {
            _speedSlider.onValueChanged.RemoveListener(OnSpeedSliderChange);
            _distributionSlider.onValueChanged.RemoveListener(OnDistributionSliderChange);
        }
    }
}
