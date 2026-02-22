using System;
using _Sim.Scripts;
using TMPro;
using UnityEngine;

public class SpeedLevelLabel : MonoBehaviour
{

    [SerializeField] private TMP_Text _label;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _label.text = "Speed: 0";
        SimManager.Instance.OnSpeedChanged += OnSpeedChanged;
    }

    private void OnSpeedChanged(float speed)
    {
        _label.text = "Speed: " + speed.ToString("#.00");
    }

    private void OnDestroy()
    {
        if (SimManager.Instance != null)
        {
            SimManager.Instance.OnSpeedChanged -= OnSpeedChanged;
        }
    }
}
