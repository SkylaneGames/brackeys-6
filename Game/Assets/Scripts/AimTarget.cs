using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

public class AimTarget : MonoBehaviour
{
    [SerializeField]
    private WeaponLoadout _parent;

    protected WeaponLoadout Parent => _parent;

    protected float MinDistanceFromParent => Parent.MinRange;
    protected float MaxDistanceFromParent => Parent.MaxRange;

    private Vector3 _target;
    public Vector3 Target
    {
        get => _target;
        set
        {
            var toVector = value - Parent.transform.position;

            var magnitude = toVector.magnitude;

            _target = value;
            if (magnitude > MaxDistanceFromParent)
            {
                _target = Parent.transform.position + toVector.normalized * MaxDistanceFromParent;
            }
            else if (magnitude < MinDistanceFromParent)
            {
                _target = Parent.transform.position + toVector.normalized * MinDistanceFromParent;
            }
        }
    }

    [SerializeField]
    [Range(0f, 1f)]
    private float _targetSpeed = 0.05f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target, _targetSpeed);
    }
}
