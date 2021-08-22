using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    private Transform _lookTarget;

    [SerializeField]
    private float _speed = 0.1f;

    private void Start()
    {
        if (_lookTarget == null)
        {
            Debug.LogWarning($"No look target assigned to mech '{name}'");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var vectorToTarget = _lookTarget.position - transform.position;
        vectorToTarget.y = 0;
        var directionToTarget = vectorToTarget.normalized;
        var rotation = Vector3.SignedAngle(Vector3.forward, directionToTarget, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(rotation, Vector3.up), _speed);
    }
}
