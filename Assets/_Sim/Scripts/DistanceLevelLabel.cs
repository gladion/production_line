using System;
using _Sim.Scripts;
using TMPro;
using UnityEngine;

public class DistanceLevelLabel : MonoBehaviour
{

    [SerializeField] private TMP_Text _label;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _label.text = "Dist: 0";
        SimManager.Instance.OnDistanceChanged += OnDistanceChanged;
    }

    private void OnDistanceChanged(float distance)
    {
        _label.text = "Dist: " + distance.ToString("#.00");
    }

    private void OnDestroy()
    {
        if (SimManager.Instance != null)
        {
            SimManager.Instance.OnDistanceChanged -= OnDistanceChanged;
        }
    }
}
