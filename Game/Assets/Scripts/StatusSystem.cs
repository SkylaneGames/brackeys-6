using System;
using UnityEngine;

public abstract class StatusSystem : MonoBehaviour
{
    public event EventHandler<float> ValueChanged;

    [SerializeField]
    private float _maxValue = 100f;
    public float MaxValue => _maxValue;

    private float _value;

    public float Value
    {
        get => _value;
        protected set
        {
            _value = Mathf.Clamp(value, 0, MaxValue);
            ValueChanged?.Invoke(this, Value);
        }
    }

    protected virtual void Start()
    {
        Value = MaxValue;
    }

    protected virtual void Awake() { }
    protected virtual void Update() { }
}
