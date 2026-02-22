using UnityEngine;
using UnityEngine.UI;

namespace _Sim.Scripts
{
    public class FanPositionController : MonoBehaviour
    {
        [SerializeField] private Slider _rotationSlider;
        [SerializeField] private Slider _distanceSlider;

        [SerializeField] private Transform _center;
        [SerializeField] private Transform _fan;

        [SerializeField] private float _maxDistance = 10;
        
        private float _currentRotation;
        private float _currentDistance;
        private Vector3 _initialLocalPosition;
        private float _initialRotation;

        private void Start()
        {
            _initialLocalPosition = _fan.localPosition;
            
            _distanceSlider.minValue = 0f;
            _distanceSlider.maxValue = 1f; // Normalized (0-1)

            // Set distance slider to match initial distance
            _currentDistance = _fan.localPosition.x;
            SimManager.Instance.UpdateDistance(_currentDistance);
            _distanceSlider.value = Mathf.Clamp01(_currentDistance / _maxDistance);
            _distanceSlider.onValueChanged.AddListener(OnDistanceChanged);
            UpdateFanDistance();

            // Store the initial rotation of the centre
            _initialRotation = _center.rotation.eulerAngles.y;

            // Set up slider ranges
            _rotationSlider.minValue = 0f;
            _rotationSlider.maxValue = 1f; // Normalized (0-1)

            // Set rotation slider to 0 to maintain initial rotation
            _rotationSlider.value = 0f;
            _currentRotation = _initialRotation;

            // Add separate listeners for each slider
            _rotationSlider.onValueChanged.AddListener(OnRotationChanged);
            
            // Set initial position and rotation
            UpdateCentreRotation();
        }

        private void OnRotationChanged(float sliderValue)
        {
            _currentRotation = MapSliderToRotation(sliderValue);
            UpdateCentreRotation();
        }

        private void OnDistanceChanged(float normalizedDistance)
        {
            _currentDistance = normalizedDistance * _maxDistance;
            SimManager.Instance.UpdateDistance(_currentDistance);
            UpdateFanDistance();
        }

        private void UpdateCentreRotation()
        {
            // Rotate the center around its Y axis
            _center.rotation = Quaternion.Euler(0f, _currentRotation, 0f);
        }

        private void UpdateFanDistance()
        {
            // Move the fan along its local forward direction (Z axis)
            _fan.localPosition = new Vector3(_currentDistance, _initialLocalPosition.y, _initialLocalPosition.z);
        }
        
        private float MapSliderToRotation(float sliderValue)
        {
            // Map slider value to rotation:
            // 0.0 -> 360°, 1.0 -> 180°
        
            return Mathf.Lerp(360f, 180f, sliderValue);
        }

        private void OnDestroy()
        {
            // Clean up listeners
            _rotationSlider.onValueChanged.RemoveListener(OnRotationChanged);
            _distanceSlider.onValueChanged.RemoveListener(OnDistanceChanged);
        }
    }
}