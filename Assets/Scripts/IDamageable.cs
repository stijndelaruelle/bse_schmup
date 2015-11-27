using UnityEngine;
using System.Collections;

namespace Schmup
{
    public interface IDamageable
    {
        void Heal(int health);
        void Damage(int damage);
        bool IsAlive();
    }
}