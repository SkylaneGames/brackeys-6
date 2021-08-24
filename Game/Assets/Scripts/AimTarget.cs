using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

public class AimTarget : MonoBehaviour
{
    [SerializeField]
    private WeaponLoadout _parent;

    //[SerializeField]
    private float _minRadius;

    //[SerializeField]
    private float _maxRadius;

    [SerializeField]
    [Range(0f, 1f)]
    private float _targetSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        _minRadius = _parent.MinRange;
        _maxRadius = _parent.MaxRange;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hit))
        {
            var target = hit.point;

            var toVector = target - _parent.transform.position;

            var magnitude = toVector.magnitude;

            var newPoint = target;
            if (magnitude > _maxRadius)
            {
                newPoint = _parent.transform.position + toVector.normalized * _maxRadius;
            }
            else if (magnitude < _minRadius)
            {
                newPoint = _parent.transform.position + toVector.normalized * _minRadius;
            }


            transform.position = Vector3.Lerp(transform.position, newPoint, _targetSpeed);
        }
    }
}
