using UnityEngine;
using System.Collections;
using System;

namespace Schmup
{
    public interface IDamageable
    {
        int Health { get; }
        int MaxHealth { get; }

        Action HealEvent { get; set; }
        Action DamageEvent { get; set; }
        Action DeathEvent { get; set; }

        void Heal(int health);
        void Damage(int damage);
        bool IsAlive();
    }
}