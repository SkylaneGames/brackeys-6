using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        protected float _damage = 10f;

        [SerializeField]
        protected float _speed = 10f;
    } 
}
