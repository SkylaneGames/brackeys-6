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
            var distanceFromCentre = (other.transform.position - transform.position).magnitude;
            var damagePortion = 1 - (distanceFromCentre / _radius);
            Debug.Log($"DistanceFromCentre: {distanceFromCentre}; Damage portion {damagePortion}");
            damageSystem.Damage(Damage * damagePortion);
        }

        if (rigidbody != null)
        {
            rigidbody.AddExplosionForce(_force, transform.position, _radius);
        }
    }

    public void Explode()
    {
        _sphereCollider.enabled = true;
    }
}
