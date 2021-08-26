using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public enum Faction
{
    Team_1, Team_2
}

[RequireComponent(typeof(WeaponLoadout))]
[RequireComponent(typeof(DamageSystem))]
public abstract class UnitController : MonoBehaviour
{
    public event EventHandler Destroyed;

    private WeaponLoadout _weapons;
    protected WeaponLoadout Weapons => _weapons;

    private DamageSystem _damageSystem;

    [SerializeField]
    [Range(0f, 50f)]
    protected float _speed = 5f;

    [SerializeField]
    [Range(0f, 5f)]
    protected float _turnSpeed = 1f;

    [SerializeField]
    private Faction _faction;
    public Faction Faction => _faction;

    [SerializeField]
    private ParticleSystem _explosionEffect;

    public abstract Vector3 Velocity { get; }

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
        Destroyed?.Invoke(this, null);
        var explosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        Destroy(explosion, explosion.main.duration);
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

    public void FireSecondary()
    {
        _weapons.FireWeapon(1);
    }

    public void StopFiringSecondary()
    {
        _weapons.StopFiringWeapon(1);
    }
}
