using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float _radius = 2f;

    [SerializeField]
    private float _force = 10f;

    private SphereCollider _sphereCollider;

    public float Damage { get; set; }

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _sphereCollider.enabled = false;
        _sphereCollider.radius = _radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageSystem = other.GetComponent<DamageSystem>();
        var rigidbody = other.GetComponent<Rigidbody>();

        if (damageSystem != null)
        {
            Debug.Log($"Damaging '{other.name}' Force = {Damage}");
            damageSystem.Damage(Damage);
        }

        if (rigidbody != null)
        {
            Debug.Log($"Adding force to '{other.name}' Force = {Damage}");
            rigidbody.AddExplosionForce(_force, transform.position, _radius, 1f);
        }
    }

    public void Explode()
    {
        _sphereCollider.enabled = true;
    }
}
