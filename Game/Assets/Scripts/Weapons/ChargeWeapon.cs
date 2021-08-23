using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class ChargeWeapon : WeaponBase
    {
        [SerializeField]
        [Range(0f, 1f)]
        private float _energyRegenPerSecond = 0.05f;
        
        private float _energy;
        public float Energy
        {
            get => _energy;
            set
            {
                _energy = Mathf.Clamp01(value);
            }
        }

        public override bool CanFire => Energy > 0;

        public void Charge(float amount)
        {
            Energy += amount;
        }

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Energy = 1f;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
            if (!IsFiring)
            {
                Regen();
            }
        }

        private void Regen()
        {
            Energy += _energyRegenPerSecond * Time.deltaTime;
        }
    }
}
