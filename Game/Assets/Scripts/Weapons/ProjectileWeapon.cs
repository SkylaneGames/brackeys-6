using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class ProjectileWeapon : WeaponBase
    {
        [SerializeField]
        [Range(0, 1000)]
        private int _maxAmmo = 50;
        public int MaxAmmo => _maxAmmo;

        [SerializeField]
        private bool _infiniteAmmo;

        [SerializeField]
        [Range(0, 1000)]
        private int _clipSize = 10;
        public int ClipSize => _clipSize;

        [SerializeField]
        [Range(0, 50)]
        [Tooltip("Rate of fire (per second)")]
        private float _fireRate = 2;
        protected float FireRate => _fireRate;
        protected float FireDelay => 1 / FireRate;

        [SerializeField]
        [Range(0f, 20f)]
        private float _reloadTime = 5f;
        public float ReloadTime => _reloadTime;

        [SerializeField]
        private Transform[] _spawnPoints = null;

        private Transform SpawnPoint
        {
            get
            {
                if (_spawnPoints == null || _spawnPoints.Length == 0)
                {
                    return transform;
                }

                return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            }
        }

        [SerializeField]
        private Projectile _projectilePrefab;

        public float ReloadTimeRemaining { get; private set; }

        public int CurrentAmmo { get; private set; }
        public int CurrentClip { get; private set;}

        public override bool CanFire => _infiniteAmmo && _timeSinceLastFire >= FireDelay || (CurrentClip > 0 && CurrentAmmo > 0);

        private float _timeSinceLastFire = 0f;
        

        protected override void OnFire(Transform reticule)
        {
            if (FiringMode == FireMode.Single || FiringMode == FireMode.Burst)
            {
                if (_timeSinceLastFire >= FireDelay)
                {
                    LaunchProjectile(reticule);
                }
            }
        }

        protected override void OnStopFiring()
        {

        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            CurrentClip = ClipSize;
            CurrentAmmo = MaxAmmo;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

            if (IsFiring)
            {
                if (FiringMode == FireMode.Automatic && _timeSinceLastFire >= FireDelay)
                {
                    LaunchProjectile(null);
                }
            }

            _timeSinceLastFire += Time.deltaTime;
        }

        private void LaunchProjectile(Transform reticule)
        {
            var spawnPoint = SpawnPoint;
            var projectile = Instantiate(_projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            // Standard projectile simply follows a direction after after being spawned
            // Guilded projectile will home in on a fixed position
            // TODO: Homing Projectile would take in a transform (create a new weapon type which can create list of target locks) which it would home in on.
            if (projectile is GuidedProjectile guidedProjectile)
            {
                guidedProjectile.SetTarget(reticule.position);
            }

            _timeSinceLastFire = 0f;
        }
    } 
}
