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
    }

    [Serializable]
    public class WeaponGroup
    {
        public int GroupIndex = 0;
        public List<int> WeaponIndexes = new List<int>();
    }
}
