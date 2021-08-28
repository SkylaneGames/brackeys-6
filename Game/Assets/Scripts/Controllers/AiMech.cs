using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class AiMech : AIController
{
    private CharacterController _characterController;
    private Animator _animator;
    private AudioSource _footsteps;

    [SerializeField]
    private float _animationSpeedScale = 0.15f;

    public override Vector3 Velocity => _characterController.velocity;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _footsteps = GetComponent<AudioSource>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        _animator.speed = NavAgent.velocity.magnitude * _animationSpeedScale;
        if (NavAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {

            _animator.SetBool("Walking", true);
        }
        else
        {
            _animator.SetBool("Walking", false);
        }
    }

    protected override void Attack(Vector3 target)
    {
        base.Attack(target);

        FireSecondary();
    }

    protected override void StopAttacking()
    {
        base.StopAttacking();

        StopFiringSecondary();
    }

    public void PlayFootstep()
    {
        _footsteps.Play();
    }
}
