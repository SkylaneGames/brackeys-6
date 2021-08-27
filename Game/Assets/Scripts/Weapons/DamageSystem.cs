using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DamagedEventHandler(object sender, DamagedEventArgs e);

public class DamagedEventArgs : EventArgs
{
    public float Amount { get; }
    public Vector3 DirectionOfAttack { get; }

    public DamagedEventArgs(float amount, Vector3 directionOfAttack)
    {
        Amount = amount;
        DirectionOfAttack = directionOfAttack;
    }
}

public class DamageSystem : StatusSystem
{
    public event EventHandler Destroyed;
    public event DamagedEventHandler Damaged;

    private bool _destroyed = false;

    public void Damage(float amount, Vector3 directionOfAttack)
    {
        Value -= amount;
        Damaged?.Invoke(this, new DamagedEventArgs(amount, directionOfAttack));

        if (Value == 0 && !_destroyed)
        {
            _destroyed = true;
            Destroyed?.Invoke(this, null);
        }
    }

    public void Heal(float amount)
    {
        Value += amount;
    }
}
