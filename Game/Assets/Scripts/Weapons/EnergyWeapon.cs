using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public abstract class EnergyWeapon : WeaponBase
    {
        private EnergySystem _energySystem;

        public override bool CanFire => _energySystem.Value > 0;
        protected float Energy => _energySystem.Value;

        protected override void Awake()
        {
            base.Awake();

            _energySystem = GetComponentInParent<EnergySystem>();
        }

        protected void Discharge(float amount) => _energySystem.Discharge(amount);
    }
}
