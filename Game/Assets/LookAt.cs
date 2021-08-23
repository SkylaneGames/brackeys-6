using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    private Transform _lookTarget;

    [SerializeField]
    private float _speed = 0.1f;

    //[SerializeField]
    //private bool _yOnly = false;

    [SerializeField]
    private Vector3 _axis;

    [SerializeField]
    private Vector3 _forward = Vector3.up;

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

        //if (_yOnly)
        //{
        //    vectorToTarget.y = 0;
        //}

        Vector3 directionToTarget;
        if (_axis != Vector3.zero)
        {
            directionToTarget = new Vector3(vectorToTarget.x * _axis.x, vectorToTarget.y * _axis.y, vectorToTarget.z * _axis.z).normalized;
        }
        else
        {
            directionToTarget = vectorToTarget.normalized;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.FromToRotation(_forward, directionToTarget), _speed);
    }
}
