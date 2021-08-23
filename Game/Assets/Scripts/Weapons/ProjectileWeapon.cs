using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class ProjectileWeapon : WeaponBase
    {
        [SerializeField]
        private int _maxAmmo = 50;
        public int MaxAmmo => _maxAmmo;

        [SerializeField]
        private int _clipSize = 10;
        public int ClipSize => _clipSize;

        [SerializeField]
        [Tooltip("Rate of fire (per second)")]
        private float _fireRate;
        protected float FireRate => _fireRate;

        [SerializeField]
        [Range(0f, 20f)]
        private float _reloadTime = 5f;
        public float ReloadTime => _reloadTime;

        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        private Projectile _projectilePrefab;

        public float ReloadTimeRemaining { get; private set; }

        public int CurrentAmmo { get; private set; }
        public int CurrentClip { get; private set;}

        public override bool CanFire => throw new System.NotImplementedException();

        protected override void OnFire(Transform reticule)
        {
            var projectile = Instantiate(_projectilePrefab, _spawnPoint.position, _spawnPoint.rotation);
            
            // Standard projectile simply follows a direction after after being spawned
            // Guilded projectile will home in on a fixed position
            // TODO: Homing Projectile would take in a transform (create a new weapon type which can create list of target locks) which it would home in on.
            if (projectile is GuidedProjectile guidedProjectile)
            {
                guidedProjectile.SetTarget(reticule.position);
            }
        }

        protected override void OnStopFiring()
        {

        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }
    } 
}
