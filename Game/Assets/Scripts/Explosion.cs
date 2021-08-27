using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Explosion : MonoBehaviour
{
    [SerializeField]
    public float Radius = 3f;

    [SerializeField]
    public float Force = 10f;

    [SerializeField]
    public float Damage = 10f;

    //[SerializeField]
    //private float _destroyAfter = 3f;

    [SerializeField]
    private AudioClip[] _explosionSounds;

    private AudioSource _audio;
    private ParticleSystem _particleSystem;

    public Vector3 OriginDirection;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        if (_explosionSounds == null || _explosionSounds.Length == 0)
        {
            Debug.LogWarning($"No audio clips on explosion '{name}'");
        }
        else
        {
            _audio.clip = _explosionSounds[Random.Range(0, _explosionSounds.Length)];
            _audio.Play();
        }

        Destroy(gameObject, _particleSystem.main.duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageSystem = other.GetComponent<DamageSystem>();
        var rigidbody = other.GetComponent<Rigidbody>();

        if (damageSystem != null)
        {
            var distanceFromCentre = (other.transform.position - transform.position).magnitude;
            var damagePortion = 1 - (distanceFromCentre / Radius);
            //Debug.Log($"DistanceFromCentre: {distanceFromCentre}; Damage portion {damagePortion}");
            damageSystem.Damage(Damage * damagePortion, OriginDirection);
        }

        if (rigidbody != null)
        {
            rigidbody.AddExplosionForce(Force, transform.position, Radius);
        }
    }
}
