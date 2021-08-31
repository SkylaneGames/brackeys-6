using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseAimTarget : AimTarget
{
    private Transform _camera;

    protected override void Start()
    {
        base.Start();

        _camera = Camera.main.transform;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        //var ray = new Ray(Parent.transform.position, (_camera.forward + _camera.up * 0.1f).normalized);
        
        if (Physics.Raycast(ray, out var hit, Parent.MaxRange, LayerMask.GetMask("Units")))
        {
            if (hit.collider.GetComponent<UnitController>())
            {
                Target = hit.collider.transform.position;
            }
            else
            {
                Target = hit.point;
            }
        }
        else
        {
            Target = ray.GetPoint(Parent.MaxRange);
        }
    }
}
