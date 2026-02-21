using System;
using _Sim.Scripts;
using TMPro;
using UnityEngine;

public class IonLabelLevel : MonoBehaviour
{

    [SerializeField] private TMP_Text _label;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _label.text = "0";
        SimManager.Instance.OnParticaleChangedCount += OnPrticalChange;
    }

    private void OnPrticalChange(int countParticals)
    {
        _label.text = countParticals.ToString();
    
    }

    private void OnDestroy()
    {
        if (SimManager.Instance != null)
        {
            SimManager.Instance.OnParticaleChangedCount -= OnPrticalChange;
        }
    }
}
