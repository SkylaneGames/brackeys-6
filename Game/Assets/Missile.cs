using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Weapons;

[RequireComponent(typeof(Rigidbody))]
public class Missile : Projectile
{
    private VisualEffect _smokeEffect;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private Explosion _explosion;
    private Light _light;

    private float _collisionDelay = 0.5f;

    private float _timeAlive = 0f;

    private void Awake()
    {
        _smokeEffect = GetComponentInChildren<VisualEffect>();
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _explosion = GetComponentInChildren<Explosion>();
        _light = GetComponentInChildren<Light>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.velocity = transform.forward * _speed;
        _explosion.Damage = _damage;
    }

    // Update is called once per frame
    void Update()
    {
        _timeAlive += Time.deltaTime;
        _smokeEffect.SetVector3("Spawn Position", transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_timeAlive < _collisionDelay)
        {
            return;
        }

        if (!other.isTrigger)
        {
            _meshRenderer.enabled = false;
            enabled = false;
            _smokeEffect.Stop();
            _rigidbody.velocity = Vector3.zero;
            _light.enabled = false;

            _explosion.Explode();

            Destroy(gameObject, 5f);
        }
    }
}
