using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : StatusSystem
{
    public event Action Destroyed;

    public void Damage(float amount)
    { 
        Value -= amount;

        if (Value == 0)
        {
            Destroyed?.Invoke();
        }
    }

    public void Heal(float amount)
    {
        Value += amount;
    }
}
