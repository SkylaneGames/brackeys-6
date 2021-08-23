using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public enum FireMode
    {
        Single, Burst, Automatic, Continuous
    }

    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField]
        private string _name;
        public string Name => _name;

        [SerializeField]
        private float _maxRange = 50f;
        public float MaxRange => _maxRange;

        [SerializeField]
        private float _minRange = 5f;
        public float MinRange => _minRange;

        [SerializeField]
        private FireMode _firingMode = FireMode.Single;
        protected FireMode FiringMode => _firingMode;

        public abstract bool CanFire { get; }
        protected bool IsFiring { get; private set; }

        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void Update() { }

        public void Fire(Transform reticule = null)
        {
            if (CanFire)
            {
                IsFiring = true;
                OnFire(reticule);
            }
        }

        public void StopFiring()
        {
            OnStopFiring();
            IsFiring = false;
        }

        protected abstract void OnFire(Transform reticule);

        protected abstract void OnStopFiring();
    }
}