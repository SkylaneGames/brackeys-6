using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(AudioSource))]
    public class Laser : EnergyWeapon
    {
        [SerializeField]
        [Range(0f, 1000f)]
        private float _damagePerSecond = 10f;

        [SerializeField]
        [Range(0f, 1000f)]
        private float _energyDrainPerSecond = 10f;

        private LineRenderer _lineRenderer;
        private AudioSource _audio;

        protected override void Awake()
        {
            base.Awake();
            _lineRenderer = GetComponent<LineRenderer>();
            _audio = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (IsFiring)
            {
                Drain();
                if (Energy == 0)
                {
                    StopFiring();
                    return;
                }

                ShootRay();
            }
        }

        private void ShootRay()
        {
            Vector3 end;
            //Gizmos.DrawLine(transform.position, transform.position + transform.forward * 20f);
            if (Physics.Raycast(transform.position, transform.forward, out var hit, MaxRange))
            {
                end = hit.point;
                var damageSystem = hit.collider.GetComponent<DamageSystem>();
                if (damageSystem != null)
                {
                    damageSystem.Damage(_damagePerSecond * Time.deltaTime);
                }
            }
            else
            {
                end = transform.position + transform.forward * MaxRange;
            }

            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, end);
        }

        protected override void OnFire(Transform reticule)
        {
            _lineRenderer.enabled = true;
            _audio.Play();
        }

        protected override void OnStopFiring()
        {
            _lineRenderer.enabled = false;
            _audio.Stop();
        }

        private void Drain()
        {
            Discharge(_energyDrainPerSecond * Time.deltaTime);
        }
    } 
}
