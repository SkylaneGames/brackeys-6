using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private Vector3 _offset;
    private Quaternion _rotationOffset;

    [SerializeField]
    private float trackingSpeed = 3f;
    
    [SerializeField]
    private float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - _target.position;
        _rotationOffset = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_target == null) return;

        var position = _target.position + _target.rotation * _offset;

        transform.position = Vector3.Lerp(transform.position, position, trackingSpeed * Time.fixedDeltaTime);

        var rotation = _target.rotation * _rotationOffset;
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
