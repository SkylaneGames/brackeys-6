using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkTarget : MonoBehaviour
{
    [SerializeField]
    private Color _color = Color.green;

    [SerializeField]
    private float _radius = 0.25f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
