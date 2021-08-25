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
    private Explosion _explosion;
    private MeshRenderer _meshRenderer;
    private Light _light;

    [SerializeField]
    private float _radius = 3f;

    [SerializeField]
    private float _force = 50f;

    [SerializeField]
    private Explosion Explosion;

    private float _collisionDelay = 0.1f;

    private float _timeAlive = 0f;

    private void Awake()
    {
        _smokeEffect = GetComponentInChildren<VisualEffect>();
        _rigidbody = GetComponent<Rigidbody>();
        _explosion = GetComponentInChildren<Explosion>();
        _meshRenderer = GetComponent<MeshRenderer>();
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

        if (_smokeEffect != null)
        {
            _smokeEffect.SetVector3("Spawn Position", transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_timeAlive < _collisionDelay)
        {
            return;
        }

        if (!other.isTrigger)
        {
            var explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
            explosion.Damage = _damage;
            explosion.Radius = _radius;
            explosion.Force = _force;

            _rigidbody.velocity = Vector3.zero;
            _smokeEffect.Stop();
            _meshRenderer.enabled = false;
            _light.enabled = false;

            Destroy(gameObject, 4f);
        }
    }
}
