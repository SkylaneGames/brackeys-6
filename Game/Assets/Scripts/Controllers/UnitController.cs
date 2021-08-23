using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(WeaponLoadout))]
[RequireComponent(typeof(DamageSystem))]
public class UnitController : MonoBehaviour
{
    private WeaponLoadout _weapons;
    private DamageSystem _damageSystem;

    [SerializeField]
    [Range(0f, 50f)]
    protected float _speed = 5f;

    [SerializeField]
    [Range(0f, 5f)]
    protected float _turnSpeed = 1f;

    protected virtual void Awake()
    {
        _weapons = GetComponent<WeaponLoadout>();
        _damageSystem = GetComponent<DamageSystem>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _damageSystem.Destroyed += OnDestroyed;
    }

    private void OnDestroyed()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {

    }

    public void Fire()
    {
        _weapons.FireWeapon(0);
    }

    public void StopFiring()
    {
        _weapons.StopFiringWeapon(0);
    }
}
