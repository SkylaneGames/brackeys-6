using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(WeaponLoadout))]
[RequireComponent(typeof(DamageSystem))]
[RequireComponent(typeof(Animator))]
public class MechController : UnitController
{
    private CharacterController _characterController;
    private Animator _animator;
    private AudioSource _footsteps;

    private Vector2 _positionInput = Vector2.zero;

    [SerializeField]
    private float _animationSpeedScale = 0.15f;

    public override Vector3 Velocity => _characterController.velocity;

    private Vector3 _gravityAccel = Vector3.zero;

    [SerializeField]
    private float _gravityScale = 1f;

    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _footsteps = GetComponent<AudioSource>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!_characterController.isGrounded)
        {
            _gravityAccel += Physics.gravity * Time.fixedDeltaTime * _gravityScale;
        }
        else
        {
            _gravityAccel = Vector3.zero;
        }

        var move = transform.forward * _positionInput.y * _speed;
        move += _gravityAccel;
        transform.Rotate(Vector3.up, _positionInput.x * _turnSpeed, Space.World);
        _characterController.Move(move * Time.deltaTime);
        
        _animator.speed = _characterController.velocity.magnitude * _animationSpeedScale;
        if (_positionInput.sqrMagnitude > 0)
        {
            if (_positionInput.y == 0)
            {
                _animator.speed = 1f;
            }
            _animator.SetBool("Walking", true);
        }
        else
        {
            _animator.SetBool("Walking", false);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        _positionInput = context.ReadValue<Vector2>();
    }

    public void PlayFootstep()
    {
        _footsteps.Play();
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

    public void FireSecondary(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FireSecondary();
        }
        else if (context.canceled)
        {
            StopFiringSecondary();
        }
    }
}
