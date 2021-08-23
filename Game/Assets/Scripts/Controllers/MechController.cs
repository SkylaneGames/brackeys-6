using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(WeaponLoadout))]
[RequireComponent(typeof(DamageSystem))]
public class MechController : UnitController
{
    private CharacterController _characterController;

    private Vector2 _positionInput = Vector2.zero;

    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        var move = transform.forward * _positionInput.y * _speed;
        move.y = -9.81f;
        transform.Rotate(Vector3.up, _positionInput.x * _turnSpeed, Space.World);
        _characterController.Move(move * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _positionInput = context.ReadValue<Vector2>();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Fire();
        }
        else if (context.canceled)
        {
            StopFiring();
        }
    }
}
