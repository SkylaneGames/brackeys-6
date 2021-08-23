using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneConstaint : MonoBehaviour
{
    [SerializeField]
    private bool _constrainX;

    [SerializeField]
    private bool _constrainY;
    
    [SerializeField]
    private bool _constrainZ;

    [SerializeField]
    private float _minX;

    [SerializeField]
    private float _maxX;

    [SerializeField]
    private float _minY;

    [SerializeField]
    private float _maxY;

    [SerializeField]
    private float _minZ;

    [SerializeField]
    private float _maxZ;

    [SerializeField]
    private Transform _lookAt;

    private Vector3 _offset;
    private Quaternion _initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.localEulerAngles;
        _initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_lookAt != null)
        {
            transform.LookAt(_lookAt);
            transform.localRotation = transform.localRotation * _initialRotation;
        }

        var rotation = transform.localEulerAngles;
        if (_constrainX)
        {
            rotation.x = _offset.x;
        }
        else if (_minX != 0 || _maxX != 0)
        {
            rotation.x = _offset.x + Mathf.Clamp(rotation.x - _offset.x, _minX, _maxX);
        }

        if (_constrainY)
        {
            rotation.y = _offset.y;
        }
        else if (_minY != 0 || _maxY != 0)
        {
            rotation.y = _offset.y + Mathf.Clamp(rotation.y - _offset.y, _minY, _maxY);
        }

        if (_constrainZ)
        {
            rotation.z = _offset.z;
        }
        else if (_minZ != 0 || _maxZ != 0)
        {
            rotation.z = _offset.z + Mathf.Clamp(rotation.z - _offset.z, _minZ, _maxZ);
        }

        transform.localEulerAngles = rotation;
    }
}
