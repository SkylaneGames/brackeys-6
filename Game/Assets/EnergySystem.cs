using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : StatusSystem
{
    [SerializeField]
    [Range(0f, 1000f)]
    private float _regenPerSecond = 5f;

    [SerializeField]
    [Range(0f, 10f)]
    private float _regenDelay = 1f;

    private float _lastDrain;

    // Update is called once per frame
    protected override void Update()
    {
        if (Time.time > _lastDrain + _regenDelay)
        {
            Value += _regenPerSecond * Time.deltaTime;
        }
    }

    public void Charge(float amount)
    {
        Value += amount;
    }

    public void Discharge(float amount)
    {
        Value -= amount;
        _lastDrain = Time.time;
    }
}
