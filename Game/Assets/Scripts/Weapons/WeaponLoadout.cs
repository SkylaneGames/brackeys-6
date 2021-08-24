using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Weapons
{
    public class WeaponLoadout : MonoBehaviour
    {
        [SerializeField]
        private List<WeaponBase> _weapons = new List<WeaponBase>();

        [SerializeField]
        private List<WeaponGroup> _weaponGroups = new List<WeaponGroup>();

        public bool CanFire => _weapons.Any(p => p.CanFire);

        public float MinRange => _weapons.Min(p => p.MinRange);
        public float MaxRange => _weapons.Max(p => p.MaxRange);

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FireWeapon(int group)
        {
            foreach (var index in _weaponGroups.Single(p => p.GroupIndex == group).WeaponIndexes)
            {
                _weapons[index].Fire();
            }
        }

        public void StopFiringWeapon(int group)
        {
            foreach (var index in _weaponGroups.Single(p => p.GroupIndex == group).WeaponIndexes)
            {
                _weapons[index].StopFiring();
            }
        }

        private void OnDrawGizmosSelected()
        {
            foreach (var weapon in _weapons)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(weapon.transform.position, weapon.MinRange);
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(weapon.transform.position, weapon.MaxRange);
            }
        }
    }

    [Serializable]
    public class WeaponGroup
    {
        public int GroupIndex = 0;
        public List<int> WeaponIndexes = new List<int>();
    }
}
