using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseAimTarget : AimTarget
{
    protected override void Update()
    {
        base.Update();

        var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out var hit, float.MaxValue, LayerMask.GetMask("Default", "Units")))
        {
            Target = hit.point;
        }
    }
}
