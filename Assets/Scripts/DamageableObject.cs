using UnityEngine;
using System.Collections;
using System;

namespace Schmup
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DamageableObject : MonoBehaviour, IDamageable
    {
        //Datamembers
        [SerializeField]
        private Collider2D m_Collider;

        [SerializeField]
        private int m_MaxHealth;
        public int MaxHealth
        {
            get { return m_MaxHealth; }
        }

        private int m_Health;
        public int Health
        {
            get { return m_Health; }
        }

        //Events
        private event Action m_HealEvent;
        public Action HealEvent
        {
            get { return m_HealEvent; }
            set { m_HealEvent = value; }
        }

        private event Action m_DamageEvent;
        public Action DamageEvent
        {
            get { return m_DamageEvent; }
            set { m_DamageEvent = value; }
        }

        private event Action m_DeathEvent;
        public Action DeathEvent
        {
            get { return m_DeathEvent; }
            set { m_DeathEvent = value; }
        }

        //Functions
	    private void Start()
        {
	        if (m_Collider == null)
                Debug.LogError("DamagealbeObject doesn't contain a collider!");

            m_Health = m_MaxHealth;
	    }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Check if the object that we hit was a damagedealer
            IDamageDealer damageDealer = other.gameObject.GetComponent<IDamageDealer>();

            if (damageDealer != null)
            {
                int damage = damageDealer.GetDamage();
                Damage(damage);
            }
        }

        #region IDamageable

        public void Heal(int health)
        {
            if (health <= 0 || m_Health >= m_MaxHealth)
                return;

            m_Health += health;

            if (m_Health > m_MaxHealth)
                m_Health = m_MaxHealth;

            if (m_HealEvent != null)
                m_HealEvent();
        }

        public void Damage(int damage)
        {
            if (damage <= 0 || m_Health <= 0)
                return;

            m_Health -= damage;

            if (m_DamageEvent != null)
                m_DamageEvent();

            if (m_Health <= 0)
            {
                m_Health = 0;
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            if (m_DeathEvent != null)
                m_DeathEvent();
        }

        public bool IsAlive()
        {
            return (m_Health > 0);
        }

        #endregion
    }
}