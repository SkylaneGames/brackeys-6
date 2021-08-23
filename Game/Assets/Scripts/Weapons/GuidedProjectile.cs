using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class GuidedProjectile : Projectile
    {
        private Vector3 _target;

        public void SetTarget(Vector3 target)
        {
            _target = target;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
